using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class MovieCards 
{
    [JsonProperty("title")]
    public string movieTitle;
    [JsonProperty("movie_photo")]
    public string urlPhotoName;
    [JsonProperty("genres")]
    public string genre;
    [JsonProperty("url_move")]
    public string movieURL;
    [JsonProperty("discription_movie")]
    public string discription;
    [JsonProperty("movie_id")]
    public string movieId;
    [JsonProperty("like_id")]
    public string likeId;
    [JsonProperty("user_id")]
    public string userId;
    [JsonProperty("favorite_id")]
    public string favoriteId;
    [JsonProperty("watched_id")]
    public string watchedId;

    public MovieCards(string movieTitle, string userId, string watchedId, string favoriteId,string genre, string discription,string urlPhotoName,  string movieURL, string movieId, string likeId)
    {
        this.movieTitle = movieTitle;
        this.urlPhotoName = urlPhotoName;
        this.discription = discription;
        this.genre = genre;
        this.movieURL = movieURL;
        this.movieId = movieId;
        this.likeId = likeId;
        this.userId = userId;
        this.favoriteId = favoriteId;
        this.watchedId = watchedId;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static MovieCards FromJson(string json)
    {
        return JsonUtility.FromJson<MovieCards>(json);
    }
}