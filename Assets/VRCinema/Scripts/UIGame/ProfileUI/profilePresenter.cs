using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class profilePresenter : MonoBehaviour
{
    [SerializeField] private Text login;
    [SerializeField] private Text nickName;
    [SerializeField] private Text city;
    public string userPhoto;
    public Image PosterMovie;

    [SerializeField] private Button authUser;

    public event Action<profilePresenter> OnButtonProfileClick;

    public Profile profile;
    public void Init(Profile profile)
    {
        nickName.text = profile.username;
        userPhoto = profile.userPhoto;
        login.text = profile.login;
        city.text = profile.city;
        this.profile = profile;

        Debug.Log("PhotoUser" + userPhoto);
        StartCoroutine(ProfileModel.LoadImageFromURL(userPhoto, PosterMovie));

        if (authUser != null)
        {
            authUser.onClick.AddListener(ButtonProfileClick);
        }
    }

    public void ButtonProfileClick()
    {
        OnButtonProfileClick?.Invoke(this);
    }

}