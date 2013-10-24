using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BeyondVerbal.Cloud.ScarlettSDK.Extensions
{
    public static class WebRequestExtensions
    {
        public static HttpWebRequest CreateJsonGetRequest(Uri url)
        {
            HttpWebRequest webRequest =
                                   (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";
            webRequest.KeepAlive = true;

            return webRequest;
        }

        public static HttpWebRequest CreateJsonPostRequest(Uri url)
        {
            HttpWebRequest webRequest =
                       (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.KeepAlive = true;

            return webRequest;
        }

        public static void PostJson<T>(this HttpWebRequest webRequest, T data)
        {
            string reqBody = JsonConvert.SerializeObject(data);
            using (System.IO.Stream requestStream = webRequest.GetRequestStream())
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(requestStream))
                    sw.Write(reqBody);

                requestStream.Close();
            }
        }

        public static T ReadJsonResponseAs<T>(this HttpWebRequest webRequest)
        {

            try
            {
                using (System.IO.Stream responseStream = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(responseStream))
                    {
                        T result = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());

                        responseStream.Close();
                        return result;
                    }                    
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0} - {1}", httpResponse.StatusCode, webRequest.RequestUri.ToString());
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        Console.WriteLine(text);
                    }
                }
                return default(T);
            }            
        }
    }
}
