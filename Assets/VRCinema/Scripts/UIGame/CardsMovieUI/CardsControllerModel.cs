using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Linq;

public class CardsControllerModel : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    public event Action OnInsertAllMovies;
    public event Action OnInsertLikes;
    public event Action OnInsertFav;
    public event Action OnInsertWatch;
    public event Action OnInsertToPanoram;

    public List<MovieCards> MovieList = new();
    public List<MovieCards> LikeList = new();
    public List<MovieCards> FavouritesList = new();
    public List<MovieCards> WatchedList = new();
    public List<MovieCards> ToPanoram = new();

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

    private void Start()
    {
        GetMovie();
    }

    public void GetMovie()
    {
        StartCoroutine(GetMoviesFromServer());
    }
   

    public IEnumerator GetMoviesFromServer()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/getmovie");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;

            MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);
            MovieList.Clear();
            foreach (var movie in movies)
            {
                string movieId = movie.movieId;
                if (MovieList.Any(existingMovie => existingMovie.movieId == movieId))
                {
                    continue;
                }
                string movieTitle = movie.movieTitle;
                string genre = movie.genre;
                string movieURL = movie.movieURL;
                string urlPhotoName = movie.urlPhotoName;
                string description = movie.discription;
                string likeId = movie.likeId;
                string favoriteId = movie.favoriteId;
                string watchedId = movie.watchedId;

                MovieCards movieCard = new MovieCards(movieTitle, likeId, watchedId, favoriteId, genre, description, urlPhotoName, movieURL, movieId, likeId);
                MovieList.Add(movieCard);
            }
            OnInsertAllMovies?.Invoke();
        }
        else
        {
            Debug.LogError("������ ��������� ������ ������ " + www.error);
        }

        www.Dispose();
        StopCoroutine(GetMoviesFromServer());
    }

    public static IEnumerator LoadImageFromURL(string url, Image image)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                image.sprite = sprite;
            }
            else
            {
                Debug.LogError("������ �������� �����������: " + www.error);
            }
        }
    }

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
        LikeList.Clear();

        foreach (var movie in movies)
        {
            string movieId = movie.movieId;
            if (LikeList.Any(existingMovie => existingMovie.movieId == movieId))
            {
                continue;
            }
            string movieTitle = movie.movieTitle;
            string genre = movie.genre;
            string movieURL = movie.movieURL;
            string urlPhotoName = movie.urlPhotoName;
            string description = movie.discription;
            string userId = UserInfo.user_id;
            string likeId = movie.likeId;
            string favoriteId = movie.favoriteId;
            string watchedId = movie.watchedId;

            Debug.Log($"Title: {movieTitle}, Genre: {genre}, URL: {movieURL}, Photo: {urlPhotoName}, Description: {description}, ID: {movieId}");
            MovieCards movieCard = new MovieCards(movieTitle, userId, watchedId, favoriteId, genre, description, urlPhotoName, movieURL, movieId, likeId);
            LikeList.Add(movieCard);
        }
        OnInsertLikes?.Invoke();
    }

    public void invokeFavCards()
    {
        string userId = UserInfo.user_id;
        Debug.Log(userId);
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/datafavorite?user_id=" + userId);

        StartCoroutine(ProcessRequestFav(www));
    }

    private IEnumerator ProcessRequestFav(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            ParseAndSortDataFav(json);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }

    private void ParseAndSortDataFav(string json)
    {
        MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);
        Debug.Log(json + " fav");

        foreach (var movie in movies)
        {
            string movieId = movie.movieId;
            if (FavouritesList.Any(existingMovie => existingMovie.movieId == movieId))
            {
                continue;
            }
            string movieTitle = movie.movieTitle;
            string genre = movie.genre;
            string movieURL = movie.movieURL;
            string urlPhotoName = movie.urlPhotoName;
            string description = movie.discription;
            string userId = UserInfo.user_id;
            string likeId = movie.likeId;
            string favoriteId = movie.favoriteId;
            string watchedId = movie.watchedId;


            Debug.Log($"Title: {movieTitle}, Genre: {genre}, URL: {movieURL}, Photo: {urlPhotoName}, Description: {description}, ID: {movieId}");
            MovieCards movieCard = new MovieCards(movieTitle, userId, watchedId, favoriteId, genre, description, urlPhotoName, movieURL, movieId, likeId);
            FavouritesList.Add(movieCard);
        }
        OnInsertFav?.Invoke();
    }

    public void invokeWatchCards()
    {
        string userId = UserInfo.user_id;
        Debug.Log(userId);
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/history?user_id=" + userId);

        StartCoroutine(ProcessRequestWatch(www));
    }

    private IEnumerator ProcessRequestWatch(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            ParseAndSortDataWatch(json);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }

    private void ParseAndSortDataWatch(string json)
    {
        MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);
        Debug.Log(json + " fav");

        foreach (var movie in movies)
        {
            string movieId = movie.movieId;
            if (WatchedList.Any(existingMovie => existingMovie.movieId == movieId))
            {
                continue;
            }
            string movieTitle = movie.movieTitle;
            string genre = movie.genre;
            string movieURL = movie.movieURL;
            string urlPhotoName = movie.urlPhotoName;
            string description = movie.discription;
            string userId = UserInfo.user_id;
            string likeId = movie.likeId;
            string favoriteId = movie.favoriteId;
            string watchedId = movie.watchedId;

            Debug.Log($"Title: {movieTitle}, Genre: {genre}, URL: {movieURL}, Photo: {urlPhotoName}, Description: {description}, ID: {movieId}");
            MovieCards movieCard = new MovieCards(movieTitle, userId, watchedId, favoriteId, genre, description, urlPhotoName, movieURL, movieId, likeId);
            WatchedList.Add(movieCard);
            Debug.Log(movieCard);
        }
        OnInsertWatch?.Invoke();
    }


    public void AddToLike(MovieCards movie)
    {
        StartCoroutine(AddToLikeCoroutine(Convert.ToInt32(movie.movieId), movie.movieTitle));
    }

    private IEnumerator AddToLikeCoroutine(int movieId, string movieTitle)
    {
        WWWForm form = new WWWForm();
        form.AddField("movieId", movieId.ToString());
        form.AddField("userId", UserInfo.user_id.ToString());
        form.AddField("title", movieTitle);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/addtolikes", form))
        {
            yield return www.SendWebRequest();
            try
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string response = www.downloadHandler.text;

                    Debug.Log("����� �������� � like: " + response);
                }
                else
                {
                    Debug.LogError("������ ��� ���������� ������ � like: " + www.error);
                }
            }
            catch (System.Exception er)
            {

                Debug.LogError("������ ��������� ��������� �������: " + er.Message);
            }
        }
        StopCoroutine(AddToLikeCoroutine(movieId, movieTitle));
    }

    public void GetMovieForPanoram(MovieCards movie)
    {
        StartCoroutine(GetMoviesForPanoramFromServer(Convert.ToInt32(movie.movieId)));
    }


    public IEnumerator GetMoviesForPanoramFromServer(int movieId)
    {
        string url = "http://localhost:3000/getmovieforpanoram?movie_id=" + movieId;


        string moviead = Convert.ToString(movieId);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string json = www.downloadHandler.text;

                MovieCards[] movies = JsonConvert.DeserializeObject<MovieCards[]>(json);
                ToPanoram.Clear();
                foreach (var movie in movies)
                {
                     moviead = movie.movieId;
                    if (ToPanoram.Any(existingMovie => existingMovie.movieId == moviead))
                    {
                        continue;
                    }



                    string movieTitle = movie.movieTitle;
                    string genre = movie.genre;
                    string movieURL = movie.movieURL;
                    string urlPhotoName = movie.urlPhotoName;
                    string description = movie.discription;
                    string likeId = movie.likeId;
                    string favoriteId = movie.favoriteId;
                    string watchedId = movie.watchedId;

                    MovieCards movieCard = new MovieCards(movieTitle, likeId, watchedId, favoriteId, genre, description, urlPhotoName, movieURL, moviead, likeId);
                    ToPanoram.Add(movieCard);
                }
                OnInsertToPanoram?.Invoke();
            }
            else
            {
                Debug.LogError("������ ��������� ������ ������ " + www.error);
            }
        }
    }







    public void AddToFavorites(MovieCards movie)
    {
        StartCoroutine(AddFavorite(Convert.ToInt32(movie.movieId), movie.movieTitle));
    }

    IEnumerator AddFavorite(int movieId, string movieTitle)
    {
        WWWForm form = new WWWForm();
        form.AddField("movieId", movieId.ToString());
        form.AddField("userId", UserInfo.user_id.ToString());
        form.AddField("title", movieTitle);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/addtofavorites", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("����� �������� � favorites.");
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
        StopCoroutine(AddFavorite(movieId, movieTitle));
    }

    public void AddTWatched(MovieCards movie)
    {
        StartCoroutine(AddTWatch(Convert.ToInt32(movie.movieId), movie.movieTitle));
    }

    IEnumerator AddTWatch(int movieId, string movieTitle)
    {
        WWWForm form = new WWWForm();
        form.AddField("movieId", movieId.ToString());
        form.AddField("userId", UserInfo.user_id.ToString());
        form.AddField("title", movieTitle);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/addtowatched", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("����� �������� � watched.");
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
        StopCoroutine(AddTWatch(movieId, movieTitle));
    }

    public void OnButtonClickDeleteLike(MovieCards movie)
    {
        StartCoroutine(DeleteLikesOnServer(movie));
    }

    private IEnumerator DeleteLikesOnServer(MovieCards movie)
    {
        string userId = UserInfo.user_id;
        int likeId = Convert.ToInt32(movie.likeId);
        string title = movie.movieTitle;

        Debug.Log("userId " + userId);
        Debug.Log("movieId " + likeId);

        string url = $"http://localhost:3000/likesDelete?user_id={userId}&like_id={likeId}";
        UnityWebRequest requests = UnityWebRequest.Delete(url);

        yield return requests.SendWebRequest();

        if (requests.result == UnityWebRequest.Result.Success)
        {
            uniqueTitlesInt.Remove(title);
            LikeList.Remove(movie);
            Debug.Log(userId + "  " + likeId + "�������");
        }
        else
        {
            Debug.LogError(requests.error);
        }
        StopCoroutine(DeleteLikesOnServer(movie));
    }

    public void OnButtonClickDeleteFav(MovieCards movie)
    {
        StartCoroutine(DeleteFavOnServer(movie));
    }

    private IEnumerator DeleteFavOnServer(MovieCards movie)
    {
        string userId = UserInfo.user_id;
        int favoriteId = Convert.ToInt32(movie.favoriteId);
        string title = movie.movieTitle;

        Debug.Log("userId " + userId);
        Debug.Log("movieId " + favoriteId);

        string url = $"http://localhost:3000/favDelete?user_id={userId}&favorite_id={favoriteId}";
        UnityWebRequest request = UnityWebRequest.Delete(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            uniqueTitlesInt.Remove(title);
            FavouritesList.Remove(movie);
            Debug.Log(userId + "  " + favoriteId + "�������");
        }
        else
        {
            Debug.LogError(request.error);
        }
        StopCoroutine(DeleteFavOnServer(movie));
    }

    public void OnButtonClickDeleteWatch(MovieCards movie)
    {
        StartCoroutine(DeleteWatchOnServer(movie));
    }

    private IEnumerator DeleteWatchOnServer(MovieCards movie)
    {
        string userId = UserInfo.user_id;
        int watchedId = Convert.ToInt32(movie.watchedId);
        string title = movie.movieTitle;

        Debug.Log("userId " + userId);
        Debug.Log("movieId " + watchedId);

        string url = $"http://localhost:3000/hisDelete?user_id={userId}&watched_id={watchedId}";
        UnityWebRequest request = UnityWebRequest.Delete(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            uniqueTitlesInt.Remove(title);
            WatchedList.Remove(movie);
            Debug.Log(userId + "  " + watchedId + "�������");
        }
        else
        {
            Debug.LogError(request.error);
        }
        StopCoroutine(DeleteWatchOnServer(movie));
    }

    public void PlayMovie(MovieCards movie)
    {
        videoPlayer.url = movie.movieURL;
        int watchedId = Convert.ToInt32(movie.movieId);
        videoPlayer.Play();
    }
}