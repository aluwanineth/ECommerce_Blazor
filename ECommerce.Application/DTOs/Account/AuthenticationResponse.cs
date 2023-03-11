using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? JWToken { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
    }
}
