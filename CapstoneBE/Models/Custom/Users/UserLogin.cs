﻿namespace CapstoneBE.Models.Custom.Users
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FcmToken { get; set; } = "";
    }
}