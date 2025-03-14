using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Models
{
    public interface IEntity
    {
        public int Id { get; set; }
    }
}
