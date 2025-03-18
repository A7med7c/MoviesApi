using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Consts
{
    public class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";

       
        public static List<string> GetAllRoles()
        {
            return new List<string> { Admin, User};
        }
    }
}
