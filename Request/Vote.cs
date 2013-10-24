
namespace BeyondVerbal.Cloud.ScarlettSDK.Request
{
    public class Vote
    {
        public int? offset { get; set; }
        public int? duration { get; set; }
        public string subject { get; set; }
        public int voteScore { get; set; }
        public string verbalVote { get; set; }
    } 
}
