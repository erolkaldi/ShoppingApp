﻿

namespace ShoppingApp.IdentityModels
{
    public class Token
    {
        public string AccessToken { get; set; } = "";
        public DateTime Expiration { get; set; }
        public string Message { get; set; } = "";
    }
}
