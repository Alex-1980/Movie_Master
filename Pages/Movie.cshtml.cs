using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieMaster.API;

namespace MovieMaster.Pages;

public class MovieModel : PageModel { 

    public string backdropPath;
    public string movieTitle;
    public string releaseDate;
    public string runtime;
    public string voteAverage;
    public string overview;
    public string posterPath = "https://image.tmdb.org/t/p/w500";
    public List<string> genres = new List<string>();

    public const int MAX_CAST = 8;
    public List<string> castImgs = new List<string>();
    public List<string> castNames = new List<string>();
    public List<string> castIDs = new List<string>();

    public List<string> videos = new List<string>();

    public async Task OnGet(string id) {
        await Fetch.MovieDetails(id);

        // Movie Details
        backdropPath = Fetch.movie.backdrop_path;
        movieTitle = Fetch.movie.title;
        releaseDate = Fetch.movie.release_date;
        runtime = Fetch.movie.runtime.ToString();
        voteAverage = string.Format("{0:0.0%}", Fetch.movie.vote_average / 100);
        posterPath += Fetch.movie.poster_path;
        overview = Fetch.movie.overview;

        for(int i = 0; i < Fetch.movie.genres.Count; i++) {
            genres.Add(Fetch.movie.genres[i].name);
        }

        // Cast Details
        if(Fetch.theCast.cast.Count >= MAX_CAST) {
            for(int i = 0; i < MAX_CAST; i++) {
                castImgs.Add(Fetch.theCast.cast[i].profile_path);
                castNames.Add(Fetch.theCast.cast[i].name);
                castIDs.Add(Fetch.theCast.cast[i].id.ToString());
            }
        }
        else {
            for(int i = 0; i < Fetch.theCast.cast.Count; i++) {
                castImgs.Add(Fetch.theCast.cast[i].profile_path);
                castNames.Add(Fetch.theCast.cast[i].name);
                castIDs.Add(Fetch.theCast.cast[i].id.ToString());
            }
        }

        for(int i = 0; i < Fetch.videoSet.results.Count; i++) {
            videos.Add(Fetch.videoSet.results[i].key);
        }
    } // OnGet()

    public void OnPostGetCast(string actorID) {
        Response.Redirect("/Actor?actorID=" + actorID);
    }
}