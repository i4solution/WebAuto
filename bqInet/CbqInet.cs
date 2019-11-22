using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.IO;
using System.Management;

namespace bqInet
{
    public class CbqInet
    {
        public static bool PingDns()
        {
            try
            {
                using (Ping p = new Ping())
                {
                    string data = "testing internet";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 5200;

                    try
                    {
                        PingReply reply = p.Send("203.162.4.190", timeout, buffer);
                        if (reply.Status == IPStatus.Success)
                            return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public static HttpWebResponse PostData(Uri postUrl, IWebProxy proxyServer, string postData)
        {
            HttpWebRequest httpRequest = null;

            HttpWebResponse httpResponse = null;

            try
            {
                byte[] postBytes = null;

                // Create HTTP web request

                httpRequest = (HttpWebRequest)WebRequest.Create(postUrl);

                httpRequest.Method = "POST";

                // Posted forms need to be encoded so change the content type

                httpRequest.ContentType = "application/x-www-form-urlencoded";

                // Set the proxy

                httpRequest.Proxy = proxyServer;

                postBytes = Encoding.UTF8.GetBytes(postData);

                httpRequest.ContentLength = postBytes.Length;

                // Retrieve the response

                Console.WriteLine("Retrieving the response...");

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();                
            }
            catch (Exception ii)
            {
                
            }
            return httpResponse;
        }
        public static bool RequestPOSTPhpDBRead(string user, string pass)
        {
            string siteContent = string.Empty;

            string url = "http://vuhoang8x.000webhostapp.com/testdb.php/read";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 ( compatible ) ";
            request.ContentType = "application/x-www-form-urlencoded";

            //pass = CbqCryt.crypt(pass, "$1$bacquanl$");

            string data = "usr=" + Uri.EscapeDataString(user);
            data += "&pass=" + Uri.EscapeDataString(pass);
            request.ContentLength = data.Length;
            var dataS = System.Text.Encoding.ASCII.GetBytes(data);
            request.ContentLength = dataS.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(dataS, 0, dataS.Length);
            }
            HttpWebResponse response = null;
            HttpStatusCode statusCode;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
            }
            catch (WebException ii)
            {
                statusCode = ((HttpWebResponse)ii.Response).StatusCode;
                Console.WriteLine(((HttpWebResponse)ii.Response).StatusCode.ToString());
            }
            if (statusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                data = reader.ReadToEnd();

                Console.WriteLine(data);
                return true;
            }
            else
            {
                Console.WriteLine("Wrong username or password");
            }
            //data = data.Replace(")", "").Replace("(", "");
            return false;
        }
        public static string RequestPOSTPhpCheckVersion()
        {
            string siteContent = string.Empty;

            string url = "http://vuhoang8x.000webhostapp.com/testdb.php/checkversion";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 ( compatible ) ";
            request.ContentType = "application/x-www-form-urlencoded";

            //pass = CbqCryt.crypt(pass, "$1$bacquanl$");

            string data = "";
            //data += "&pass=" + Uri.EscapeDataString(pass);
            //request.ContentLength = data.Length;
            //var dataS = System.Text.Encoding.ASCII.GetBytes(data);
            request.ContentLength = 0;
            //using (Stream stream = request.GetRequestStream())
            //{
            //    stream.Write(dataS, 0, dataS.Length);
            //}
            HttpWebResponse response = null;
            HttpStatusCode statusCode;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
            }
            catch (WebException ii)
            {
                statusCode = ((HttpWebResponse)ii.Response).StatusCode;
                Console.WriteLine(((HttpWebResponse)ii.Response).StatusCode.ToString());
            }
            if (statusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                data = reader.ReadToEnd();

                Console.WriteLine(data);
                return data;
            }
            else
            {
                Console.WriteLine("Cannot check Version");
            }
            //data = data.Replace(")", "").Replace("(", "");
            return "";
        }
        public static bool RequestPOSTPhpDBWrite(string user, string pass, string email, string phone)
        {
            string siteContent = string.Empty;

            string url = "http://vuhoang8x.000webhostapp.com/testdb.php/create";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 ( compatible ) ";
            request.ContentType = "application/x-www-form-urlencoded";

            //pass = CbqCryt.crypt(pass, "$1$bacquanl$");

            string data = "usr=" + Uri.EscapeDataString(user);
            data += "&pass=" + Uri.EscapeDataString(pass);
            data += "&email=" + Uri.EscapeDataString(email);
            data += "&phone=" + Uri.EscapeDataString(phone);
            request.ContentLength = data.Length;
            var dataS = System.Text.Encoding.ASCII.GetBytes(data);
            request.ContentLength = dataS.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(dataS, 0, dataS.Length);
            }

            HttpWebResponse response = null;
            HttpStatusCode statusCode;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
            }
            catch (WebException ii)
            {
                statusCode = ((HttpWebResponse)ii.Response).StatusCode;
                Console.WriteLine(((HttpWebResponse)ii.Response).StatusCode.ToString());
            }
            if (statusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                data = reader.ReadToEnd();

                Console.WriteLine(data);
                return true;
            }
            else { }
            
            return false;
        }
        public static string RequestDownloadFile()
        {
            string siteContent = string.Empty;

            string url = @"http://localhost/home/WebControl_V2.exe";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 ( compatible ) ";
            //request.ContentType = "application/x-www-form-urlencoded";

            //pass = CbqCryt.crypt(pass, "$1$bacquanl$");

            string data = "";
            //data += "&pass=" + Uri.EscapeDataString(pass);
            //request.ContentLength = data.Length;
            //var dataS = System.Text.Encoding.ASCII.GetBytes(data);
            request.ContentLength = 0;
            //using (Stream stream = request.GetRequestStream())
            //{
            //    stream.Write(dataS, 0, dataS.Length);
            //}
            HttpWebResponse response = null;
            HttpStatusCode statusCode;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
            }
            catch (WebException ii)
            {
                statusCode = ((HttpWebResponse)ii.Response).StatusCode;
                Console.WriteLine(((HttpWebResponse)ii.Response).StatusCode.ToString());
            }
            if (statusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                data = reader.ReadToEnd();
                using (StreamWriter w = new StreamWriter("abc.exe"))
                {                    
                    w.Write(data);
                    w.Close();
                }
                //Console.WriteLine(data);
                //return data;
            }
            else
            {
                Console.WriteLine("Cannot check Version");
            }
            //data = data.Replace(")", "").Replace("(", "");
            return "";
        }
    }
}
