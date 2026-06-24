namespace incidentmanagement.Auth
{
    public class JwSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; } 
        public string Audience { get; set; }
    }
}
