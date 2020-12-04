﻿namespace CapstoneBE.Models.Custom.Users
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool IsNewUser { get; set; }
    }
}