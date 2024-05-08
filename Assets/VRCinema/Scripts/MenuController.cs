using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameObject UIMenu;
    [SerializeField] private GameObject UISettings;

    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnBack;

    private GameObject activeWindow = null;


    private void Awake()
    {
        btnSettings.onClick.AddListener(StateWindowUISettings);
        btnNext.onClick.AddListener(StateWindowUIMenu);
        btnBack.onClick.AddListener(StateWindowCloseUISettings);



        //UIMenu.gameObject.SetActive(false);
        //btnBack.gameObject.SetActive(false);
    }

    private void StateWindowUISettings()
    {
        UIMenu.SetActive(false);
        UISettings.SetActive(true);
    }

    private void StateWindowUIMenu()
    {
        UIMenu.SetActive(false);
        UISettings.SetActive(false);
    }

    private void StateWindowCloseUISettings()
    {
        UIMenu.SetActive(true);
        UISettings.SetActive(false);
    }

}

