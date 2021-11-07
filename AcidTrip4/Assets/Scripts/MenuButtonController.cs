using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for Menu Navigation
// Keeps track of current menu position
public class MenuButtonController : MonoBehaviour
{
    public int index;
    public int maxIndex;
    [SerializeField] bool keyDown;
    [SerializeField] RectTransform rectTransform;
    bool isPressUp, isPressDown, isPressConfirm;
    int VerticalMovement;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isPressUp = isPressDown = false;   
    }
    void Update()
    {
        if (isPressUp) VerticalMovement = 1;
        if (isPressDown) VerticalMovement = -1;
        if (!isPressUp && !isPressDown) VerticalMovement = 0;

        // Scrollable Menu Function
        if (Input.GetAxis("Vertical") != 0 || VerticalMovement != 0)
        {
            if(!keyDown)
            {
                // adjusts menu position, ensures cursor stays in bounds
                if(Input.GetAxis("Vertical") < 0 || VerticalMovement < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                        if (index > 1 && index < maxIndex)
                        {
                            rectTransform.offsetMax -= new Vector2(0, -25);
                        }
                    }
                    else
                    {
                        index = 0;
                        rectTransform.offsetMax = Vector2.zero;
                    }
                } else if(Input.GetAxis("Vertical") > 0 || VerticalMovement > 0) {
                    if (index > 0)
                    {
                        index--;
                        if(index < maxIndex - 1 && index > 0)
                        {
                            rectTransform.offsetMax -= new Vector2(0, 25);
                        }
                    } 
                    else
                    {
                        index = maxIndex;
                        rectTransform.offsetMax = new Vector2(0, (maxIndex - 2) * 25);
                    }
                }

                keyDown = true;
            } 
        } else {
            keyDown = false;
        }
    }
    public void onPressUp()
    {
        isPressUp = true;
    }
    public void onReleaseUp()
    {
        isPressUp = false;
    }
    public void onPressDown()
    {
        isPressDown = true;
    }
    public void onReleaseDown()
    {
        isPressDown = false;
    }
    public void onPressConfirm()
    {
        isPressConfirm = true;
    }
    public void onReleaseConfirm()
    {
        isPressConfirm = false;
    }
}
