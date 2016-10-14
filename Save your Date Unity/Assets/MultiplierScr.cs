using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiplierScr : MonoBehaviour {

    Text mMultiplierText;
    LevelManager levelManager;

    // Use this for initialization
    void Start ()
    {        
        mMultiplierText = GetComponent<Text>();
        levelManager = LevelManager.instance;
        levelManager.mMultiplierAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        mMultiplierText.text = ("x") + levelManager.mMultiplier.ToString();
    }
}
