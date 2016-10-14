using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using System.Collections.Generic;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.social;
using System.Collections;

public class FBControllerScr : MonoBehaviour
{

    public GameObject LogIn;
    public GameObject LogOut;

    LevelManager levelManager;

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);      
    }

    void Start()
    {
        if(FB.IsInitialized)
        {
            SetInit();
        }

        levelManager = LevelManager.instance;

    }

    private void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            LogIn.SetActive(false);
            LogOut.SetActive(true);
        }
        else
        {
            LogIn.SetActive(true);
            LogOut.SetActive(false);
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FacebookLogin()
    {
        if(!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, HandleResult);           
        }
        else
        {
            FB.LogOut();
            LogIn.SetActive(true);
            LogOut.SetActive(false);
            PlayerPrefs.DeleteKey("highscore");
        }
      
    }

    protected void HandleResult(IResult result)
    {
        Debug.Log(result.RawResult);
        if (FB.IsLoggedIn)
        {
            // Log In
            Debug.Log("Successfully Logged in");
            LogIn.SetActive(false);
            LogOut.SetActive(true);

            // Get User named Set up Shephertz
            FB.API("/me", HttpMethod.GET, UserIdCallBack);                      
        }
    }

    public class ShephertzCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            com.shephertz.app42.paas.sdk.csharp.social.Social social = (com.shephertz.app42.paas.sdk.csharp.social.Social)response;
            Debug.Log("Shephertz Linked");
            LevelManager.instance.UpdateScore();
        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
        }
    }


    public void UserIdCallBack(IGraphResult result)
    {
        // Set User name
        levelManager.mUserName = (string)result.ResultDictionary["name"];
        levelManager.mUserId = (string)result.ResultDictionary["id"];
        Debug.Log("User name is " + levelManager.mUserName);
        Debug.Log("User id is " + levelManager.mUserId);

        // Link FB account with Shephertz
        string token = AccessToken.CurrentAccessToken.TokenString;
        levelManager.socialService.LinkUserFacebookAccount(levelManager.mUserName, token, new ShephertzCallBack());
    }

    public void SetScore(int score)
    {
        FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, null);
        if (FB.IsLoggedIn)
        {
            var scoreData = new Dictionary<string, string>() { { "score", score.ToString() } };
            FB.API("/me/scores", HttpMethod.POST, Callback, scoreData);
        }
    }

    void Callback(IGraphResult result)
    {
        Debug.Log(result.RawResult);
    }  
}

