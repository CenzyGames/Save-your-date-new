using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.social;
using System;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    // Singleton Class
    public static LevelManager instance = null;

    // Info
    public string mUserName;
    public string mUserId;
    public string mGlobalLeaderBoard = "Save Your Date";
    public string mFriendsLeaderBoard = "Save Your Data - Friends";

    // Shephertz Services
    public SocialService socialService;
    public ScoreBoardService scoreBoardService;

    // Score 
    public int mScore = 0;
    public int mMultiplier = 1;
    public int mHighScore;
    public float UpdateScoreTime;

    // Game Over
    public float GameOverDelay;
    float mGameOverTimer = 0.0f;
    public bool mGameOver = false;

    // Level Prefab
    public static GameObject LevelPrefab;

    // Multiplier Animatoin
    float mTimer = 0.0f;
    public Animator mMultiplierAnim;
    static int mAnimBegin = Animator.StringToHash("MultiplierChange");

    bool mRunning;

    void Awake()
    {
        if (instance == null)
        {           
            instance = this;
        }
      
        else if (instance != this)
        {            
            Destroy(gameObject);

        }

        App42API.Initialize("8ecb7919b653d8413cbac5816f38eaf0e05d0de1cde2935e0d7211d90b633f63", "7363cab1359c4241c63c12a7e55167de71c38f94530c231666203770ca24f6a9");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    { 
        mHighScore = PlayerPrefs.GetInt("highscore", 0);

        // Build Shephertz services
        socialService = App42API.BuildSocialService();
        scoreBoardService = App42API.BuildScoreBoardService();

        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!mGameOver)
        {
            mTimer += Time.deltaTime;
            mRunning = true;

            if(mTimer > UpdateScoreTime)
            {
                mScore += mMultiplier;
                mTimer -= UpdateScoreTime;
            }          
        }
        else
        {
            mGameOverTimer += Time.deltaTime;
            if(mGameOverTimer > GameOverDelay && mRunning)
            {
                if(mHighScore < mScore)
                {
                    mHighScore = mScore;
                    PlayerPrefs.SetInt("highscore", mHighScore);               
                    if(FB.IsLoggedIn)
                    {                                                                              
                        scoreBoardService.SaveUserScore(mGlobalLeaderBoard, mUserName, mScore, new SaveScoreBoardCallBack());
                        scoreBoardService.SaveUserScore(mFriendsLeaderBoard, mUserId, mScore, new SaveScoreBoardCallBack());
                    }

                }
                mRunning = false;
                mGameOverTimer = 0.0f;   
                SceneManager.LoadScene("Game Over");
            }


        }
	}




    public void UpdateScore()
    {
        Debug.Log("Updating Score");
        scoreBoardService.GetHighestScoreByUser(mGlobalLeaderBoard, mUserName, new UpdatingScoreCallBack());
        scoreBoardService.GetHighestScoreByUser(mFriendsLeaderBoard, mUserId, new UpdatingScoreCallBack());
    }

    public class UpdatingScoreCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Game game = (Game)response;           
            double savedHighScore = game.GetScoreList()[0].GetValue();
            LevelManager levelManager = LevelManager.instance;

            if (savedHighScore > levelManager.mHighScore)
            {
                Debug.Log("Syncing offline score with cloud score");
                Debug.Log("Highscore is " + savedHighScore);
                levelManager.mHighScore = (int)savedHighScore;
                PlayerPrefs.SetInt("highscore", levelManager.mHighScore);
            }
            else if (savedHighScore < levelManager.mHighScore)
            {
                Debug.Log("Syncing cloud score with offline score");
                instance.scoreBoardService.EditScoreValueById(game.GetScoreList()[0].GetScoreId(), savedHighScore, new SaveScoreBoardCallBack());
            }
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }


    public class SaveScoreBoardCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Game game = (Game)response;   
            
            Debug.Log("New HighScore Is : " + game.GetScoreList()[0].GetValue());       
            
        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
        }
    }

    public void BeginPlay()
    {
        mScore = 0;
        mMultiplier = 1;
        mGameOver = false;
    }

    public void PlayMultiplierAnim()
    {
        mMultiplierAnim.SetTrigger(mAnimBegin);
    }
}
