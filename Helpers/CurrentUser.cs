namespace kurs.Helpers
{
    public static class CurrentUser
    {
        public static string Login { get; set; }
        public static string Role { get; set; }
        public static bool IsAdmin => Role == "admin";
    }
}