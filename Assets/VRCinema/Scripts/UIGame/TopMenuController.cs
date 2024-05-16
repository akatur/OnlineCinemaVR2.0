using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TopMenuController : MonoBehaviour
{
    [SerializeField] private Button btnLike;
    [SerializeField] private Button btnFavourite;
    [SerializeField] private Button btnWatched;
    [SerializeField] private Button btnProfile;
    [SerializeField] private Button btnBackPan;

    [SerializeField] private Button btnAccept;
    [SerializeField] private Button btnSearch;
    [SerializeField] private TMP_InputField imputSearch;

    [SerializeField] private TMP_Dropdown genreDropdown;

    //[SerializeField] private Button btnPanoram;


    [SerializeField] private Button btnBack;

    [SerializeField] private GameObject UIScrollCards;
    [SerializeField] private GameObject UILikes;
    [SerializeField] private GameObject UIFavouritesList;
    [SerializeField] private GameObject UIWatchedList;
    [SerializeField] private GameObject UIProfile;
    [SerializeField] private GameObject UIPan;

    [SerializeField] private GameObject VideoPlayerController;

    
    //public event Action<MovieCards> OnButtonLikeClick;

    private GameObject activeWindow = null;

    [SerializeField] private CardsControllerModel cardsControllerModel;
    [SerializeField] private ProfileModel profileModel;

    CardControllerPresenter cardsControllerPresenter;

    private void Awake()
    {
        //btnLike.interactable = false;
        //btnFavourite.interactable = false;
        //btnWatched.interactable = false;
        //btnBack.interactable = false;

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

        btnBackPan.onClick.AddListener(StateWindowBack);

        btnAccept.onClick.AddListener(StateAccept);

        btnProfile.onClick.AddListener(profileModel.invokeProfile);

        imputSearch.gameObject.SetActive(true);
        btnSearch.gameObject.SetActive(true);
        btnSearch.onClick.AddListener(SendDataSearch);


        //btnPanoram.onClick.AddListener(StateWindowUIPanoram);
        btnBack.onClick.AddListener(Back);
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
        imputSearch.gameObject.SetActive(true);
        btnSearch.gameObject.SetActive(true);
        genreDropdown.gameObject.SetActive(true);
        btnAccept.gameObject.SetActive(true);
    }

    private void StateWindowBack()
    {
        UIPan.SetActive(false);
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
            //btnLike.interactable = true;
            //btnBack.interactable = true;
            //btnFavourite.interactable = true;
            //btnWatched.interactable = true;
            imputSearch.gameObject.SetActive(false);
            btnSearch.gameObject.SetActive(false);
            genreDropdown.gameObject.SetActive(false);
            btnAccept.gameObject.SetActive(false);
        }
        else
        {
            genreDropdown.gameObject.SetActive(true);
            imputSearch.gameObject.SetActive(true);
            btnSearch.gameObject.SetActive(true);
            btnAccept.gameObject.SetActive(true);
            ToggleButtons(false);
            //btnLike.interactable = false;
            //btnBack.interactable = false;
            //btnFavourite.interactable = false;
            //btnWatched.interactable = false;
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
            genreDropdown.gameObject.SetActive(true);
            imputSearch.gameObject.SetActive(true);
            btnSearch.gameObject.SetActive(true);
            btnAccept.gameObject.SetActive(true);
            //btnBack.interactable = false;
            //btnLike.interactable = false;
            //btnFavourite.interactable = false;
            //btnWatched.interactable = false;
            return;
        }
        window.SetActive(true);
        activeWindow = window;
    }

    public void SendDataSearch()
    {
        string searchTerm = imputSearch.text.Trim();

        if (searchTerm != "")
        {
            StartCoroutine(cardsControllerModel.GetMoviesToSearchFromServer(searchTerm));
        }
        else
        {
            StartCoroutine(cardsControllerModel.GetMoviesFromServer());
            Debug.Log("������� � ������ ������");
        }
    }

    public void StateAccept()
    {
        string genreTerm = genreDropdown.captionText.text;
        Debug.Log(genreTerm);
        if (genreTerm != "")
        {
            StartCoroutine(cardsControllerModel.GetMoviesToGenreshFromServer(genreTerm));
        }
        else
        {
            StartCoroutine(cardsControllerModel.GetMoviesFromServer());
            Debug.Log("������� � ������ ������");
        }
    }
}
