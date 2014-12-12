using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Diagnostics;

namespace ProcMonTest
{
    class Program
    {
        static string time = DateTime.Now.Millisecond.ToString();

        static void Main(string[] args)
        {
            CreateAndDeleteFile();
            CreateRegistryValue();
            ConnectToWebsite();
            LaunchCmd();
        }

        static void LaunchCmd()
        {
            try
            {
                Process si = new Process();
                si.StartInfo.WorkingDirectory = @"c:\";
                si.StartInfo.UseShellExecute = false;
                si.StartInfo.FileName = "cmd.exe";
                si.StartInfo.Arguments = "dir";
                si.StartInfo.CreateNoWindow = true;
                si.StartInfo.RedirectStandardInput = true;
                si.StartInfo.RedirectStandardOutput = true;
                si.StartInfo.RedirectStandardError = true;
                si.Start();
                string output = si.StandardOutput.ReadToEnd();
                si.Close();
                si.Kill();
                Console.WriteLine("Ran dir at the command line");
            }
            catch (Exception ex)
            {
                Console.WriteLine(time + "-" + ex.Message);
            }
        }

        static void CreateAndDeleteFile()
        {
            try
            {
                string file = @"c:\windows\temp\neonprimetimeTestFile" + time + ".txt";

                using (FileStream fs = File.Create(file))
                {
                    Byte[] text = new UTF8Encoding(true).GetBytes("neonprimetimeTestText" + time);
                    fs.Write(text, 0, text.Length);
                }
                Console.WriteLine("Created file " + file);
                File.Delete(file);
                Console.WriteLine("Deleted file " + file);
            }
            catch (Exception ex)
            {
                Console.WriteLine(time + "-" + ex.Message);
            }
            
        }

        static void CreateRegistryValue()
        {
            try
            {
                RegistryKey key;
                key = Registry.CurrentUser.CreateSubKey("neonprimetimeTestKey" + time);
                key.SetValue("neonprimetimeTestName" + time, "neonprimetimeTestValue" + time);
                Console.WriteLine("Created Registry Key" + key.ToString());
                key.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(time + "-" + ex.Message);
            }
        }

        static void ConnectToWebsite()
        {
            try
            {
                string url = "http://www.google.com?id=neonprimetimetest" + time;
                HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(url);
                WebRequestObject.UserAgent = "neonprimetime Test " + time;
                WebRequestObject.Referer = url + "referer";
                WebResponse Response = WebRequestObject.GetResponse();
                Stream WebStream = Response.GetResponseStream();
                StreamReader Reader = new StreamReader(WebStream);
                string PageContent = Reader.ReadToEnd();
                Reader.Close();
                WebStream.Close();
                Response.Close();
                Console.WriteLine((String.IsNullOrWhiteSpace(PageContent) ? "EMPTY" : "Connect to " + url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(time + "-" + ex.Message);
            }
        }
    }
}
