namespace TreeGecko.Archery.Server.JsonObjects
{
    public class LoginResult : BaseResult
    {
        public string Username { get; set; }
        public string AuthToken { get; set; }
        public string NeedsEula { get; set; }
        public string EulaGuid { get; set; }
        public string EulaText { get; set; }
    }
}