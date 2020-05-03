namespace SimpleAuth.Common
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string PasswordChangeStrategy { get; set; }
        public int PasswordChangeHistoryRule { get; set; }
    }
}
