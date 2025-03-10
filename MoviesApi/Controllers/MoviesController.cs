using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Core.Dtos;
using MoviesApi.Core.IRepositories;
using MoviesApi.Core.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;

        public MoviesController(IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto movieDto)
        {

            using var dataStream = new MemoryStream();
            await movieDto.Poster.CopyToAsync(dataStream);


            var movie = new Movie
            {
                Title = movieDto.Title,
                Poster = dataStream.ToArray(),
                Rate = movieDto.Rate,
                StoreLine = movieDto.StoreLine,
                Year = movieDto.Year,
                GenreId = movieDto.GenreId
            };
            
            await _uniteOfWork.Movies.AddAsync(movie);
            await _uniteOfWork.SaveAsync();

            return Ok(movie);
        }
    }
}
