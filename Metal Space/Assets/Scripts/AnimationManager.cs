using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
   public Animator animator;
   //Why are these variables padded by one space?
     int horizontal;
     int vertical;
    bool isInteracting;

//Why is this not tabbed?
 private void Awake()
    {
        animator = GetComponent<Animator>();
        //IMO these "const" values are best declared as globals in the top or in a seperated file so you dont need to look for hardcoded stuff in the codebase
        //when its time to refactor
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string TargetAnimation,bool isInteracting)
    {
        //see above
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(TargetAnimation, 0.2f);
    }
    //Empty lines?


  


    public void UpdateAnimator(float HorizontalMovement, float VerticalMovement  )//Empty space?
    {
        //Is there a need for these variables as you have a copy of them in the function args?
        float trickedHorizontal;
        float trickedVertical;

        //Did you write these regions? What did you want to achive with them?
        #region TrickedHorizontal

        //I find the intention of this code hard to read, consider naming conditions as
        //bool displayWalking = HorizontalMovement > 0 && HorizontalMovement < 0.55f
        //bool displayRunning = displayWalking == false && HorizontalMovement > 0.55f
        if (HorizontalMovement > 0 && HorizontalMovement < 0.55f)
        {
            trickedHorizontal = 0.5f;
            //Empty line?

        }
        else if (HorizontalMovement > 0.55f)
        {
            trickedHorizontal = 1;
        }
        else if (HorizontalMovement < 0 && HorizontalMovement > -0.55f)
        {
            trickedHorizontal = -0.5f;

        }
        else if (HorizontalMovement < -0.55f)
        {
            trickedHorizontal = -1;
        }
        else
        {
            trickedHorizontal = 0;
        }


        #endregion

        #region TrickedVertical
        //I see code duplication based on movement direction, do you see a way to make code be direction agnostic?
        if (VerticalMovement > 0 && VerticalMovement < 0.55)
        {
            trickedVertical = 0.5f;

        }
        else if (VerticalMovement > 0.55f)
        {
            trickedVertical = 1;
        }
        else if (VerticalMovement < 0 && VerticalMovement > -0.55)
        {
            trickedVertical = -0.5f;

        }
        else if (VerticalMovement < -0.55f)
        {
            trickedVertical = -1;
        }
        else
        {
            trickedVertical = 0;
        }
        #endregion


        animator.SetFloat(horizontal,trickedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical,trickedVertical, 0.1f, Time.deltaTime);
    }
}
