namespace CapstoneBE.Models.Custom.Users
{
    public class UserBasicInfo
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int[] LocationIds { get; set; }
    }
}