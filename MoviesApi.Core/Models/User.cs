﻿
using Microsoft.AspNetCore.Identity;

namespace MoviesApi.Core.Models
{
    public class User : IdentityUser
    {

        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
