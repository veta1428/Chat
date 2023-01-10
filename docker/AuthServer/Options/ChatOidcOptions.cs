namespace AuthServer.Options
{
    public class ChatOidcOptions
    {
        public string RedirectUri { get; set; } = null!;
        public string PostLogoutRedirectUri { get; set; } = null!;
    }
}