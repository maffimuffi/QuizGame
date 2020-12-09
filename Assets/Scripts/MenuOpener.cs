using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public GameObject gameScreen;

    // Animaatio pelin ruutujen vaihtamiselle
    public void OpenGameScreen()
    {
        if(gameScreen != null)
        {
            //bool isActive = gameScreen.activeSelf;

            //gameScreen.SetActive(!isActive);

            Animator animator = gameScreen.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("OpenMenu");
                animator.SetBool("OpenMenu", !isOpen);
            }
        }
    }
}
