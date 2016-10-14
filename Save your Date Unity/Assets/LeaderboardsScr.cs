using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Collections;

public class LeaderboardsScr : MonoBehaviour
{ 
    public  GameObject[] Players;
    public GameObject[] Scores;

    public GameObject[] Friends;
    public GameObject[] FriendScores;

    LevelManager levelManager;

    HighScoreCallBack highScoreCallBack;
    FriendsHighScoreCallBack friendsHighScoreCallBack;

    public void Start()
    {
        this.gameObject.SetActive(false);
        levelManager = LevelManager.instance;
        highScoreCallBack = new HighScoreCallBack();
        friendsHighScoreCallBack = new FriendsHighScoreCallBack();
        highScoreCallBack.leaderBoardScr = this;
        friendsHighScoreCallBack.leaderBoardScr = this;
    }
    
    public void DisplayLeaderBoard()
    {
        if (FB.IsLoggedIn)
        {
            GetHighScores();                     
        }
    }

    void GetHighScores()
    {
        // Global
        levelManager.scoreBoardService.GetTopNRankers(levelManager.mGlobalLeaderBoard, 10, highScoreCallBack);

        // Friends
        levelManager.scoreBoardService.GetTopNRankersFromFacebook(levelManager.mFriendsLeaderBoard, AccessToken.CurrentAccessToken.TokenString, 10, friendsHighScoreCallBack);
    }


    public class HighScoreCallBack : App42CallBack
    {
        public LeaderboardsScr leaderBoardScr;
 
        public void OnSuccess(object response)
        {
            Game game = (Game)response;          
            for (int i = 0; i < game.GetScoreList().Count; i++)
            {
                Text mPlayerText = leaderBoardScr.Players[i].GetComponent<Text>();
                Text mScoreText = leaderBoardScr.Scores[i].GetComponent<Text>();

                mPlayerText.text = game.GetScoreList()[i].GetUserName();  
                mScoreText.text = game.GetScoreList()[i].GetValue().ToString();                             
            }      

        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
        }
    }

    public class FriendsHighScoreCallBack : App42CallBack
    {
        public LeaderboardsScr leaderBoardScr;

        public void OnSuccess(object response)
        {
            Game game = (Game)response;
            for (int i = 0; i < game.GetScoreList().Count; i++)
            {
                Text mPlayerText = leaderBoardScr.Friends[i].GetComponent<Text>();
                Text mScoreText = leaderBoardScr.FriendScores[i].GetComponent<Text>();

                mPlayerText.text = game.GetScoreList()[i].GetFacebookProfile().GetName();
                mScoreText.text = game.GetScoreList()[i].GetValue().ToString();
            }
            leaderBoardScr.gameObject.SetActive(true);

        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
            leaderBoardScr.gameObject.SetActive(true);
        }
    }

   public void HideLeaderBoard()
    {
        this.gameObject.SetActive(false);
    }
}
