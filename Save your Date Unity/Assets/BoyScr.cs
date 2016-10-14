using UnityEngine;
using System.Collections;

public class BoyScr : MonoBehaviour
{
    Animator mHitAnim;
    int mHitLeft = Animator.StringToHash("LeftTap");
    int mHitRight = Animator.StringToHash("RightTap");

    public float inputDelay;
    bool mInputReady = true;
    float mInputTimer = 0.0f;
    // Use this for initialization
    void Start ()
    {
        mHitAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        mInputTimer -= Time.deltaTime;

        if(mInputReady)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                // use the input stuff
                if(Input.GetMouseButtonDown(0))
                {
                    mInputReady = false;
                    mInputTimer = inputDelay;

                   Vector3 touchPos = Input.mousePosition;
                    if (touchPos.x > Screen.width * 0.5f)
                    {
                        mHitAnim.SetTrigger(mHitRight);
                    }
                    else
                    {
                        mHitAnim.SetTrigger(mHitLeft);
                    }
                }
            }

            else
            {
                if (Input.touchCount >= 1)
                {
                    mInputReady = false;
                    mInputTimer = inputDelay;
                    Touch touch = Input.GetTouch(0);
                    if(touch.position.x > Screen.width*0.5f)
                    {
                        mHitAnim.SetTrigger(mHitRight);
                    }
                    else
                    {
                        mHitAnim.SetTrigger(mHitLeft);
                    }
                }
            }
        }
        else if(mInputTimer < 0.0f)
        {
            mInputReady = true;
        }
	}
}
