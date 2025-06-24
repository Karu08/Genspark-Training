namespace OnlineGroceryPortal.Stores
{
    //to temporarily save refresh tokens
    public static class RefreshTokenStore
    {
        // username -> refreshToken
        public static Dictionary<string, string> Tokens = new();
    }
}