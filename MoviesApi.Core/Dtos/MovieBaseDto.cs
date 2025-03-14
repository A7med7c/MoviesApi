using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Dtos
{
    public class MovieBaseDto
    {
            public string Title { get; set; }
            public int Year { get; set; }
            public double Rate { get; set; }
            [MaxLength(2500)]
            public string StoreLine { get; set; }
            public int GenreId { get; set; }
        }

}
