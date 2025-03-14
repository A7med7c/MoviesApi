using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Dtos
{
    public class MovieUpdateDto : MovieBaseDto
    {
        public IFormFile? Poster { get; set; } 

    }
}
