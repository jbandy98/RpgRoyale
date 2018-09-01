﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour {

    public Text usernameText;
    public Text passwordText;
    public string responseText;
    public GameObject errorPanel;
    public Text errorText;
    public const string VersionText = "RPG Royale Version 0.1.2   All Rights Reserved.";
    public Text versionText;

    private void Start()
    {
        versionText.text = VersionText;
    }

    public void SendAuthentication()
    {
        Debug.Log("Send Authentication clicked.");
        User user = new User();
        user.username = usernameText.text;
        user.password = passwordText.text;

        string url = RestUtil.SERVER_URI + "user/login";
        string jsonData = JsonUtility.ToJson(user);
        Debug.Log("Sending jsonData: " + jsonData + " to " + url);
        Post(url, jsonData);

        Login login = JsonUtility.FromJson<Login>(responseText);
        if (login != null && login.status.Equals("OK"))
        {
            Debug.Log("Returned login info: " + login.ToString());
            // we have set a good login - lets set some session variables
            PlayerPrefs.SetString("user", login.user);
            PlayerPrefs.SetString("sessionId", login.sessionId);

            // now lets move into the game scene (eventually we will create a party and wait for players first)
            Debug.Log("User authenticated, going to game!");
            SceneManager.LoadScene("MainGame");
        }   else if (login != null && login.status.Equals("BADCREDENTIALS"))
        {
            errorText.text = "Authentication failed. Please verify your credentials and try again, or click create to start a new account.";
            errorPanel.SetActive(true);
        } else
        {
            errorText.text = "Error connecting to server. Please try again later.";
            errorPanel.SetActive(true);
        }     
    }

    public void CreateUserClick()
    {
        Debug.Log("Create user clicked.");
        User user = new User();
        user.username = usernameText.text;
        user.password = passwordText.text;

        string url = RestUtil.SERVER_URI + "user/create";
        string jsonData = JsonUtility.ToJson(user);
        Debug.Log("Sending jsonData: " + jsonData + " to " + url);
        Post(url, jsonData);

        CreateUserResponse response = JsonUtility.FromJson<CreateUserResponse>(responseText);

        if (response != null && response.status == "DUPLICATE")
        {
            errorText.text = "The username" + response.user + " is already in use. Please pick a different username.";
            errorPanel.SetActive(true);
        }

        else if (response != null && response.status == "SUCCESS")
        {
            errorText.text = "User " + response.user + " successfully created! Please sign in now to play.";
            errorPanel.SetActive(true);
        }
        else 
        {
            errorText.text = "An error occurred when trying to create a new user. Please try again in a few minutes.";
            errorPanel.SetActive(true);
        }
    }

    public void ErrorPanelOkClick()
    {
        errorPanel.SetActive(false);
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
        } else
        {
            responseText = data.text;
            Debug.Log("WWW Request: " + data.text);
        }

        return data;
    }

}