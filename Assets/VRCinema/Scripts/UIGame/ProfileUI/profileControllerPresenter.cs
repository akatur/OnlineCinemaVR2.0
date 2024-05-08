using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class profileControllerPresenter : MonoBehaviour
{



    [SerializeField] private List<Profile> cardListProfile = new List<Profile>();
    profileModel profileModel;
    void Start()
    {
        profileModel.OnInsertProfile += LoadingProfile;
    }

    public void LoadingProfile()
    {
        foreach (var item in cardListProfile)
        {
            //Destroy(item.gameObject);
        }
        cardListProfile.Clear();
        foreach (var item in profileModel.ProfileList)
        {
            //Profile profile;
            //profile = Instantiate(btnCard, Vector3.zero, Quaternion.identity, PanelCards);
            //profile.Init(item);
            //profile.Add(profile);



            //profile.OnButtonFavorClick += AddToFavorites;
            //profile.OnButtonLikeClick += AddToLikes;
            //profile.OnButtonWatchClick += AddTWatched;
            //profile.OnButtonWatchClick += PlayMovies;
        }
    }
}
