using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieMaster.API;

namespace MovieMaster.Pages;

public class IndexModel : PageModel
{
    public const int MAX_RESULTS = 9;
    public List<string> posterURLS = new List<string>();
    public List<string> overviews = new List<string>();
    public List<string> movieIDS = new List<string>();

    public List<string> posterURLSTops = new List<string>();
    public List<string> overviewsTops = new List<string>();
    public List<string> movieIDSTops = new List<string>();
    
    public async Task OnGet() {
        await Fetch.GetTrends();
        if(Fetch.posterSet.results.Count <= MAX_RESULTS) {
            foreach(Poster poster in Fetch.posterSet.results) {
                posterURLS.Add(poster.poster_path);
                overviews.Add(poster.overview);
                movieIDS.Add(poster.id.ToString());
            }
        } 
        else {
            for(int i = 0; i < MAX_RESULTS; i++) {
                posterURLS.Add(Fetch.posterSet.results[i].poster_path);
                overviews.Add(Fetch.posterSet.results[i].overview);
                movieIDS.Add(Fetch.posterSet.results[i].id.ToString());
            }
        }
        
        if(Fetch.topMovieSet.results.Count <= MAX_RESULTS) {
            foreach(TopMovie topMovie in Fetch.topMovieSet.results) {
                posterURLSTops.Add(topMovie.poster_path);
                overviewsTops.Add(topMovie.overview);
                movieIDSTops.Add(topMovie.id.ToString());
            }
        }
        else {
            for(int i = 0; i < MAX_RESULTS; i++) {
                posterURLSTops.Add(Fetch.topMovieSet.results[i].poster_path);
                overviewsTops.Add(Fetch.topMovieSet.results[i].overview);
                movieIDSTops.Add(Fetch.topMovieSet.results[i].id.ToString());
            }
        }
        
    }

    public async Task OnPostSearch(string search) {
        await Fetch.Search(search);
        foreach(Poster poster in Fetch.posterSet.results) {
            posterURLS.Add(poster.poster_path);
            overviews.Add(poster.overview);
            movieIDS.Add(poster.id.ToString());
        }
    }

    public void OnPostDetails(string movieID) {
        // redirect to a details page
        // putting the movieID in the URL
        Response.Redirect("./Movie?id=" + movieID);
    }
}