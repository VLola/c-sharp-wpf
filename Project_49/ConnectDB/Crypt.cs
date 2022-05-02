namespace Project_49.Control
{
    public static class Crypt
    {
        public static string Generate(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
        public static bool Veryfy(string pass, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(pass, hash);
        }
    }
}
