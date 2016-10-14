using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreScr : MonoBehaviour
{
    Text mHighScoreText;
	// Use this for initialization
	void Start ()
    {
        mHighScoreText = GetComponent<Text>();
        mHighScoreText.text = LevelManager.instance.mHighScore.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
