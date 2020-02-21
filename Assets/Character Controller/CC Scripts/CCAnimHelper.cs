using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCAnimHelper : MonoBehaviour
{
    //script that helps blending animations

    [Range(0, 1)]
    public float vertical;
    

    Animator anim;

    public string animName;
    public bool playAnim;

    //sets up animator
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    

    void Update()
    {
        //enables root motion when the player can move
       

        //when the attack animation plays it won't be blended, the animation will have 
        //a crossfade and then the animation stops playing
        if (playAnim)
        {
            vertical = 0;

            anim.CrossFade(animName, 0.3f);
            anim.SetBool("canMove", false);
         
            playAnim = false;
            
        }
        
        anim.SetFloat("Vertical", vertical);
    }
}
