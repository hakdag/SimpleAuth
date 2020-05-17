namespace SimpleAuth.Common
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string PasswordChangeStrategy { get; set; }
        public string AuthenticateAttemptStrategy { get; set; }
        public int RemainingAttemptsCount { get; set; }
        public int PasswordChangeHistoryRule { get; set; }
    }
}
