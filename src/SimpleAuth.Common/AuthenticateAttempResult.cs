namespace SimpleAuth.Common
{
    public class AuthenticateAttempResult
    {
        public bool Verified { get; set; }
        public AuthenticationToken VerifiedAttempt { get; set; }
        public AuthenticationToken UnverifiedAttempt { get; set; }
    }
}
