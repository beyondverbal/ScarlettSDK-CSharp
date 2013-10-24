using BeyondVerbal.Cloud.ScarlettSDK.Properties;
using BeyondVerbal.Cloud.ScarlettSDK.Request;
using BeyondVerbal.Cloud.ScarlettSDK.Session;
using System;

namespace BeyondVerbal.Cloud.ScarlettSDK
{
    public class EmotionsAnalyzer
    {
        private Guid _apiKey;
        public EmotionsAnalyzer()
        {
            _apiKey=Settings.Default.APIKey;
        }

        public EmotionsAnalyzer(Guid apiKey)
        {
            _apiKey = apiKey;
        }

        public EmotionsAnalyzerSession InitializeSession(SessionParameters sessionParameters)
        {
            return EmotionsAnalyzerSession.Initialize(sessionParameters, _apiKey);
        }
    }
}
