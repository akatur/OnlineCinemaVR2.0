using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class TopMenuController : MonoBehaviour
{
    [SerializeField] private Button btnLike;
    [SerializeField] private Button btnFavourite;
    [SerializeField] private Button btnWatched;
    [SerializeField] private Button btnProfile;

    [SerializeField] private Button btnBack;

    [SerializeField] private GameObject UIScrollCards;
    [SerializeField] private GameObject UILikes;
    [SerializeField] private GameObject UIFavouritesList;
    [SerializeField] private GameObject UIWatchedList;
    [SerializeField] private GameObject UIProfile;

    public event Action<MovieCards> OnButtonLikeClick;

    private GameObject activeWindow = null;

    [SerializeField] private CardsControllerModel cardsControllerModel;
    CardControllerPresenter cardsControllerPresenter;


   
    private void Awake()
    {
        btnLike.interactable = false;
        btnFavourite.interactable = false;
        btnWatched.interactable = false;
        btnBack.interactable = false;

        btnFavourite.gameObject.SetActive(false);
        btnWatched.gameObject.SetActive(false);
        btnLike.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(false);

        btnLike.onClick.AddListener(StateWindowUILikes);
        btnLike.onClick.AddListener(cardsControllerModel.invokeLikesCards);

        btnFavourite.onClick.AddListener(StateWindowUIFavouritesList);
        btnFavourite.onClick.AddListener(cardsControllerModel.invokeFavCards);

        btnWatched.onClick.AddListener(StateWindowUIWatchedList);
        btnWatched.onClick.AddListener(cardsControllerModel.invokeWatchCards);


        btnProfile.onClick.AddListener(StateWindowUIProfile);

        
        btnBack.onClick.AddListener(Back);
        btnBack.onClick.AddListener(cardsControllerModel.GetMovie);


    }

    private void Back()
    {
        btnLike.gameObject.SetActive(false);
        btnFavourite.gameObject.SetActive(false);
        btnWatched.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(false);
        UILikes.SetActive(false);
        UIWatchedList.SetActive(false);
        UIFavouritesList.SetActive(false);
        UIProfile.SetActive(false);
    }

    private void StateWindowUILikes()
    {
        ToggleWindow(UILikes);
    }

    private void StateWindowUIFavouritesList()
    {
        ToggleWindow(UIFavouritesList);
    }

    private void StateWindowUIWatchedList()
    {
        ToggleWindow(UIWatchedList);
    }

    private void ToggleButtons(bool active)
    {
        btnBack.gameObject.SetActive(active);
        btnFavourite.gameObject.SetActive(active);
        btnWatched.gameObject.SetActive(active);
        btnLike.gameObject.SetActive(active);
    }

    private void StateWindowUIProfile()
    {
        ToggleWindow(UIProfile);
        ToggleWindow(UIScrollCards);

        if (UIProfile.activeSelf)
        {
            ToggleButtons(true);

            btnLike.interactable = true;
            btnBack.interactable = true;
            btnFavourite.interactable = true;
            btnWatched.interactable = true;
        }
        else
        {
            ToggleButtons(false);
            btnLike.interactable = false;
            btnBack.interactable = false;
            btnFavourite.interactable = false;
            btnWatched.interactable = false;
        }

    }

    private void ToggleWindow(GameObject window)
    {
        if (window.activeSelf)
        {
            return;
        }

        if (activeWindow != null)
        {
            activeWindow.SetActive(false);
        }

        if (window == UIScrollCards)
        {
            btnBack.interactable = false;
            btnLike.interactable = false;
            btnBack.interactable = false;
            btnFavourite.interactable = false;
            btnWatched.interactable = false;
            return;
        }

        window.SetActive(true);
        activeWindow = window;
    }


    private void OnLogUserButtonClick()
    {
        if (activeWindow != null)
        {
            btnBack.gameObject.SetActive(false);
            btnBack.interactable = false;
            UIScrollCards.SetActive(true);
            activeWindow.SetActive(false);
            activeWindow = null;
        }
    }
}
