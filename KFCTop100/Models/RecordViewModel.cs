namespace KFCTop100.Models
{
    public class RecordViewModel
    {
        public List<LeaderBoardItem> items = new List<LeaderBoardItem>();
        public string SearchString { get; set; } = "";
    }
}
