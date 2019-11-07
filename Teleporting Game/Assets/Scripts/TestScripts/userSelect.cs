using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class userSelect : MonoBehaviour
{
    public string URL;
    public string[] UserData;

    void Start()
    {

        StartCoroutine(GetText());
    }

    // Update is called once per frame
   IEnumerator GetText()
    {
        UnityWebRequest users = UnityWebRequest.Get(URL);

        yield return users.SendWebRequest();

        if(users.isNetworkError || users.isHttpError)
        {
            Debug.Log(users.error);
        }
        else
        {
            string userDataString = users.downloadHandler.text;
            UserData = userDataString.Split(';');

        }

        Debug.Log(GetDataText(UserData[0], "levelid:"));
       
    }

    public string GetDataText(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);

        if(value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

   
   
}
