using BeyondVerbal.Cloud.ScarlettSDK;
using BeyondVerbal.Cloud.ScarlettSDK.DataFormat;
using BeyondVerbal.Cloud.ScarlettSDK.Request;
using BeyondVerbal.Cloud.ScarlettSDK.Session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BeyondVerbal.Cloud.SDK.Sample
{
    class Program
    {
        static string filename;

        static void Main(string[] args)
        {
            filename = args[0];

            var EmotionsSessionParameters = SessionParameters.Create().WithDataFormat(new WAVFormat());

            var session = new EmotionsAnalyzer().InitializeSession(EmotionsSessionParameters);
            
            session.NewAnalysis += session_NewAnalysis;
            session.ProcessingDone += session_ProcessingDone;

            using (var stream = File.Open(args[0], FileMode.Open))
            {
                var task = session.AnalyzeAsync(stream);

                while (!task.Wait(100))
                {
                    Console.Write("\r" + (int)(((float)stream.Position / (float)stream.Length) * 100.0) + "%");
                    if (stream.Length <= stream.Position)
                    {
                        stream.Close();
                        break;
                    }
                }
            }

            Console.WriteLine("\nStreaming done.");
            Console.ReadLine();
        }

        static void session_ProcessingDone(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        static void session_NewAnalysis(object sender, ScarlettSDK.Session.AnalysisEventArgs e)
        {
            var analysis=JsonConvert.SerializeObject(e.analysis, Formatting.Indented);

            File.AppendAllText(ManipulateFilename(filename, "Analysis"), analysis);
            Console.Write(analysis);
        }

        private static string ManipulateFilename(string path, string suffix)
        {
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileExt = Path.GetExtension(path);

            return Path.Combine(dir, fileName + "." + suffix + ".txt");

        }
    }
}
