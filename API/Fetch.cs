using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MovieMaster.API {
    public static class Fetch {
        public static HttpClient client = new HttpClient();
        public const string API_KEY = "922ac978d94b5c7c36be4279f6a3b6dc";
        public static string Data { get; set; }
        public static PosterSet posterSet = new PosterSet();
        public static PosterSetKids posterSetKids = new PosterSetKids();
        public static Movie movie = new Movie();
        public static TheCast theCast = new TheCast();
        public static VideoSet videoSet = new VideoSet();
        public static Actor actor = new Actor();
        public static External external = new External();
        public static MovieCredit movieCredit = new MovieCredit();
        public static ActorImages actorImages = new ActorImages();
        public static TopMovieSet topMovieSet = new TopMovieSet();

        public static async Task ActorDetails(string actorID) {
            ClearHeaders();

            // Get the actor details
            // https://api.themoviedb.org/3/person/24343?api_key=922ac978d94b5c7c36be4279f6a3b6dc&language=en-US
            HttpResponseMessage response = await client.GetAsync(
                "https://api.themoviedb.org/3/person/" + actorID + "?api_key=" + API_KEY);

            if(response.IsSuccessStatusCode) {
                Data = await response.Content.ReadAsStringAsync();
                actor = JsonSerializer.Deserialize<Actor>(Data);
            }
            else {
                Data = null;
            }

            response = await client.GetAsync(
                "https://api.themoviedb.org/3/person/" + actorID + "/images?api_key=" + API_KEY);

            if(response.IsSuccessStatusCode) {
                Data = await response.Content.ReadAsStringAsync();
                actorImages = JsonSerializer.Deserialize<ActorImages>(Data);
            }
            else {
                Data = null;
            }

            response = await client.GetAsync(
                "https://api.themoviedb.org/3/person/" + actorID + "/external_ids?api_key=" + API_KEY);

            if(response.IsSuccessStatusCode) {
                Data = await response.Content.ReadAsStringAsync();
                external = JsonSerializer.Deserialize<External>(Data);
            }
            else {
                Data = null;
            }

            response = await client.GetAsync(
                "https://api.themoviedb.org/3/person/" + actorID + "/movie_credits?api_key=" + API_KEY);

            if(response.IsSuccessStatusCode) {
                Data = await response.Content.ReadAsStringAsync();
                movieCredit = JsonSerializer.Deserialize<MovieCredit>(Data);
            }
            else {
                Data = null;
            }
        }
        
        public static async Task MovieDetails(string movieID) {
            ClearHeaders();

            // Get the movie details
            // https://api.themoviedb.org/3/movie/766507?api_key=922ac978d94b5c7c36be4279f6a3b6dc&language=en-US
            HttpResponseMessage response = await client.GetAsync(
                "https://api.themoviedb.org/3/movie/" + movieID + "?api_key=" + API_KEY);

            if(response.IsSuccessStatusCode) {
                Data = await response.Content.ReadAsStringAsync();
                movie = JsonSerializer.Deserialize<Movie>(Data);
            }
            else {
                Data = null;
            }

            // Next get the cast images for that movie
            response = await client.GetAsync(
                "https://api.themoviedb.org/3/movie/" + movieID + "/credits?api_key=" + API_KEY);

            if(response.IsSuccessStatusCode) {
                Data = await response.Content.ReadAsStringAsync();
                theCast = JsonSerializer.Deserialize<TheCast>(Data);
            }
            else {
                Data = null;
            }

            // Get the video(s) for the movie
            response = await client.GetAsync(
                "https://api.themoviedb.org/3/movie/" + movieID + "/videos?api_key=" + API_KEY);

            if(response.IsSuccessStatusCode) {
                Data = await response.Content.ReadAsStringAsync();
                videoSet = JsonSerializer.Deserialize<VideoSet>(Data);
            }
            else {
                Data = null;
            }
        } // MovieDetails()

        public static async Task Search(string search) {
            ClearHeaders();

            // Does a movie Search!
            HttpResponseMessage response = await client.GetAsync(
                "https://api.themoviedb.org/3/search/movie?api_key=" + 
                    API_KEY + "&page=1&query=" + search);

            if(response.IsSuccessStatusCode) { // 200
                Data = await response.Content.ReadAsStringAsync();
                posterSet = JsonSerializer.Deserialize<PosterSet>(Data);
            }
            else {
                Data = null;
            }
        } // Search()

        public static async Task GetTrends() {
            ClearHeaders();

            // Gets the Movie trends for the last week(7 days)
            HttpResponseMessage response = await client.GetAsync(
                "https://api.themoviedb.org/3/trending/movie/week?api_key=" + 
                    API_KEY);

            if(response.IsSuccessStatusCode) { // 200
                Data = await response.Content.ReadAsStringAsync();
                posterSet = JsonSerializer.Deserialize<PosterSet>(Data);
            }
            else {
                Data = null;
            }

            // Gets the Top Rated movies
            // https://api.themoviedb.org/3/movie/top_rated?api_key=922ac978d94b5c7c36be4279f6a3b6dc&page=1
            response = await client.GetAsync(
                "https://api.themoviedb.org/3/movie/top_rated?api_key=" + API_KEY + "&page=1");

            if(response.IsSuccessStatusCode) { // 200
                Data = await response.Content.ReadAsStringAsync();
                topMovieSet= JsonSerializer.Deserialize<TopMovieSet>(Data);
            }
            else {
                Data = null;
            }
        } // GetTrends()

        private static void ClearHeaders() {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));
        }
    }
}