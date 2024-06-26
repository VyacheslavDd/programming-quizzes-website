﻿using Core.Base;
using Core.Enums;

namespace UserService.Api.ResponseModels.Auth
{
    public class AuthenticationResponse : BaseHttpResponse
    {
        public string Token { get; set; }
    }
}
