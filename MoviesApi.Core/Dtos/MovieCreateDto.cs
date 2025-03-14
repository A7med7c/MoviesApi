using Microsoft.AspNetCore.Http;
using MoviesApi.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Dtos
{
    public class MovieCreateDto : MovieBaseDto
    {
        public IFormFile Poster { get; set; }
    }
}
