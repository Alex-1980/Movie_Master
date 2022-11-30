using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieMaster.API;

namespace MovieMaster.Pages;

public class ActorModel : PageModel { 

    public string profile_pic = "https://image.tmdb.org/t/p/w500";
    public string name = "";
    public string bio = "";
    public string birth = "";
    public int age = 0;
    public string birthPlace = "";
    public string homePage = "";
    public string twitterID = "";
    public string facebookID = "";
    public string instagramID = "";
    public List<string> posterURLS = new List<string>();
    public List<string> profileURLS = new List<string>();

    public async Task OnGet(string actorID) {
        await Fetch.ActorDetails(actorID);

        // Actor Details
        profile_pic = profile_pic + Fetch.actor.profile_path;
        name = Fetch.actor.name;
        bio = Fetch.actor.biography;
        birthPlace = Fetch.actor.place_of_birth;
        homePage = Fetch.actor.homepage;
        twitterID = Fetch.external.twitter_id;
        facebookID = Fetch.external.facebook_id;
        instagramID = Fetch.external.instagram_id;

        birth = Fetch.actor.birthday;
        int yearBorn = Convert.ToInt16(birth.Substring(0, 4));
        DateTime date = DateTime.Now;
        int year = date.Year;
        age = year - yearBorn;

        foreach(MovieCast movieCast in Fetch.movieCredit.cast) {
            posterURLS.Add(movieCast.poster_path);
        }

        foreach(Profile profile in Fetch.actorImages.profiles) {
            profileURLS.Add(profile.file_path);
        }
    }

}