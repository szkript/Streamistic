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

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class RecordService : IService
{
    

    public string GetData(int value)
    {
        return string.Format("You entered: {0}", value);
    }

    public async Task<bool> StreamRecordingAsync(string username, string formattedUploadDate, string filePath)
    {
        bool result;
        try
        {
            Process rtmpdump = new Process();
            rtmpdump.StartInfo.FileName = "cmd.exe";
            rtmpdump.StartInfo.Arguments = "/C rtmpdump -q --rtmp rtmp://64.225.24.130:1935/show --playpath " + username + " -o " + filePath + "/" + username + ".flv --live";
            rtmpdump.StartInfo.UseShellExecute = true;

            result = await Task.Run<bool>(() =>
                {
                    bool recordResult = false;
                    int counter = 0;
                    while (true)
                    {
                        rtmpdump.Start();
                        rtmpdump.WaitForExit();
                        rtmpdump.Close();
                        FileInfo file = new FileInfo(filePath + "/" + username + ".flv");
                        if (file.Length > 0)
                        {
                            Process ffmpeg = new Process();
                            ffmpeg.StartInfo.FileName = "cmd.exe";
                            ffmpeg.StartInfo.Arguments = "/C ffmpeg -i " + filePath + "/" + username + ".flv -c:v libx264 -crf 19 -strict experimental " + filePath + "/" + username + formattedUploadDate + ".mp4";
                            ffmpeg.StartInfo.UseShellExecute = true;
                            ffmpeg.StartInfo.RedirectStandardOutput = false;
                            ffmpeg.Start();
                            ffmpeg.WaitForExit();
                            file.Delete();
                            ffmpeg.Close();
                            ffmpeg.Dispose();
                            rtmpdump.Dispose();
                            recordResult = true;
                            break;
                        }
                        if (counter > 10)
                        {
                            rtmpdump.Dispose();
                            file.Delete();
                            recordResult = false;
                            break;
                        }
                        counter++;
                    }
                    return recordResult;
                });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = false;
        }
        return result;
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
