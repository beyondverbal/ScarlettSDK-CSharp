using BeyondVerbal.Cloud.ScarlettSDK.Extensions;
using BeyondVerbal.Cloud.ScarlettSDK.Properties;
using BeyondVerbal.Cloud.ScarlettSDK.Request;
using BeyondVerbal.Cloud.ScarlettSDK.Response;
using Newtonsoft.Json;
using StringInject;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BeyondVerbal.Cloud.ScarlettSDK.Session
{
    public class EmotionsAnalyzerSession
    {
        private SessionParameters _sessionParameters;
        private Guid _apiKey;
        private FollowupActions _actions;
        private Task _StreamTask;
        private SessionStatus _SessionStatus;

        private EmotionsAnalyzerSession(SessionParameters sessionParameters, Guid apiKey)
        {
            _sessionParameters = sessionParameters;

            if (_sessionParameters._dataFormat.AutoDetect)
            {
                _sessionParameters._dataFormat = null;
            }

            _apiKey = apiKey;
        }

        internal static EmotionsAnalyzerSession Initialize(SessionParameters sessionParameters, Guid apiKey)
        {
            var session=new EmotionsAnalyzerSession(sessionParameters, apiKey);

            session.Initialize();

            return session;
        }

        private void Initialize()
        {
            try
            {
                HttpWebRequest webRequest = WebRequestExtensions.CreateJsonPostRequest(new Uri(Settings.Default.APIStartUrl.Inject(new { APIKey = _apiKey })));

                string reqBody = JsonConvert.SerializeObject(_sessionParameters);
                using (System.IO.Stream requestStream = webRequest.GetRequestStream())
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(requestStream))
                        sw.Write(reqBody);
                }

                var result = webRequest.ReadJsonResponseAs<Result<AnalysisResult>>();

                _actions = result.followupActions;
            }
            catch (WebException exception)
            {
                HttpWebResponse errorResponse = (HttpWebResponse)exception.Response;
                var serializer = new JsonSerializer();

                using (StreamReader streamReader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    dynamic response = serializer.Deserialize(new JsonTextReader(streamReader));

                    throw new Exception(response["reason"].ToString());
                }
            }
        }        

        private void IntervalHandler()
        {
            if (_StreamTask==null)
            {
                Task.Delay(_sessionParameters.PollingInterval).ContinueWith(task => IntervalHandler()); 
                return;
            }

            GetAnalysis();

            if (_SessionStatus != SessionStatus.Done)
            {
                Task.Delay(_sessionParameters.PollingInterval).ContinueWith(task => IntervalHandler());
            }
            else
            {
                OnProcessingDone(new EventArgs());
            }
        }

        private void GetAnalysis()
        {
            HttpWebRequest webRequest = WebRequestExtensions.CreateJsonGetRequest(_actions.analysis);

            var response = webRequest.ReadJsonResponseAs<Result<AnalysisResult>>();

            _actions.MergeFrom(response);

            _SessionStatus = response.result.SessionStatus;

            if (response.result.AnalysisSegments != null && response.result.AnalysisSegments.Count() > 0)
            {
                OnNewAnalysis(new AnalysisEventArgs() { analysis = response.result });
            }            
        }

        public event EventHandler<EventArgs> ProcessingDone;
        protected virtual void OnProcessingDone(EventArgs e)
        {
            EventHandler<EventArgs> handler = ProcessingDone;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<AnalysisEventArgs> NewAnalysis;
        protected virtual void OnNewAnalysis(AnalysisEventArgs e)
        {
            EventHandler<AnalysisEventArgs> handler = NewAnalysis;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public async Task AnalyzeAsync(Stream voiceStream)
        {
            IntervalHandler();
            _StreamTask = UpStreamVoiceData(voiceStream);

            await _StreamTask;
        }

        public void Analyze(Stream voiceStream)
        {
            IntervalHandler();
            (_StreamTask = UpStreamVoiceData(voiceStream)).Wait();
        }      

        public void Vote(Segment analysisSegment, string voteSubject, string verbalVote, int voteScore)
        {
            HttpWebRequest webRequest = WebRequestExtensions.CreateJsonPostRequest(_actions.vote);

            Vote vote = new Vote() { duration = (int)analysisSegment.duration, offset = (int)analysisSegment.offset, subject = voteSubject, verbalVote = verbalVote, voteScore = voteScore };

            webRequest.PostJson(vote);

            var result = webRequest.ReadJsonResponseAs<Result<string>>();            
        }

        private async Task<Result<AnalysisResult>> UpStreamVoiceData(Stream voiceStream)
        {
            HttpWebRequest webRequest = WebRequestExtensions.CreateJsonPostRequest(_actions.upStream);

            webRequest.ReadWriteTimeout = 100000;
            webRequest.Timeout = 1000000;
            webRequest.SendChunked = true;            

            webRequest.AllowWriteStreamBuffering = false;
            webRequest.AllowReadStreamBuffering = false;

            using (var requeststream = webRequest.GetRequestStream())
            {
                await voiceStream.CopyStreamWithAutoFlush(requeststream);
                requeststream.Close();
            }

            return webRequest.ReadJsonResponseAs<Result<AnalysisResult>>();            
        }
    }
}
