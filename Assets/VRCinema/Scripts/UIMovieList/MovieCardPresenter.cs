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

    [SerializeField] private Button btnDeleteLike;
    [SerializeField] private Button btnDeleteFavorit;

    public Text movieTitle;
    public Image PosterMovie;
    public string urlPhotoName;

    public MovieCards movie;
    public event Action <MovieCardPresenter> OnButtonFavorClick;
    public event Action <MovieCardPresenter> OnButtonLikeClick;

    public event Action <MovieCardPresenter> OnButtonDeleteLikeClick;
    public event Action <MovieCardPresenter> OnButtonDeleteFavorClick;


    //public event Action <MovieCardPresenter> OnButtonSelectMovie;



    public void Init(MovieCards movie)
    {
        movieTitle.text = movie.movieTitle;
        this.movie = movie;

        //if (btnFavorit != null)
        //{
        //    btnFavorit.onClick.AddListener(ButtonFavClick);
        //}
        if (btnLike != null)
        {
            btnLike.onClick.AddListener(ButtonLikeClick);
        }
        if (btnFavorit != null)
        {
            btnFavorit.onClick.AddListener(ButtonFavClick);
        }


        if (btnDeleteLike != null)
        {
            btnDeleteLike.onClick.AddListener(ButtonDeleteLikeClick);
        }
        if (btnDeleteFavorit != null)
        {
            btnDeleteFavorit.onClick.AddListener(ButtonDeleteFavClick);
        }



        //StartCoroutine(LoadImageFromURL(movie.urlPhotoName));
    }

    public void ButtonDeleteLikeClick()
    {
        OnButtonDeleteLikeClick?.Invoke(this);
        Destroy(this.gameObject);
    }

    public void ButtonDeleteFavClick()
    {
        OnButtonDeleteFavorClick?.Invoke(this);
        Destroy(this.gameObject);
    }


    public void ButtonFavClick()
    {
        OnButtonFavorClick?.Invoke(this);
    } 
    public void ButtonLikeClick()
    {
        OnButtonLikeClick?.Invoke(this);
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
