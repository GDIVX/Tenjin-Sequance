using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        anim.SetTrigger(name);
    }
}
