using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Lab5.Models;

namespace Lab5.Services
{
    public class ActorServices
    {
        private readonly string tmdbApiKey = "1444a2e6bce219a4c2419892b101027b"; 
        private readonly string tmdbBaseUrl = "https://api.themoviedb.org/3";
        private readonly HttpClient httpClient;

        public ActorServices()
        {
            httpClient = new HttpClient();
        }

        //Actor info
        public async Task<List<Movie.Actor>> GetActorsFromTmdbAsync(string imdbId)
        {
            //imdbID → tmdbID 변환
            var tmdbIdResponse = await httpClient.GetStringAsync($"{tmdbBaseUrl}/find/{imdbId}?api_key={tmdbApiKey}&external_source=imdb_id");
            dynamic tmdbData = JsonConvert.DeserializeObject(tmdbIdResponse);
            string tmdbId = tmdbData.movie_results[0]?.id.ToString();

            if (string.IsNullOrEmpty(tmdbId))
                return null;

            //tmdbID로 배우 정보 가져오기
            var creditsResponse = await httpClient.GetStringAsync($"{tmdbBaseUrl}/movie/{tmdbId}/credits?api_key={tmdbApiKey}");
            dynamic creditsData = JsonConvert.DeserializeObject(creditsResponse);

            //배우 정보 파싱
            var actors = new List<Movie.Actor>();
            foreach (var cast in creditsData.cast)
            {
                string profilePath = cast.profile_path != null ? $"https://image.tmdb.org/t/p/w500{cast.profile_path}" : "/images/default-profile.png";
                actors.Add(new Movie.Actor
                {
                    Name = (string)cast.name,
                    ProfileImageUrl = profilePath
                });
            }
            return actors;
        }
    }
}
