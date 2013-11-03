using System;

namespace BeyondVerbal.Cloud.ScarlettSDK.Response
{
    public class FollowupActions
    {
        public Uri analysis;
        public Uri upStream;
        public Uri vote;

        public void MergeFrom(Result<AnalysisResult> otherResult)
        {
            analysis = otherResult.followupActions.analysis ?? analysis;
            upStream = otherResult.followupActions.upStream ?? upStream;
            vote = otherResult.followupActions.vote ?? vote;
        }
    }
}
