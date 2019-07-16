using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class RestUtil : MonoBehaviour  {
    public static RestUtil _instance;
    public static string LOGIN_SERVICE_URI = "http://localhost:7101/user/";
    public static string JOB_SERVICE_URI = "http://localhost:7102/jobs/";
    public static string PLAYER_SERVICE_URI = "http://localhost:7103/player/";
    public static string HERO_SERVICE_URI = "http://localhost:7104/hero/";
    public static string WORLD_SERVICE_URI = "http://localhost:7105/world/";
    public static string GAME_DATA_URI = "http://localhost:7106/gamedata/";
    public static string ENEMY_SERVICE_URI = "http://localhost:7108/enemy/";
    public static string GAME_SERVER_URI = "http://localhost:7110/game/";
    public static string COMBAT_SERVER_URI = "http://localhost:7112/combat/";
    public static string COMBAT_WEBSOCKET = "ws://localhost:7112/combatsocket";

    public static RestUtil Instance { get { return _instance;  } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
            Debug.Log("Rest Util initialized.");
        }
    }

    public static string fixJson(string type, string value)
    {
        value = "{\"" + type + "\":" + value + "}";
        return value;
    }

    public string Get(string url)
    {
        Debug.Log("Sending request to " + url);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Debug.Log("Response received: " + response.StatusCode);
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        return jsonResponse;
    }

    public WWW Post(string url, string json)
    {
        WWW www;
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        // convert json string to bytes
        var jsonData = System.Text.Encoding.UTF8.GetBytes(json);
        Debug.Log("www request being sent to server now.");
        www = new WWW(url, jsonData, postHeader);

        StartCoroutine(WaitForRequest(www));
        return www;
    }

    IEnumerator WaitForRequest(WWW data)
    {

        WaitForSeconds w;
        while (!data.isDone)
        {
            w = new WaitForSeconds(0.1f);
        }

        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            Debug.Log("WWW Request: " + data.text);
        }

        return data;
    }
}
