using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace bqInet
{
    public class CbqDownloader
    {
        private readonly string _url;
        private readonly string _fullPathWhereToSave;
        private bool _result = false;
        bool downloadComplete = false;
        public CbqDownloader(string url, string fullPathWhereToSave)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (string.IsNullOrEmpty(fullPathWhereToSave)) throw new ArgumentNullException("fullPathWhereToSave");

            this._url = url;
            this._fullPathWhereToSave = fullPathWhereToSave;
        }

        public bool StartDownload(int timeout, System.Net.DownloadProgressChangedEventHandler downloadProgress = null, AsyncCompletedEventHandler downloadComplete = null)
        {
            try
            {
                //System.IO.Directory.CreateDirectory(Path.GetDirectoryName(_fullPathWhereToSave));

                if (File.Exists(_fullPathWhereToSave + "\\WebControl_V2.bqt"))
                {
                    File.Delete(_fullPathWhereToSave + "\\WebControl_V2.bqt");
                }
                using (WebClient client = new WebClient())
                {
                    var ur = new Uri(_url);
                    // client.Credentials = new NetworkCredential("username", "password");
                    if (downloadComplete == null && downloadProgress == null)
                    {
                        client.DownloadProgressChanged += WebClientDownloadProgressChanged;
                        client.DownloadFileCompleted += WebClientDownloadCompleted;
                    }
                    else
                    {
                        client.DownloadProgressChanged += downloadProgress;
                        client.DownloadFileCompleted += downloadComplete;
                    }
                    Console.WriteLine(@"Downloading file:");

                    //Action<object> action = (object obj) =>
                    //{
                    //    byte[] data = client.DownloadData(ur);
                    //    using (BinaryWriter w = new BinaryWriter(new FileStream(_fullPathWhereToSave + "\\WebControl_V2.exe", FileMode.Create)))
                    //    {
                    //        w.Write(data);
                    //        w.Close();
                    //    }
                    //};
                    //Task t1 = new Task(action, "downloadFile");
                    //t1.Start();      

                    
                    client.DownloadFileAsync(ur, _fullPathWhereToSave + "\\WebControl_V2.bqt");
                   
                    //_semaphore.Wait(timeout);
                    //return _result && File.Exists(_fullPathWhereToSave);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Was not able to download file!");
                Console.Write(e);
                return false;
            }
            finally
            {
            }
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\r     -->    {0}%.", e.ProgressPercentage);
        }

        private void WebClientDownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            downloadComplete = true;
            _result = !args.Cancelled;
            if (!_result)
            {
                Console.Write(args.Error.ToString());
            }
            Console.WriteLine(Environment.NewLine + "Download finished!");
        }

        public static bool DownloadFile(string url, string fullPathWhereToSave, int timeoutInMilliSec)
        {
            return new CbqDownloader(url, fullPathWhereToSave).StartDownload(timeoutInMilliSec);
        }
        public static bool DownloadFile(string url, string fullPathWhereToSave, System.Net.DownloadProgressChangedEventHandler downloadProgress, AsyncCompletedEventHandler downloadComplete)
        {
            return new CbqDownloader(url, fullPathWhereToSave).StartDownload(0, downloadProgress, downloadComplete);
        }
    }
}
