using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject ExitMenu;
	public GameObject SoundOn;
	public GameObject SoundOff;
	bool leaderShow;

	// Use this for initialization
	void Start () 
	{
		if(ExitMenu!=null)
		{
			ExitMenu = GameObject.Find("ExitMenu");
			ExitMenu.SetActive(false);
		}
		if(SoundOn!=null)
		{
			SoundOn = GameObject.Find("SoundOn");
			SoundOn.SetActive(true);
		}
		if(SoundOff!=null)
		{
			SoundOff = GameObject.Find("SoundOff");
			SoundOff.SetActive(false);
		}

		leaderShow = false;
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
		Application.LoadLevel("Gameplay");
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

	public void LeaderBoardController()
	{
		if(leaderShow == false)
		{
			LeaderboardShow();
		}
		else
		{
			LeaderboardHide();
		}
	}

	void LeaderboardShow()
	{

	}

	void LeaderboardHide()
	{
		
	}

	public void FacebookLogin()
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
