using UnityEngine;
using System.Collections;
using System;
using Facebook.Unity;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.social;

public class FBShareScr : MonoBehaviour
{

    public string status;

    // Use this for initialization
    void Start ()
    {
	    
	}

    public void Share()
    {    
        if(FB.IsLoggedIn)
        {
            FB.FeedShare
                (
                    "",
                    new Uri("https://www.google.ca/?gfe_rd=cr&ei=BZn9V-qaH5C_-wXCgLKQBw"),
                    "Google"
                );
        }
    }

    public void FeedCallback(IShareResult result)
    {
        Debug.Log(result.RawResult);
    }
}
