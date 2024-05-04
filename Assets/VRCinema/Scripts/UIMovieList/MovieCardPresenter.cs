using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MovieCardPresenter : MonoBehaviour
{

    [SerializeField] private Button btnFavorit;
    [SerializeField] private Button btnLike;

    public Text movieTitle;
    public Image PosterMovie;
    public string urlPhotoName;


    public MovieCards   movie;

    public event Action <MovieCards> OnButtonFavorClick;
    public event Action <MovieCards> OnButtonLikeClick;

    public void Init(MovieCards movie)
    {
        movieTitle.text = movie.movieTitle;
        this.movie = movie;

        //StartCoroutine(LoadImageFromURL(movie.urlPhotoName));


        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //btnFavorit.onClick.AddListener(ButtonFavClick);
        //btnLike.onClick.AddListener(ButtonLikeClick);
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    //IEnumerator LoadImageFromURL(string urlPhotoName)
    //{
    //    UnityWebRequest www = UnityWebRequestTexture.GetTexture(urlPhotoName);
    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.Success)
    //    {
    //        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    //        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    //        PosterMovie.sprite = sprite;
    //    }
    //    else
    //    {
    //        Debug.Log("Failed to load image: " + www.error);
    //    }
    //}


   

}
