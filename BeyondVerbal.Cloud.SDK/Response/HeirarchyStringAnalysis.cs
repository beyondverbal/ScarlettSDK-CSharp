using System;

namespace BeyondVerbal.Cloud.ScarlettSDK.Response
{
    public class HeirarchyStringAnalysis
    {
        public string Primary;
        public string Secondary;

        public override string ToString()
        {
            return String.Format("Primary: {0}; Secondary: {1}", Primary, Secondary);
        }
    }
}
