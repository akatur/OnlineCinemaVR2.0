using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEditor.MemoryProfiler;
using UnityEngine.Video;
using TMPro;
using System;
using Unity.VisualScripting;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.Analytics;
using System.Linq;

public class CardsControllerModel : MonoBehaviour
{

    public event Action OnInsert;


    public List<MovieCards> MovieList = new();
    public List<MovieCards> LikeList = new();

    public List<MovieCards> FavouritesList = new();
    public List<MovieCards> WatchedList = new();


    private HashSet<string> uniqueTitlesInt = new HashSet<string>();

    public class Row
    {
        [JsonProperty("rowCount")]
        public int rowCounts;

        public string ToJson()
        {
            return JsonUtility.ToJson(rowCounts);
        }
        public static Row FromJson(string json)
        {
            return JsonUtility.FromJson<Row>(json);
        }
    }

   
    

    //private string connectionString = "Server=127.0.0.1;Database=cinemabd;User ID=root;Password='';";
    private VideoPlayer videoPlayer;

   
   
    private void Start()
    {
        StartCoroutine(GetMoviesFromServer());
        //StartCoroutine(GetLikesFromServer());

        //LoadingCards();
        //LoadingLikes();
        //LoadingFavourites();
        //LoadingWatched();

    }

    private IEnumerator GetMoviesFromServer()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/getmovie");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            

            MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);
            //Debug.Log("kok1");
            //Debug.Log(movies);

            foreach (var movie in movies)
            {
                string movieTitle = movie.movieTitle;
                string genre = movie.genre;
                string movieURL = movie.movieURL;
                string urlPhotoName = movie.urlPhotoName;
                string description = movie.discription;
                string movieId = movie.movieId;
                string likeId = movie.likeId;

                //Debug.Log($"Title: {movieTitle}, Genre: {genre}, URL: {movieURL}, Photo: {urlPhotoName}, Description: {description}, ID: {movieId}");

                MovieCards movieCard = new MovieCards(movieTitle, likeId ,genre, description, urlPhotoName, movieURL, movieId, likeId);
                MovieList.Add(movieCard);

            }
        }
        else
        {
            Debug.LogError("������ getMOV " + www.error);
        }

        www.Dispose();
        StopCoroutine(GetMoviesFromServer());
    }


    //public void invokeLikesCards()
    //{
    //    StartCoroutine(GetDataFromServer());
    //}




    public void invokeLikesCards()
    {
        string userId = UserInfo.user_id;
        Debug.Log(userId);
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/likes?user_id=" + userId);

        StartCoroutine(ProcessRequest(www));
    }

    private IEnumerator ProcessRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            ParseAndSortData(json);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }

    private void ParseAndSortData(string json)
    {
        MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);
        Debug.Log(json + " like");
        //textCanvas.text = "";

        foreach (var movie in movies)
        {
            string movieId = movie.movieId;

            
            // ���������, ���� �� ����� � ����� �� ��������������� � ������
            if (LikeList.Any(existingMovie => existingMovie.movieId == movieId))
            {
                continue; // ���������� �����, ���� �� ��� ������������ � ������
            }


            string movieTitle = movie.movieTitle;
            string genre = movie.genre;
            string movieURL = movie.movieURL;
            string urlPhotoName = movie.urlPhotoName;
            string description = movie.discription;
            string userId = UserInfo.user_id;
            string likeId = movie.likeId;

            Debug.Log($"Title: {movieTitle}, Genre: {genre}, URL: {movieURL}, Photo: {urlPhotoName}, Description: {description}, ID: {movieId}");

            MovieCards movieCard = new MovieCards(movieTitle, userId, genre, description, urlPhotoName, movieURL, movieId, likeId);
            LikeList.Add(movieCard);
            Debug.Log(movieCard);
        }
        OnInsert?.Invoke();

    }
    //private IEnumerator GetLikesFromServer()
    //{
    //    string userId = UserInfo.user_id;
    //    Debug.Log(userId);
    //    UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/likes?user_id=" + userId);

    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.Success)
    //    {
    //        string json = www.downloadHandler.text;
    //        //ParseAndSortData(json);
    //        MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);

    //        foreach (var movie in movies)
    //        {
    //            string movieTitle = movie.movieTitle;
    //            string genre = movie.genre;
    //            string movieURL = movie.movieURL;
    //            string urlPhotoName = movie.urlPhotoName;
    //            string description = movie.discription;
    //            string movieId = movie.movieId;
    //            string likeId = movie.likeId;
    //            string userId = UserInfo.user_id;

    //            Debug.Log($"Title: {movieTitle}, Genre: {genre}, URL: {movieURL}, Photo: {urlPhotoName}, Description: {description}, ID: {movieId}");
    //            MovieCards movieCard = new MovieCards(movieTitle, userId, genre, description, urlPhotoName, movieURL, movieId, likeId);
    //            LikeList.Add(movieCard);
    //            Debug.Log(movieCard);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError(www.error);
    //    }
    //    //StopCoroutine(GetLikesFromServer());
    //}


    //private IEnumerator GetLikeFromServer()
    //{
    //    UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/likes");

    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.Success)
    //    {
    //        string json = www.downloadHandler.text;
    //        Debug.Log(json);

    //        MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);
    //        //Debug.Log("kok1");
    //        //Debug.Log(movies);

    //        foreach (var movie in movies)
    //        {
    //            string movieTitle = movie.movieTitle;
    //            string genre = movie.genre;
    //            string movieURL = movie.movieURL;
    //            string urlPhotoName = movie.urlPhotoName;
    //            string description = movie.discription;
    //            string movieId = movie.movieId;
    //            string likeId = movie.likeId;
    //            string userId = movie.userId;
    //            //Debug.Log($"Title: {movieTitle}, Genre: {genre}, URL: {movieURL}, Photo: {urlPhotoName}, Description: {description}, ID: {movieId}");
    //            MovieCards movieCard = new MovieCards(movieTitle, userId, genre, description, urlPhotoName, movieURL, movieId, likeId);
    //            LikeList.Add(movieCard);
    //            //Debug.Log("kok2");
    //            Debug.Log(movieCard);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("������ getMOV " + www.error);
    //    }

    //    www.Dispose();
    //    StopCoroutine(GetLikesFromServer());
    //}



    //private void LoadingLikes()
    //{
    //    connection = new MySqlConnection(connectionString);
    //    connection.Open();

    //    string sqlQuery = "SELECT ul.like_id, ul.title, m.genres, m.url_move, m.movie_id, m.movie_photo, m.discription_movie " +
    //              "FROM user_likes ul " + 
    //              "INNER JOIN movies m " + 
    //              "ON ul.movie_id = m.movie_id"; 

    //    MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
    //    MySqlDataReader reader = cmd.ExecuteReader();

    //    while (reader.Read())
    //    {
    //        string movieTitle = reader.GetString("title");
    //        int likeId = reader.GetInt32("like_id");
    //        string genre = reader.GetString("genres");
    //        string movieURL = reader.GetString("url_move");
    //        string urlPhotoName = reader.GetString("movie_photo");
    //        string discription = reader.GetString("discription_movie");
    //        int movieId = reader.GetInt32("movie_id");

    //        Movie likeCard = new Movie(urlPhotoName, genre, discription, movieTitle, movieURL, movieId);
    //        LikeList.Add(likeCard);
    //    }
    //    reader.Close();
    //    connection.Close();
    //}


    //private void LoadingFavourites()
    //{
    //    connection = new MySqlConnection(connectionString);
    //    connection.Open();
        
    //    string sqlQuery = "SELECT fav.favorite_id , fav.title, m.genres, m.url_move, m.movie_id, m.movie_photo, m.discription_movie " +
    //              "FROM favourites fav " +
    //              "INNER JOIN movies m " +
    //              "ON fav.movie_id = m.movie_id";

        

    //    MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
    //    MySqlDataReader reader = cmd.ExecuteReader();

    //    while (reader.Read())
    //    {
    //        string movieTitle = reader.GetString("title");
    //        int likeId = reader.GetInt32("favorite_id");
    //        string genre = reader.GetString("genres");
    //        string movieURL = reader.GetString("url_move");
    //        string urlPhotoName = reader.GetString("movie_photo");
    //        string discription = reader.GetString("discription_movie");
    //        int movieId = reader.GetInt32("movie_id");

    //        Movie likeCard = new Movie(urlPhotoName, genre, discription, movieTitle, movieURL, movieId);
    //        FavouritesList.Add(likeCard);
    //    }
    //    reader.Close();
    //    connection.Close();
    //}

    //private void LoadingWatched()
    //{
    //    connection = new MySqlConnection(connectionString);
    //    connection.Open();
    //    string sqlQuery = "SELECT wat.watched_id , wat.title, m.genres, m.url_move, m.movie_id, m.movie_photo, m.discription_movie " +
    //              "FROM watched_movies wat " +
    //              "INNER JOIN movies m " +
    //              "ON wat.movie_id = m.movie_id";
       
    //    MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
    //    MySqlDataReader reader = cmd.ExecuteReader();

    //    while (reader.Read())
    //    {
    //        string movieTitle = reader.GetString("title");
    //        int likeId = reader.GetInt32("favorite_id");
    //        string genre = reader.GetString("genres");
    //        string movieURL = reader.GetString("url_move");
    //        string urlPhotoName = reader.GetString("movie_photo");
    //        string discription = reader.GetString("discription_movie");
    //        int movieId = reader.GetInt32("movie_id");

    //        Movie likeCard = new Movie(urlPhotoName, genre, discription, movieTitle, movieURL, movieId);
    //        WatchedList.Add(likeCard);
    //    }
    //    reader.Close();
    //    connection.Close();
    //}

    //private void PlayMovie(string movieURL, int movieId, string movieTitle)
    //{
    //    if (videoPlayer == null)
    //    {
    //        videoPlayer = GetComponent<VideoPlayer>();
    //        if (videoPlayer == null)
    //        {
    //            Debug.LogError("���������� �� ������");
    //            return;
    //        }
    //    }
    //    videoPlayer.url = movieURL;
    //    videoPlayer.Play();
    //}

    //public void AddToFavorites(Movie movie)
    //{
    //    if (connection.State != ConnectionState.Open)
    //    {
    //        connection.Open();
    //    }
    //    string query = $"INSERT INTO favourites (movie_id, user_id, title) VALUES (@movieId, @userId, @title)";

    //    MySqlCommand command = new MySqlCommand(query, connection);

    //    command.Parameters.AddWithValue("@movieId", movie.movieId);
    //    command.Parameters.AddWithValue("@userId", UserInfo.user_id);
    //    command.Parameters.AddWithValue("@title", movie.movieTitle);
    //    try
    //    {
    //        command.ExecuteNonQuery();
    //        Debug.Log("����� �������� � ���������.");
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("��������� ������ ��� ���������� ������ � ���������: " + ex);
    //    }
    //}

    //public void AddToLike(Movie movie)
    //{
    //    if (connection.State != ConnectionState.Open)
    //    {
    //        connection.Open();
    //    }

    //    string query = $"INSERT INTO `user_likes` (movie_id, user_id, title) VALUES (@movieId, @userId, @title)";
    //    MySqlCommand command = new MySqlCommand(query, connection);

    //    command.Parameters.AddWithValue("@movieId", movie.movieId);
    //    command.Parameters.AddWithValue("@userId", UserInfo.user_id);
    //    command.Parameters.AddWithValue("@title", movie.movieTitle);

    //    try
    //    {
    //        command.ExecuteNonQuery();
    //        Debug.Log("����� �������� � like.");
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("��������� ������ ��� ���������� ������ � like: " + ex);
    //    }

    //}

    //public void AddToWatched(int movieId, string movieTitle)
    //{

    //    if (connection.State != ConnectionState.Open)
    //    {
    //        connection.Open();
    //    }

    //    string query = $"INSERT INTO `watched_movies` (movie_id, user_id, title) VALUES (@movieId, @userId, @title)";
    //    MySqlCommand command = new MySqlCommand(query, connection);

    //    command.Parameters.AddWithValue("@movieId", movieId);
    //    command.Parameters.AddWithValue("@userId", UserInfo.user_id);
    //    command.Parameters.AddWithValue("@title", movieTitle);

    //    try
    //    {
    //        command.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("������ ��� ���������� ������ � History: " + ex);
    //    }
    //}
}


