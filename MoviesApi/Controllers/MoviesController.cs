using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Core.Consts;
using MoviesApi.Core.Dtos;
using MoviesApi.Core.IRepositories;
using MoviesApi.Core.Models;
using System.Linq.Expressions;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        private new List<string> _allowedExtensions = new List<string> { ".jpg",".png" };
        private long _maxAllowedPosterSize = 1048576;
        public MoviesController(IUniteOfWork uniteOfWork,IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var movies = await _uniteOfWork.Movies.GetAllAsync();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            
            var movie = await _uniteOfWork.Movies.GetByIdWithProperties(id,includeProperties: new[] { "Genre" });
           
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        //[HttpGet("GetByGenreId")]
        //public async Task<IActionResult> GetByGenreIdAsync(int genreId)
        //{
        //    var movies = await _uniteOfWork.Movies.FindAsync(m=> m.GenreId == genreId);
         
        //    return Ok(movies);
        //}



        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(int genreId, int? take = null, int? skip = null)
        {
            var movies = await _uniteOfWork.Movies.GetAllAsync(
              criteria: m => m.GenreId == genreId,
              take: take,
              skip: skip,
              orderBy: m => m.Rate,
              orderDirection: OrderBy.Ascending,
              includeProperties: new[] { "Genre" });

            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync([FromForm]MovieCreateDto movieDto)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                  return BadRequest("Only .png and .jpg images are allowed!");
            
            if(movieDto.Poster.Length >_maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre = await _uniteOfWork.Genres.GetByIdAsync(movieDto.GenreId);

            if(isValidGenre == null)
                return BadRequest("Invalid Genre!");

            using var dataStream = new MemoryStream();
            await movieDto.Poster.CopyToAsync(dataStream);


            var movie = _mapper.Map<Movie>(movieDto);
            movie.Poster = dataStream.ToArray();
            
            await _uniteOfWork.Movies.AddAsync(movie);
            await _uniteOfWork.SaveAsync();

            return Ok(movie);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(int id,[FromForm]MovieUpdateDto movieDto)
        {
            var movie = await _uniteOfWork.Movies.GetByIdAsync(id);
            
            if (movie == null)
                return NotFound($"There Is No Movie With this Id{id}");

            var isValidGenre = await _uniteOfWork.Genres.GetByIdAsync(movieDto.GenreId);

            if (isValidGenre == null)
                return BadRequest("Invalid Genre!");
            
            using var dataStream = new MemoryStream();

            if (movieDto.Poster != null)
            {

                if (!_allowedExtensions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (movieDto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");
               
               
                await movieDto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();

            }


            movie = _mapper.Map<Movie>(movieDto);
            movie.Poster = dataStream.ToArray();

            _uniteOfWork.Movies.Update(movie);
            await _uniteOfWork.SaveAsync();

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _uniteOfWork.Movies.GetByIdAsync(id);
           
            if (movie == null)
                return NotFound($"There Is No Genre With this Id {id}");

            _uniteOfWork.Movies.Delete(movie);
            await _uniteOfWork.SaveAsync();

            return Ok(movie);
        }
    }
}
