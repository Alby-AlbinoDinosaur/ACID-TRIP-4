using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void Conversation();
    public static event Conversation OnDialogueEnd;
    public static event Conversation OnDialogueStart;

    public delegate void BattleState();
    public static event BattleState OnBattleStart;
    public static event BattleState OnBattleEnd;
    public string nextScene = "";
    public FadeImage fade;
    public bool isBattleNext = false;
    public bool isBattleBefore = false;
    public bool startSong = true;

     

    // Start is called before the first frame update

    
    

    
    void Start()
    {
        OnDialogueEnd += nextBattle;
        if(startSong)
        {
            AudioManager.instance.Play("field_theme");
        }
        if(isBattleBefore){
            Image fadeImage = fade.GetComponent(typeof(Image)) as Image;
            fadeImage.color = new Color32(178,142,217,0);
        }
        if(fade){
            //fade.FadeToBlack(false);
        }
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
        
        if(nextScene.Length>0)
        {
            if(fade)
            {
                if(isBattleNext){
                    
                    Image fadeImage = fade.GetComponent(typeof(Image)) as Image;
                    fadeImage.color = new Color32(178,142,217,0);
                    
                    
                }
                else{
                    Image fadeImage = fade.GetComponent(typeof(Image)) as Image;
                    fadeImage.color = new Color32(0,0,0,0);
                }
                fade.FadeToBlack(true);
                StartCoroutine(transition());
            }
            else
            {
                AudioManager.instance.Stop("field_theme");
                SceneManager.LoadScene(nextScene);
            }
        }
        
    }

    IEnumerator transition(){

        yield return new WaitForSeconds(1.5f);
        if(isBattleNext)
        {
            AudioManager.instance.Stop("field_theme");
        }
        SceneManager.LoadScene(nextScene);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
