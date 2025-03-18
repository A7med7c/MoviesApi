using MoviesApi.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.IRepositories
{
    public interface IAuthService
    {
        Task<string> AddRoleAsync(AddRoleDto addRoleDto);
    }
}
