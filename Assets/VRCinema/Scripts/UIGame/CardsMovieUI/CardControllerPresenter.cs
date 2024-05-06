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

public class CardControllerPresenter : MonoBehaviour
{
    [SerializeField] private List<MovieCardPresenter> cardListMovies = new List<MovieCardPresenter>();
    [SerializeField] private List<MovieCardPresenter> cardListLikes = new List<MovieCardPresenter>();
    [SerializeField] private List<MovieCardPresenter> cardListFav = new List<MovieCardPresenter>();
    [SerializeField] private List<MovieCardPresenter> cardListWatch = new List<MovieCardPresenter>();

    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private MovieCardPresenter btnCard;

    [SerializeField] private MovieCardPresenter btnCardFav;

 

    [SerializeField] private GameObject[] btnFavorit;
    [SerializeField] private GameObject[] btnLike;

    [SerializeField] private Button btnLikes;

    [SerializeField] private GameObject[] btnGen;

    [SerializeField] private CardsControllerModel cardsControllerModel;

    [SerializeField] private Transform PanelCards;
    [SerializeField] private Transform PanelCardsFav;

    //likes
    [SerializeField] private MovieCardPresenter btnCardLike;
    [SerializeField] private Transform PanelCardsLike;

    //watched
    [SerializeField] private MovieCardPresenter btnCardWatched;
    [SerializeField] private Transform PanelCardsWatched;

    CardsControllerModel CardsControllerModel;




    private void Start()
    {
        LoadingCards();
        cardsControllerModel.OnInsertLikes += InstCardsLikes;
        cardsControllerModel.OnInsertFav += InstCardsFav;
        cardsControllerModel.OnInsertWatch += InstCardsWatch;
        cardsControllerModel.OnInsertAllMovies += LoadingCards;

        //cardsControllerModel.OnInsertLikes += InstCardsLikes;
        //cardsControllerModel.OnInsertFav += InstCardsFav;
        //cardsControllerModel.OnInsertWatch += InstCardsWatch;

        //foreach (var item in cardsControllerModel.MovieList)
        //{
        //    MovieCardPresenter movieCard ;
        //    movieCard = Instantiate(btnCard, Vector3.zero, Quaternion.identity, PanelCards);
        //    movieCard.Init(item);
        //    cardListMovies.Add(movieCard);

        //    movieCard.OnButtonFavorClick += AddToFavorites;

        //    movieCard.OnButtonLikeClick += AddToLikes;
        //}


        //if (cardButton != null)
        //{
        //    cardButton.onClick.AddListener(() => PlayMovie(movieURL, movieId, movieTitle));
        //    cardButton.onClick.AddListener(() => AddToWatched(movieId, movieTitle));
        //}
    }



    public void LoadingCards()
    {
        foreach (var item in cardListMovies)
        {
            Destroy(item.gameObject);
        }
       
        foreach (var item in cardsControllerModel.MovieList)
        {
            MovieCardPresenter movieCard;
            movieCard = Instantiate(btnCard, Vector3.zero, Quaternion.identity, PanelCards);
            movieCard.Init(item);
            cardListMovies.Add(movieCard);
            movieCard.OnButtonFavorClick += AddToFavorites;
            movieCard.OnButtonLikeClick += AddToLikes;
        }
    }



    private void AddToLikes(MovieCardPresenter movieCardPresenter)
    {
        cardsControllerModel.AddToLike(movieCardPresenter.movie);
        cardListLikes.Add(movieCardPresenter);
    }

    private void AddToFavorites(MovieCardPresenter movieCardPresenter)
    {
        cardsControllerModel.AddToFavorites(movieCardPresenter.movie);
        cardListFav.Add(movieCardPresenter);
    }


    private void InstCardsLikes()
    {
        foreach (var item in cardListLikes)
        {
            Destroy(item.gameObject);
        }
        cardListLikes.Clear();
        foreach (var item in cardsControllerModel.LikeList)
        {
            MovieCardPresenter likeCard;
            likeCard = Instantiate(btnCardLike, Vector3.zero, Quaternion.identity, PanelCardsLike);
            likeCard.Init(item);
            cardListLikes.Add(likeCard);
            likeCard.OnButtonDeleteLikeClick += OnButtonClickDeleteLikes;
        }
    }

    private void OnButtonClickDeleteLikes(MovieCardPresenter movieCardPresenter)
    {
        cardsControllerModel.OnButtonClickDeleteLike(movieCardPresenter.movie);
        cardListLikes.Remove(movieCardPresenter);
    }




    private void InstCardsFav()
    {
        foreach (var item in cardListFav)
        {
            Destroy(item.gameObject);
        }
        cardListFav.Clear();
        foreach (var item in cardsControllerModel.FavouritesList)
        {
            MovieCardPresenter likeCard;
            likeCard = Instantiate(btnCardLike, Vector3.zero, Quaternion.identity, PanelCardsFav);
            likeCard.Init(item);
            cardListFav.Add(likeCard);

            //likeCard.OnButtonDeleteLikeClick += cardsControllerModel.OnButtonClickDeleteLike;

        }
    }

    private void InstCardsWatch()
    {
        foreach (var item in cardListWatch)
        {
            Destroy(item.gameObject);
        }
        cardListWatch.Clear();
        foreach (var item in cardsControllerModel.WatchedList)
        {
            MovieCardPresenter likeCard;
            likeCard = Instantiate(btnCardLike, Vector3.zero, Quaternion.identity, PanelCardsWatched);
            likeCard.Init(item);
            cardListWatch.Add(likeCard);
            //likeCard.OnButtonDeleteLikeClick += cardsControllerModel.OnButtonClickDeleteLike;
        }
    }


    //private void PlayMovie(string movieURL, int movieId, string movieTitle)
    //{
    //    OnPlayMovie?.Invoke(movieURL, movieId, movieTitle);
    //}














}