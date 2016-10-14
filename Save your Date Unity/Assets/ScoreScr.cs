using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScr : MonoBehaviour {

    Text mScoreText;
    LevelManager levelManager;
    // Use this for initialization
    void Start ()
    {
        mScoreText = GetComponent<Text>();
        levelManager = LevelManager.instance;
    }
	
	// Update is called once per frame
	void Update ()
    {
        mScoreText.text = levelManager.mScore.ToString();
    }
}
