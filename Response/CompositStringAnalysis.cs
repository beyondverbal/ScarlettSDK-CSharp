using System;

namespace BeyondVerbal.Cloud.ScarlettSDK.Response
{
    public class CompositStringAnalysis
    {
        public HeirarchyStringAnalysis value;

        public override string ToString()
        {
            return String.Format("Primary: {0}; Secondary: {1}", value.Primary, value.Secondary);
        }
    }
}
