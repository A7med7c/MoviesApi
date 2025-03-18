

namespace MoviesApi.Core.Dtos
{
    public class UpdateUserDetailsDto
    {
    //    public  string Username { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
