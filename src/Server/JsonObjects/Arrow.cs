namespace TreeGecko.Archery.Server.JsonObjects
{
    public class Arrow : BaseObject
    {
        public int Score { get; set; }
        public bool IsX { get; set; }

        public Arrow()
        {
            
        }

        public Arrow(Library.Archery.Objects.Arrow _arrow)
            : base(_arrow.Guid)
        {
            Score = _arrow.Score;
            IsX = _arrow.IsX;
        }
    }
}