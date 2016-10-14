using UnityEngine;
using System.Collections;

public class Scared : MonoBehaviour
{
    Animator mScaredAnim;
    int mAnimBegin = Animator.StringToHash("AnimationBegin");

    public float minScareTime;
    public float maxScareTime;

    float mTimer = 0.0f;
    float mRandomRange;

    public float Force;

    BoatScr boatScr;

    bool mGirlScared = false;

    LevelManager levelManager;

    // Use this for initialization
    void Start ()
    {
        mScaredAnim = GetComponent<Animator>(); 
        mRandomRange =  Random.Range(minScareTime, maxScareTime);

        GameObject Boat = GameObject.Find("Boat L5");
        boatScr = Boat.GetComponent<BoatScr>();

        levelManager = LevelManager.instance;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(levelManager.mGameOver)
        {
            return;
        }
        mTimer += Time.deltaTime;

        if(mGirlScared && mTimer > 2.0f)
        {
            boatScr.AddForceOnLeft(Force);
        }

        if(mTimer > mRandomRange)
        {
            mScaredAnim.SetTrigger(mAnimBegin);
            mTimer = 0.0f;
            mRandomRange = Random.Range(minScareTime, maxScareTime);
            mGirlScared = !mGirlScared;
        }
	}
}