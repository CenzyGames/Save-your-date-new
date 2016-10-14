using UnityEngine;
using System.Collections;

public class BoatScr : MonoBehaviour
{
    public GameObject weightL;
    public GameObject weightR;

    public float forceMultiplier;
    public float moveThreshold;
    OctopusScr octopusScr;
    public float TentacleForce;

    int mProgression = 1;

    public float ProgressionTime;
    public float ForcePerProgression;
    public int MaxProgressions;

    float mProgressionTimer;

    Rigidbody2D rb;

    LevelManager levelManager;
    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody2D>();
        GameObject Octopus = GameObject.Find("Monster L5");
        octopusScr = Octopus.GetComponent<OctopusScr>();
        levelManager = LevelManager.instance;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(levelManager.mGameOver)
        {
            return;
        }

        mProgressionTimer += Time.deltaTime;

        if (mProgressionTimer > ProgressionTime)
        {
            mProgressionTimer = 0.0f;
            mProgression++;
            if(mProgression <= MaxProgressions)
            {
                TentacleForce += ForcePerProgression;
            }
        }

        float tilt = Input.acceleration.x;

        if(Mathf.Abs(tilt) < moveThreshold)
        {
            return;
        }
                  
  
        if(tilt >= 0.0f)
        {
            AddForceOnRight(tilt * forceMultiplier);
        }
        else
        {

            AddForceOnLeft(-tilt * forceMultiplier);                  
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(levelManager.mGameOver)
        {
            return;
        }
        if (!octopusScr.HasHit())
        {
            if (collider.gameObject.name == "Monster Tentacle 8")
            {
                octopusScr.Hit();
                AddForceOnLeft(TentacleForce);
                levelManager.mMultiplier = 1;
                Debug.Log("Bad Job");
                levelManager.PlayMultiplierAnim();
                return;
            }

            else if (collider.gameObject.name == "Monster Tentacle 8L")
            {
                octopusScr.Hit();
                AddForceOnRight(TentacleForce);
                levelManager.mMultiplier = 1;
                Debug.Log("Bad Job");
                levelManager.PlayMultiplierAnim();
                return;
            }
        }

        if(collider.gameObject.name == "Gyro off collider")
        {
            levelManager.mGameOver = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Monster Tentacle 8")
        {
            AddForceOnLeft(TentacleForce);
        }

        else if (collider.gameObject.name == "Monster Tentacle 8L")
        {            
            AddForceOnRight(TentacleForce);            
        }
    }


    public void AddForceOnLeft(float forceMagnitude)
    {
        Vector2 WeightPosL = weightL.transform.position;
        Vector2 WeightPosR = weightR.transform.position;
        Vector2 ForceDir = Vector2.zero - WeightPosR;
        ForceDir.Normalize();
        rb.AddForceAtPosition(ForceDir * forceMagnitude, WeightPosL);
    }

    public void AddForceOnRight(float forceMagnitude)
    {
        Vector2 WeightPosR = weightR.transform.position;
        Vector2 WeightPosL = weightL.transform.position;
        Vector2 ForceDir = Vector2.zero - WeightPosL;
        ForceDir.Normalize();
        rb.AddForceAtPosition(ForceDir * forceMagnitude, WeightPosR);
    }
}

