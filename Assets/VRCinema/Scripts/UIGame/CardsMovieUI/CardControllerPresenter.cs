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
    [SerializeField] private List<MovieCardPresenter> cardList = new List<MovieCardPresenter>();

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
        cardsControllerModel.OnInsert += InstCardsLikes;


        foreach (var item in cardsControllerModel.MovieList)
        {
            

            MovieCardPresenter movieCard ;
            movieCard = Instantiate(btnCard, Vector3.zero, Quaternion.identity, PanelCards);
            
            movieCard.Init(item);

            //movieCard.OnButtonFavorClick += cardsControllerModel.AddToFavorites;
            //movieCard.OnButtonLikeClick += cardsControllerModel.AddToLike;
        }

       

        //foreach (var item in cardsControllerModel.FavouritesList)
        //{
        //    LikesCardsPresenter likesCard;
        //    likesCard = Instantiate(btnCardFav, Vector3.zero, Quaternion.identity, PanelCardsFav);
        //    likesCard.Init(item);
        //}

        //foreach (var item in cardsControllerModel.WatchedList)
        //{
        //    LikesCardsPresenter likeCard;
        //    likeCard = Instantiate(btnCardWatched, Vector3.zero, Quaternion.identity, PanelCardsWatched);
        //    likeCard.Init(item);
        //}


        //if (cardButton != null)
        //{
        //    cardButton.onClick.AddListener(() => PlayMovie(movieURL, movieId, movieTitle));
        //    cardButton.onClick.AddListener(() => AddToWatched(movieId, movieTitle));
        //}

    }

    private void InstCardsLikes()
    {

        foreach (var item in cardList)
        {
            Destroy(item.gameObject);
        }
        cardList.Clear();
        foreach (var item in cardsControllerModel.LikeList)
        {
            MovieCardPresenter likeCard;
            likeCard = Instantiate(btnCardLike, Vector3.zero, Quaternion.identity, PanelCardsLike);
            likeCard.Init(item);
            cardList.Add(likeCard);
        }
    }


    //private void PlayMovie(string movieURL, int movieId, string movieTitle)
    //{
    //    OnPlayMovie?.Invoke(movieURL, movieId, movieTitle);
    //}














}