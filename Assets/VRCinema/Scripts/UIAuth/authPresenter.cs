using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AuthPresenter : MonoBehaviour
{


    [SerializeField] private Button btnLogin;

    [SerializeField] private Button btnReg;


    [SerializeField] private GameObject UILogin;

    [SerializeField] private GameObject UIRegistration;

    //LoginImputs
    [SerializeField] private TMP_InputField logInputLogin;

    [SerializeField] private TMP_InputField logInputPsw;

    //RegistrationImputs
    [SerializeField] private TMP_InputField regInputNickName;
   

    [SerializeField] private TMP_InputField regInputLogin;

    [SerializeField] private TMP_InputField regInputPsw;


    [SerializeField] private Button LOGUserButton;

    [SerializeField] private Button REGUserButton;

    [SerializeField] private UnityEvent events;


    //AddMovieInput

    [SerializeField] private TMP_InputField NameMovieInput;
    [SerializeField] private TMP_InputField GenreInput;
    [SerializeField] private TMP_InputField UrlMovieInput;
    [SerializeField] private TMP_InputField UrlPhotoInput;
    [SerializeField] private TMP_InputField MovieDiscriptionInput;

    [SerializeField] private Button MovieAddButton;

    // ������ �� ������ 
    [Header("Scripts")]
    [SerializeField] private authModel authModel;

    //[SerializeField] private CardsControllerModel cardsControllerModel;

    private void Awake()
    {

        btnLogin.onClick.AddListener(Authorization);
        btnReg.onClick.AddListener(Registation);
        LOGUserButton.onClick.AddListener(SendDataAuth);
        REGUserButton.onClick.AddListener(SendDataReg);
        MovieAddButton.onClick.AddListener(SendDataNewMovie);
    }

    private void Authorization()
    {
        UILogin.SetActive(true);
        UIRegistration.SetActive(false);
    }

    private void Registation()
    {
        UILogin.SetActive(false);
        UIRegistration.SetActive(true);
    }

    private void OnDestroy()
    {
        btnLogin.onClick.RemoveListener(Authorization);
        btnReg.onClick.RemoveListener(Registation);
    }

    public void SendDataAuth()
    {
        string login = logInputLogin.text.Trim();
        string password = logInputPsw.text.Trim(); 
        authModel.LoginUser(login, password);
        
    }

    public void SendDataReg()
    {
        string login = regInputLogin.text.Trim();
        string password = regInputPsw.text.Trim();
        string nickname = regInputNickName.text.Trim();
        authModel.RegUser(login, password, nickname);
    }
 
    public void SendDataNewMovie()
    {
        string namemovie = NameMovieInput.text.Trim();
        string genre = GenreInput.text.Trim();
        string urlMovie = UrlMovieInput.text.Trim();
        string photoUrlMovie = UrlPhotoInput.text.Trim();
        string discription = MovieDiscriptionInput.text.Trim();
        authModel.AddMovie(namemovie, genre, urlMovie, photoUrlMovie, discription);
    }


    //public void SendDataNewMovieLike()
    //{
    //    string namemovie = NameMovieInput.text.Trim();
    //    //string photoUrlMovie = UrlPhotoInput.text.Trim();

    //    cardsControllerModel.LoadingCardsLikes();
    //}
}
