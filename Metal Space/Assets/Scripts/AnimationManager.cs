using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
     int horizontal;
    int vertical;

 private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }



  


    public void UpdateAnimator(float HorizontalMovement, float VerticalMovement  )
    {
        float trickedHorizontal;
        float trickedVertical;

        #region TrickedHorizontal

        if (HorizontalMovement > 0 && HorizontalMovement < 0.55f)
        {
            trickedHorizontal = 0.5f;

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
