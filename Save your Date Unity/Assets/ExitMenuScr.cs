using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitMenuScr : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
      
	}

    private void ScoreCallBack(IGraphResult result)
    {
        
    }

    public void PlayGame()
    {
        LevelManager.instance.BeginPlay();
        SceneManager.LoadScene("Gameplay");
        //Application.LoadLevel("Gameplay");
    }

    public void GoToHome()
    {
        Destroy(LevelManager.LevelPrefab);
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