//public class MovieData
//{
//    [JsonProperty("movie_photo")]
//    public string urlPhotoName;
//    [JsonProperty("genre")]
//    public string genre;
//    [JsonProperty("movie_title")]
//    public string movieTitle;
//    [JsonProperty("movieURL")]
//    public string movieURL;
//    [JsonProperty("discription")]
//    public string discription;
//    [JsonProperty("movie_id")]
//    public int movieId;

//    public string ToJson()
//    {
//        return JsonUtility.ToJson(this);
//    }
//    public static MovieData FromJson(string json)
//    {
//        return JsonUtility.FromJson<MovieData>(json);
//    }
//}
//public string ToJson()
//{
//    return JsonUtility.ToJson(this);
//}
//public static Movie FromJson(string json)
//{
//    return JsonUtility.FromJson<Movie>(json);
//}





//private void LoadingCards()
//{
//    connection = new MySqlConnection(connectionString);
//    connection.Open();

//    string sqlQuery = "SELECT m.title,m.genres, m.url_move, m.movie_id, m.movie_photo,discription_movie " +
//                          "FROM movies m ";

//    MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
//    MySqlDataReader reader = cmd.ExecuteReader();

//    while (reader.Read())
//    {
//            string movieTitle = reader.GetString("title");
//            string genre = reader.GetString("genres");
//            string movieURL = reader.GetString("url_move");
//            string urlPhotoName = reader.GetString("movie_photo");
//            string discription = reader.GetString("discription_movie");
//            int movieId = reader.GetInt32("movie_id");

//            Movie movieCard = new Movie(urlPhotoName,  genre, discription, movieTitle, movieURL,  movieId  );
//            MovieList.Add(movieCard);
//    }
//    reader.Close();
//    connection.Close();
//}

//[System.Serializable]
//public class MovieData
//{
//    [JsonProperty("movie_id")]
//    public int movieId;
//    [JsonProperty("movie_title")]
//    public string movieTitle;
//    [JsonProperty("movie_url")]
//    public string movieURL;
//    [JsonProperty("genre_name")]
//    public string genreName;

//    public string ToJson()
//    {
//        return JsonUtility.ToJson(this);
//    }
//    public static MovieData FromJson(string json)
//    {
//        return JsonUtility.FromJson<MovieData>(json);
//    }
//}
