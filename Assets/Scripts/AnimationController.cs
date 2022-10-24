using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // My very own animation controller, didn't want to clutter my player movement setup
    [Header("References")]
    public Animator animator;

    float moveX;
    float moveY;


    private void Update()
    {
        // Just changes the float values inside the animator depending on how the player moves, nothing fancy yet.
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("xPos", moveX);
        animator.SetFloat("yPos", moveY);
    }
}
