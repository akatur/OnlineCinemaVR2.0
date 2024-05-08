using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MenuUiControll : MonoBehaviour
{
    [SerializeField] private GameObject UISrollCards;
    [SerializeField] private GameObject TopTable;
    [SerializeField] private GameObject UIAddMovie;
    [SerializeField] private  GameObject UIProfile;
    [SerializeField] private  GameObject UIFav;
    [SerializeField] private  GameObject UILike;
    [SerializeField] private  GameObject UIWatch;

    [SerializeField] private  GameObject UIMenu;
    [SerializeField] private  GameObject UISettings;

    public bool isOpenedOne;
    public bool isOpenedTwo;
    public bool isOpenedTree;

    [SerializeField] private authModel authModel;

    void Start()
    {
        Cursor.visible = true;
        UISrollCards.SetActive(false);
        TopTable.SetActive(false);
        UIAddMovie.SetActive(false);
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpenedOne = !isOpenedOne;

            if (isOpenedOne)
            {
                UISrollCards.SetActive(true);
                TopTable.SetActive(true);
            }
            else
            {
                UISrollCards.SetActive(false);
                TopTable.SetActive(false);
                UIProfile.SetActive(false);
                UIFav.SetActive(false);
                UILike.SetActive(false);
                UIWatch.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpenedTree = !isOpenedTree;

            if (isOpenedTree)
            {

                UIMenu.SetActive(true);
                UISrollCards.SetActive(false);
                TopTable.SetActive(false);
                UIProfile.SetActive(false);
                UIFav.SetActive(false);
                UILike.SetActive(false);
                UIWatch.SetActive(false);
            }
            else
            {
                UIMenu.SetActive(false);
                UISettings.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (UserInfo.currentLogin == "admin")
            {
                isOpenedTwo = !isOpenedTwo;
                if (isOpenedTwo)
                {
                    UIAddMovie.SetActive(true);
                }
                else
                {
                    UIAddMovie.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Не админ");
            }
        }


    }
}
