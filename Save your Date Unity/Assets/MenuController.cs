using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using System.Collections.Generic;
using System.Collections;

public class MenuController : MonoBehaviour
{

	public GameObject ExitMenu;
	public GameObject SoundOn;
    public GameObject SoundOff;


	// Use this for initialization
	void Start () 
	{
        if (ExitMenu!=null)
		{
			ExitMenu.SetActive(false);
		}
		if(SoundOn!=null)
		{
			SoundOn.SetActive(true);
		}
		if(SoundOff!=null)
		{			
			SoundOff.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			BackHardwareKeyPress();
		}
	
	}

	void BackHardwareKeyPress()
	{
		
	}
	void SoundsOn()
	{
		AudioListener.volume = 1;
		SoundOff.SetActive(false);
		SoundOn.SetActive(true);
	}
	
	void SoundsOff()
	{
		AudioListener.volume = 0;
		SoundOn.SetActive(false);
		SoundOff.SetActive(true);
	}

	public void PlayGame()
	{
        LevelManager.instance.BeginPlay();
        SceneManager.LoadScene("Gameplay");
	}

	public void SoundOnOff()
	{
		if(SoundOn.activeInHierarchy == true)
		{
			SoundsOff();
		}
		else
		{
			SoundsOn();
		}
	}

	void LeaderboardShow()
	{

	}

	public void ExitGame()
	{
		ExitMenu.SetActive(true);
	}

	public void ExitConfirmationYes()
	{
		Application.Quit();
	}

	public void ExitConfirmationNo()
	{
		if(ExitMenu!=null)
		ExitMenu.SetActive(false);
	}

}
