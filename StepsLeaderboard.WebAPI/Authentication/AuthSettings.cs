namespace StepsLeaderboard.WebAPI.Authentication
{
    public class AuthSettings
    {
        public string SecretKey { get; set; } = new string('a', 256); 

        public string Issuer { get; set; } = "";

        public string Audience { get; set; } = "";
    }
}
