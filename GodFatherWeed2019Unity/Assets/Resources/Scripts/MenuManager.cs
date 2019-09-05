using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator animator;
    public bool isCredit=false;

    public void Start()
    {
        animator.SetBool("menustart", true);
    }

    public void OpenScreen()
    {
        if (isCredit)
            animator.SetBool("creditstart", true);
        else
            animator.SetBool("introstart", true);
        animator.SetBool("menuclose", false);
    }

    public void CloseCredits()
    {
        animator.SetBool("creditstart", false);
        animator.SetBool("creditsclose", true);
    }

    public void isCreditorNot(bool value)
    {
        isCredit = value;
    }
    public void closeMenu()
    {
        animator.SetBool("menustart", false);
        animator.SetBool("menuclose", true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
