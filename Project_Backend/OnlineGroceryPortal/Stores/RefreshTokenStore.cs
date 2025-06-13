namespace OnlineGroceryPortal.Stores
{
    public static class RefreshTokenStore
    {
        // username -> refreshToken
        public static Dictionary<string, string> Tokens = new();
    }
}