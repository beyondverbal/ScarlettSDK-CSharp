using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace BeyondVerbal.Cloud.ScarlettSDK.Response
{
    public enum SessionStatus
    {
        Started, Processing, Done
    }

    public class AnalysisResult
    {
        [JsonProperty("sessionStatus"),JsonConverter(typeof(StringEnumConverter))]
        public SessionStatus SessionStatus;

        [JsonProperty("analysisSegments")]
        public List<Segment> AnalysisSegments;

        public override string ToString()
        {
            return String.Format("status: {0}", SessionStatus);
        }
    }
}
