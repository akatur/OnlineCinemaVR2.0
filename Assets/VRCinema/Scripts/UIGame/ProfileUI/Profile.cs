using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Profile  
{
    [JsonProperty("user_id")]
    public string userId;
    [JsonProperty("username")]
    public string username;
    [JsonProperty("user_photo")]
    public string userPhoto;

    public Profile(string userId, string username, string userPhoto)
    {
        this.userId = userId;
        this.username = username;
        this.userPhoto = userPhoto;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static Profile FromJson(string json)
    {
        return JsonUtility.FromJson<Profile>(json);
    }
}
