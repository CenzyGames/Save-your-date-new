using UnityEngine;
using System.Collections;

public class OarScr : MonoBehaviour
{
    OctopusScr octopusScr;
    LevelManager levelManager;

    // Use this for initialization
    void Start ()
    {
        GameObject Octopus = GameObject.Find("Monster L5");
        octopusScr = Octopus.GetComponent<OctopusScr>();
        levelManager = LevelManager.instance;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Monster Tentacle 8")
        {
            if (!octopusScr.HasHit())
            {
                octopusScr.ChangeToRetreatL();
                octopusScr.Hit();
                Debug.Log("Good Job");
                levelManager.mMultiplier++;
                levelManager.PlayMultiplierAnim();
            }
        }
        else if (collider.gameObject.name == "Monster Tentacle 8L")
        {           
            if (!octopusScr.HasHit())
            {   
                octopusScr.ChangeToRetreatR();
                octopusScr.Hit();
                Debug.Log("Good Job");
                levelManager.mMultiplier++;
                levelManager.PlayMultiplierAnim();
            }
        }
    }
}
