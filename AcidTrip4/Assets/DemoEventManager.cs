using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void Conversation();
    public static event Conversation OnDialogueEnd;
    public static event Conversation OnDialogueStart;

    public delegate void BattleState();
    public static event BattleState OnBattleStart;
    public static event BattleState OnBattleEnd;

    // Start is called before the first frame update

    
    

    
    void Start()
    {
        OnDialogueEnd += nextBattle;
    }
    public void nextDialogue()
    {
        
        if(OnDialogueStart != null)
        {
            OnDialogueStart();
        }
        
    }

    public void nextBattle()
    {
        /*
        if(OnBattleStart != null)
        {
            OnBattleStart();
        }
        */
        SceneManager.LoadScene(2);
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
