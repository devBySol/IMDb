using Lab5.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lab5.Services
{
    public class MovieService
    {   
        // 클래스 필드 HttpClient 효율적 사용을 위해 
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ActorServices _actorService = new ActorServices(); //TMDB에서 배우정보 받아옴(OMdb와 비동기로 동시에 호출)
        private readonly string _apikey = "4568d10a";
        private List<Movie> _cachedMovies = new List<Movie>(); //대량 데이터처리를 빠르게 하기 위해 한 번 불러온 데이터는 캐시에 저장

        // 하드코딩된 영화 제목들
        private readonly List<string> _movieTitles = new List<string>
        {
            "A Quiet Place",
            "Avatar: The Way of Water",
            "Avengers: Endgame",
            "Barbie",
            "Black Panther",
            "Black Widow",
            "Blade Runner 2049",
            "Coco",
            "Doctor Strange",
            "Don't Look Up",
            "Dune",
            "Django Unchained",
            "Everything Everywhere All at Once",
            "Fight Club",
            "Forrest Gump",
            "Frozen",
            "Get Out",
            "Guardians of the Galaxy",
            "Harry Potter and the Sorcerer's Stone",
            "Her",
            "Inception",
            "Interstellar",
            "John Wick",
            "Joker",
            "La La Land",
            "Logan",
            "Mad Max: Fury Road",
            "No Time to Die",
            "Parasite",
            "Pirates of the Caribbean: The Curse of the Black Pearl",
            "Pulp Fiction",
            "Shang-Chi and the Legend of the Ten Rings",
            "Shrek",
            "Snow White",
            "Spider-Man: Across the Spider-Verse",
            "Spider-Man: No Way Home",
            "Tenet",
            "The Batman",
            "The Conjuring",
            "The Dark Knight",
            "The Godfather",
            "The Grand Budapest Hotel",
            "The Hunger Games",
            "The Incredibles",
            "The Lion King",
            "The Lord of the Rings: The Return of the King",
            "The Matrix",
            "The Revenant",
            "The Shawshank Redemption",
            "The Suicide Squad",
            "The Wolf of Wall Street",
            "Toy Story",
            "Whiplash",
            "Wicked",
            "Zootopia"
        };


        // Contructor IHttpClient에서 받아온 정보 관리
        public MovieService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<List<Movie>> GetMoviesAsync()
        {
            if (_cachedMovies.Count > 0)
            {
                return _cachedMovies; //캐시가 있으면 return
            }

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            var movies = new List<Movie>();
            foreach (var title in _movieTitles)
            {
                string apiUrl = $"http://www.omdbapi.com/?t={title}&apikey={_apikey}";
                var response = await httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                    continue;
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var movieData = JsonConvert.DeserializeObject<Movie>(jsonResponse);

                    if (movieData != null && movieData.Title != null)
                    {
                        movieData.Cast = await _actorService.GetActorsFromTmdbAsync(movieData.imdbID);
                        movies.Add(movieData);
                    }
                }
            }

            _cachedMovies = movies; //없으면 API 호출로 데이터 불러온 후 캐시에 저장
            return movies;
        }

       
        public Movie GetMovieById(int id)
        {
            return _cachedMovies.FirstOrDefault(m => m.Id == id);
        }
    }
}
