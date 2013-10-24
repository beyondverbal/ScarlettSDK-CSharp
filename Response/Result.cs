
namespace BeyondVerbal.Cloud.ScarlettSDK.Response
{
    public class Result<T>
    {
        public string status;
        public T result;
        public FollowupActions followupActions;
    }
}
