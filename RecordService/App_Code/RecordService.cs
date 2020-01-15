using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using SilkVideo.Models;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class RecordService : IService
{
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

    public async void StreamRecording(string username)
    {
        Video video = new Video();
        video.Path = null;
        Process rtmpdump = new Process();
        rtmpdump.StartInfo.FileName = "cmd.exe";
        rtmpdump.StartInfo.Arguments = "/C rtmpdump -q --rtmp rtmp://64.225.24.130:1935/show --playpath " + username + " -o Videos/" + username + ".flv --live";
        rtmpdump.StartInfo.UseShellExecute = true;
        rtmpdump.StartInfo.RedirectStandardOutput = false;
        await Task.Run(() =>
        {
            int counter = 0;
            while (true)
            {
                rtmpdump.Start();
                rtmpdump.WaitForExit();
                FileInfo file = new FileInfo("Videos/" + username + ".flv");
                if (file.Length > 0)
                {
                    DateTime uploadDate = DateTime.Now;
                    string formattedUploadDate = "" + uploadDate.Year + uploadDate.Month + uploadDate.Day + uploadDate.Hour + uploadDate.Minute + uploadDate.Second;

                    video.Description = "In Progress";
                    video.Path = "Videos/" + username + formattedUploadDate + ".mp4";
                    video.UploadTime = uploadDate;
                    Process ffmpeg = new Process();
                    ffmpeg.StartInfo.FileName = "cmd.exe";
                    ffmpeg.StartInfo.Arguments = "/C ffmpeg -i Videos/" + username + ".flv -c:v libx264 -crf 19 -strict experimental Videos/" + username + formattedUploadDate + ".mp4";
                    ffmpeg.StartInfo.UseShellExecute = true;
                    ffmpeg.StartInfo.RedirectStandardOutput = false;
                    ffmpeg.Start();
                    ffmpeg.WaitForExit();
                    file.Delete();

                    break;
                }

                if (counter > 10)
                {
                    file.Delete();
                    break;
                }

                counter++;
            }
        });
    }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}
}
