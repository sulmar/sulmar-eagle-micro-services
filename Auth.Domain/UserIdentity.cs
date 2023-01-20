namespace Auth.Domain
{
    public class UserIdentity : Identity
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }


}