using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockbutton : MonoBehaviour
{
    public BattleDialogueManager battleDialogueManager;
    public int hour; public int minute;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayTime()
    {
        string result = " It is ";
        hour = System.DateTime.Now.Hour;
        minute = System.DateTime.Now.Minute;
        if (hour >= 12)
        {
            result += (hour - 12) + ":" + minute + " PM.";
        }
        else
        {
            result += (hour) + ":" + minute + " AM.";
        }
        battleDialogueManager.WriteLine(result);

    }


}
