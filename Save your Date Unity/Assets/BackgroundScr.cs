using UnityEngine;
using System.Collections;

public class BackgroundScr : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        LevelManager.LevelPrefab = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    { 
	}
}
