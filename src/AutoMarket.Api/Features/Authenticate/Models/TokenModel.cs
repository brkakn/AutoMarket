﻿using System;

namespace AutoMarket.Api.Features.Authenticate.Models
{
    public class TokenModel
    {
        public TokenModel(string accessToken)
        {
            expires_in = TimeSpan.FromDays(1).TotalSeconds.ToString();
            token_type = "Bearer";
            access_token = accessToken; ;
        }

        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
    }
}
