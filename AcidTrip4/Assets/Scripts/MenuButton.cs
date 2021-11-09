using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public MenuButtonController menuButtonController;
    public Animator animator;
    public int thisIndex;
    [SerializeField] GameObject menuPanelToOpen;
    void Update()
    {
        if(menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if(menuButtonController.isPressConfirm)
            {
                animator.SetBool("pressed", true);
                if(menuPanelToOpen != null)
                {
                
    menuButtonController.gameObject.SetActive(false);
                    menuPanelToOpen.SetActive(true);
                }
            } 
            else if(animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
            }
        } else {
            animator.SetBool("selected", false);
        }
    }
}
