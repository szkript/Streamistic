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

    public async Task<bool> StreamRecording(string username, string formattedUploadDate)
    {
        Process rtmpdump = new Process();
        rtmpdump.StartInfo.FileName = "cmd.exe";
        rtmpdump.StartInfo.Arguments = "/C rtmpdump -q --rtmp rtmp://64.225.24.130:1935/show --playpath " + username + " -o Videos/" + username + ".flv --live";
        rtmpdump.StartInfo.UseShellExecute = true;

        bool result = await Task.Run<bool>(() =>
            {
                int counter = 0;
                while (true)
                {
                    rtmpdump.Start();
                    rtmpdump.WaitForExit();
                    FileInfo file = new FileInfo("Videos/" + username + ".flv");
                    try
                    {
                        if (file.Length > 0)
                        {
                            Process ffmpeg = new Process();
                            ffmpeg.StartInfo.FileName = "cmd.exe";
                            ffmpeg.StartInfo.Arguments = "/C ffmpeg -i Videos/" + username + ".flv -c:v libx264 -crf 19 -strict experimental Videos/" + username + formattedUploadDate + ".mp4";
                            ffmpeg.StartInfo.UseShellExecute = true;
                            ffmpeg.StartInfo.RedirectStandardOutput = false;
                            ffmpeg.Start();
                            ffmpeg.WaitForExit();
                            file.Delete();
                            return true;
                        }
                        if (counter > 10)
                        {
                            file.Delete();
                            return false;
                        }
                        counter++;
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        string apPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                        Console.WriteLine(apPath);
                        try
                        { 
                            File.Create(ex.FileName);
                        }
                        catch(DirectoryNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            });
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
