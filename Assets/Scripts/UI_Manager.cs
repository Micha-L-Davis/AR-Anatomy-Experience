using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Animator anim;
    public int animIndex;

    public void Next()
    {
        if (animIndex == 2)
            return;
        animIndex++;
        anim.SetInteger("NextAnim", animIndex);
    }

    public void Previous()
    {
        if (animIndex == 0)
            return;
        animIndex--;
        anim.SetInteger("NextAnim", animIndex);
    }
}
