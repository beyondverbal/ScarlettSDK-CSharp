using BeyondVerbal.Cloud.ScarlettSDK.Properties;
using Newtonsoft.Json;
using System;

namespace BeyondVerbal.Cloud.ScarlettSDK.Request
{
    public class SessionParameters
    {
        [JsonProperty("data_format")]
        internal IDataFormat _dataFormat;

        [JsonProperty("recorder_info")]
        internal RecorderInfo _recorderInfo;

        [JsonProperty("requiredAnalysisTypes")]
        internal RequiredAnalysis _requiredAnalysisTypes;


        private SessionParameters()
        {

        }

        public static SessionParameters Create()
        {
            return new SessionParameters();
        }

        public SessionParameters WithRecorderInfo(RecorderInfo recorderInfo)
        {
            _recorderInfo = recorderInfo;

            return this;
        }

        public SessionParameters WithDataFormat(IDataFormat dataFormat)
        {
            _dataFormat = dataFormat;

            return this;
        }

        public SessionParameters WithAnalyses(RequiredAnalysis requiredAnalysis)
        {
            _requiredAnalysisTypes = requiredAnalysis;

            return this;
        }

        [JsonIgnore]
        public TimeSpan PollingInterval
        {
            get { return Settings.Default.AnalysisPollingInterval; }
        }
    }
}
