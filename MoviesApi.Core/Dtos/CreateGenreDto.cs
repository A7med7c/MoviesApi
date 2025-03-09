using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Dtos
{
    public class CreateGenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
