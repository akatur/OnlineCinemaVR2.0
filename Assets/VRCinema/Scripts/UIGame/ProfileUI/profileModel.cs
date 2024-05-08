using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEditor.MemoryProfiler;
using UnityEngine.Video;
using TMPro;
using System;
using Unity.VisualScripting;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.Analytics;
using System.Linq;

public class profileModel : MonoBehaviour
{
    public List<Profile> ProfileList = new();

    private HashSet<string> uniqueTitlesInt = new HashSet<string>();

    public event Action OnInsertProfile;

    public void invokeProfile()
    {
        string userId = UserInfo.user_id;
        Debug.Log(userId);
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/likes?user_id=" + userId);

        StartCoroutine(ProcessRequest(www));
    }

    private IEnumerator ProcessRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            ParseAndSortData(json);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }

    private void ParseAndSortData(string json)
    {
        Profile[] profiles = JsonConvert.DeserializeObject<Profile[]>(json);
        Debug.Log(json + " like");
        ProfileList.Clear();

        foreach (var profile in profiles)
        {
            string userId = UserInfo.user_id;
            string username = profile.username;
            string userPhoto = profile.userPhoto;

            if (ProfileList.Any(existingUser => existingUser.userId == userId))
            {
                continue;
            }

            Debug.Log($"User: {username}");
            Profile prof = new Profile( userId, username, userPhoto);
            ProfileList.Add(prof);
        }
        OnInsertProfile?.Invoke();
    }
}
