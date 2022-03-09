using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button plateButton;
    public Entity playerEntity;
    public Image hpBar;
    public Image ppBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI ppText;
    public TextMeshProUGUI nextText;
    public GameObject damagePrefab;
    public GameObject healPrefab;
    

    public Button attackButton;
    public Button itemsButton;
    public Button abilitiesButton;
    public Button guardButton;
    public Button basicCancelButton;
    public Animator plateAnimator;
    public Animator faceAnimator;
    public Animator statusAnimator;
    public GameObject descriptorMenu;

    public string hit_sound;
    public string death_sound;

    private int maxHp = 0;
    private int maxPpl = 0;
    private int currentHp = 0;
    private int maxPp = 0;


    
    void Start()
    {
        BattleEventManager.OnEnemySelected += finishSelection;
        //BattleEventManager.OnEnemyRevealSelect += startSelection;
        BattleEventManager.OnGUIUpdate += updateGUI;
        maxHp = playerEntity.health_stat;
        
        currentHp = maxHp;
        
        //Debug.Log("maxHp set to " + maxHp);
        maxPp = playerEntity.power_points;
        
        updateText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void finishSelection()
    {
        plateAnimator.SetTrigger("Finished");
        if (!playerEntity.IsDefeated()) { plateButton.interactable = true; }
        descriptorMenu.SetActive(false);
        attackButton.interactable = true;
        itemsButton.interactable = true;
        abilitiesButton.interactable = true;
        guardButton.interactable = true;
        basicCancelButton.interactable = true;
    }

    void startSelection()
    {
        attackButton.interactable = false;
        itemsButton.interactable = false;
        abilitiesButton.interactable = false;
        guardButton.interactable = false;
        basicCancelButton.interactable = false;
    }
    private void updateFace(float ratio){
        if(ratio > (float)2/3)
                    {   
                        //Debug.Log("Set State 0");
                        faceAnimator.SetInteger("State", 0);
                        if(hit_sound.Length > 0){
                            AudioManager.instance.Play(hit_sound);
                        }
                    }
                    else if(ratio > (float)1/3)
                    {   
                        //Debug.Log("Set State 1");
                        faceAnimator.SetInteger("State", 1);
                        if(hit_sound.Length > 0){
                            AudioManager.instance.Play(hit_sound);
                        }
                    }
                    else if(ratio > (float)0)
                    {   
                        //Debug.Log("Set State 2");
                        faceAnimator.SetInteger("State", 2);
                        if(hit_sound.Length > 0){
                            AudioManager.instance.Play(hit_sound);
                        }
                    }
                    else 
                    {   
                        //Debug.Log("Set State 3");
                        faceAnimator.SetInteger("State", 3);
                        if(death_sound.Length > 0){
                            AudioManager.instance.Play(death_sound);
                        }
                    }
                    
    }

    void updateGUI()
    {
        updateStatus();
        
        if(maxHp != 0)
        {

            

            if(currentHp != playerEntity.health_stat)
            {
                


                float ratio = (float)playerEntity.health_stat/maxHp;
                hpBar.fillAmount = ratio;
                /*
                Debug.Log("Ratio: " +ratio);
                Debug.Log("Current HP: " + currentHp);
                Debug.Log("Actual HP: " + playerEntity.health_stat);
                */
                
                if(playerEntity.health_stat < currentHp)
                {
                    
                    faceAnimator.SetTrigger("Damage");
                    plateAnimator.SetTrigger("Damage");

        
                    updateFace(ratio);
                    
                    
                    GameObject canvas = Instantiate(damagePrefab, hpBar.transform.position, Quaternion.identity);
                    canvas.transform.SetParent(gameObject.transform);
                    //Debug.Log(hpBar.transform.parent.name.transform.position);
                    
                    RectTransform rect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
                    RectTransform targetRect = hpBar.transform.parent.parent.GetComponent<RectTransform>();
                    rect.localScale = targetRect.localScale;
                    
                    
                    TextMeshProUGUI text = canvas.transform.GetChild(0).gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
                    
                    text.text = "-" + (currentHp-playerEntity.health_stat).ToString();
                    
                    
                    
                }
                else
                {
                    faceAnimator.SetTrigger("Heal");
                    plateAnimator.SetTrigger("Heal");
                    GameObject canvas = Instantiate(healPrefab, hpBar.rectTransform.position, Quaternion.identity);
                    canvas.transform.SetParent(gameObject.transform);
                    RectTransform rect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
                    RectTransform targetRect = hpBar.transform.parent.parent.GetComponent<RectTransform>();
                    rect.localScale = targetRect.localScale;
                    
                    TextMeshProUGUI text = canvas.transform.GetChild(0).gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
                    text.text = "+" + (playerEntity.health_stat-currentHp).ToString();

                    updateFace(ratio);
                }

                currentHp = playerEntity.health_stat;
            }
            
            //Debug.Log("Current HP: " + playerEntity.health_stat + "Set bar to: " + (float)playerEntity.health_stat/maxHp);
        }
        if(maxPp != 0)
        {
            ppBar.fillAmount = (float)playerEntity.power_points/maxPp;
        }
        updateText();

        if (playerEntity.IsDefeated()) { plateButton.interactable = false; }
        //nextText.text = "1";

    }
    void updateText()
    {
        hpText.text = playerEntity.health_stat.ToString() + "/" + maxHp.ToString();
        ppText.text = playerEntity.power_points.ToString() + "/" + maxPp.ToString();

        if(playerEntity.nextMove >=0 && !playerEntity.nextMoveHasAlreadyBeenRun)
        {
            nextText.text = playerEntity.moveTextList[playerEntity.nextMove](0);
        }
        else{
            nextText.text = "";
        }

    }

    void updateStatus()
    {
        
        if(playerEntity.isCheesed)
        {
            //Debug.Log("AL CHEESED");

            /* I tried to get this done, but the lambdas dont have enough info to differentiate status effects
            foreach(var effect in playerEntity.thisTurnEffects)
            {
                Debug.Log(effect.GetType());
            }
            */

            statusAnimator.SetBool("Cheese Touched",true);

        }
        else
        {
            //Debug.Log("SEEET");
            statusAnimator.SetBool("Cheese Touched",false);
        }
    }

    private void OnDestroy()
    {
        BattleEventManager.OnEnemySelected -= finishSelection;
        //BattleEventManager.OnEnemyRevealSelect -= startSelection;
        BattleEventManager.OnGUIUpdate -= updateGUI;
    }

    


}
