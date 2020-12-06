namespace CapstoneBE.Models.Custom.Users
{
    public class UserCreate
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDel { get { return false; } }
        public bool IsNewUser { get { return true; } }
    }
}