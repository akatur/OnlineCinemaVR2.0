using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class menuUiControll : MonoBehaviour
{
    public GameObject UISrollCards;
    public GameObject TopTable;
    public GameObject UIAddMovie;

    public bool isOpenedOne;
    public bool isOpenedTwo;

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
