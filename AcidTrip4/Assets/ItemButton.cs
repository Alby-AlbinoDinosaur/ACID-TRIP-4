using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator buttonAnimator;
    private static bool exploded = false;
    private bool done = false;
    void Start()
    {
        if(exploded && !done){
            buttonAnimator.SetTrigger("Disappear");
            done = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(exploded && !done){
            buttonAnimator.SetTrigger("Disappear");
            done = true;
        }
    }

    public void explode()
    {
        
        buttonAnimator.SetTrigger("Explode");
        exploded = true;
        done = true;
        
    }

    

}
