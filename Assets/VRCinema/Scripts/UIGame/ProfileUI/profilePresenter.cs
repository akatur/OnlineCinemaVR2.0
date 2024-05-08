using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class profilePresenter : MonoBehaviour
{
    [SerializeField] private Text login;
    [SerializeField] private Text nickName;

    [SerializeField] private Button authUser;

    public event Action<profilePresenter> OnButtonProfileClick;

    Profile profile;
    public void Init(Profile profile)
    {
        nickName.text = profile.username;
        this.profile = profile;



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