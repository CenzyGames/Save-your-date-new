using UnityEngine;
using System.Collections;

public class OctopusScr : MonoBehaviour
{
    Animator mOctopusAnim;

    enum AnimationState
    {
        Idle = 0,
        Attacking,
        Delay,
        RetreatL,
        RetreatR
    };

    int mBlendTree = Animator.StringToHash("Blend");

    public float minAttackTime;
    public float maxAttackTime;

    float mTimer = 0.0f;
    float mRandomRange;

    public float BlendSpeedMultiplier;

    float mSign;
    float mAnimationTimer = 1.0f;

    float mDelayTime;

    public float DelayTimeLeft;
    public float DelayTimeRight;

    AnimationState mAnimState;

    bool mHit = false;

    // Use this for initialization
    void Start ()
    {
        mOctopusAnim = GetComponent<Animator>();
        mOctopusAnim.SetFloat(mBlendTree, 1.0f);
        mRandomRange = Random.Range(minAttackTime, maxAttackTime);
        mAnimState = AnimationState.Idle;
    }
	
	// Update is called once per frame
	void Update ()
    {
        mTimer += Time.deltaTime;
       
 
        switch (mAnimState)
        {
           case AnimationState.Idle :          

                if (mTimer > mRandomRange)
                {                    
                    Debug.Log("Changed to Attack");                    
                    float animTime =  mOctopusAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
                    if (animTime > 0.9)
                    {
                        mAnimState = AnimationState.Attacking;
                        mSign = -1.0f;
                    }
                    else if(animTime > 0.4f && animTime < 0.5f)
                    {
                        mAnimState = AnimationState.Attacking;
                        mSign = 1.0f;
                    }
                }


                break;

           case AnimationState.Attacking :

                if (mAnimationTimer > 0.0f && mAnimationTimer < 2.0f)
                {
                    mAnimationTimer += Time.deltaTime * BlendSpeedMultiplier * mSign;
                    mOctopusAnim.SetFloat(mBlendTree, mAnimationTimer);
                }

                else
                {
                    mTimer = 0.0f;
                    mRandomRange = Random.Range(minAttackTime, maxAttackTime);
                    Debug.Log("Changed to Delay");
                    mAnimState = AnimationState.Delay;
                    if (mSign == 1.0f)
                    {
                        mOctopusAnim.SetFloat(mBlendTree, 2.0f);
                        mDelayTime = DelayTimeLeft;
                        mSign = -1.0f;
                    }
                    else
                    {
                        mOctopusAnim.SetFloat(mBlendTree, 0.0f);
                        mDelayTime = DelayTimeRight;
                        mSign = 1.0f;
                    }
                }
                break;

            case AnimationState.Delay:
                if (mTimer > mDelayTime)
                {
                    mAnimState = (mSign == 1.0f)? AnimationState.RetreatL : AnimationState.RetreatR;
                    Debug.Log("Changed to Retreat");
                }
                break;

            case AnimationState.RetreatL:

                if (mAnimationTimer < 1.0f)
                {                  
                    mAnimationTimer += Time.deltaTime * BlendSpeedMultiplier;
                    mOctopusAnim.SetFloat(mBlendTree, mAnimationTimer);
                }
                else
                {
                    mAnimationTimer = 1.0f;
                    mHit = false;
                    mOctopusAnim.SetFloat(mBlendTree, mAnimationTimer);
                    Debug.Log("Changed to Idle");
                    mAnimState = AnimationState.Idle;
                    mTimer = 0;
                }
                break;

            case AnimationState.RetreatR:

                if (mAnimationTimer > 1.0f)
                {               
                    mAnimationTimer += Time.deltaTime * BlendSpeedMultiplier * -1.0f;
                    mOctopusAnim.SetFloat(mBlendTree, mAnimationTimer);
                }
                else
                {
                    mAnimationTimer = 1.0f;
                    mOctopusAnim.SetFloat(mBlendTree, mAnimationTimer);
                    Debug.Log("Changed to Idle");
                    mHit = false;
                    mAnimState = AnimationState.Idle;
                    mTimer = 0;
                }
                break;

            default : break;
        }
    }

    public void Hit()
    {
        mHit = true;
    }

    public bool HasHit()
    {
        return mHit;
    }

    public void ChangeToRetreatL()
    {
        mAnimState = AnimationState.RetreatL;
    }

    public void ChangeToRetreatR()
    {
        mAnimState = AnimationState.RetreatR;       
    }
}
