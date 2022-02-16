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

    private int maxHp = 0;
    private int maxPpl = 0;
    private int currentHp = 0;
    private int maxPp = 0;
    void Start()
    {
        BattleEventManager.OnEnemySelected += finishSelection;
        BattleEventManager.OnEnemyRevealSelect += startSelection;
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
        plateButton.interactable = true;

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
                    }
                    else if(ratio > (float)1/3)
                    {   
                        //Debug.Log("Set State 1");
                        faceAnimator.SetInteger("State", 1);
                    }
                    else if(ratio > (float)0)
                    {   
                        //Debug.Log("Set State 2");
                        faceAnimator.SetInteger("State", 2);
                    }
                    else 
                    {   
                        //Debug.Log("Set State 3");
                        faceAnimator.SetInteger("State", 3);
                    }
                    
    }

    void updateGUI()
    {
        
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

        
                    updateFace(ratio);
                    
                    
                    GameObject canvas = Instantiate(damagePrefab, hpBar.rectTransform.position, Quaternion.identity);
                    canvas.transform.SetParent(gameObject.transform);
                    TextMeshProUGUI text = canvas.transform.GetChild(0).gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
                    text.text = (currentHp-playerEntity.health_stat).ToString();
                    
                    
                    
                }
                else
                {
                    faceAnimator.SetTrigger("Heal");
                    GameObject canvas = Instantiate(healPrefab, hpBar.rectTransform.position, Quaternion.identity);
                    canvas.transform.SetParent(gameObject.transform);
                    TextMeshProUGUI text = canvas.transform.GetChild(0).gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
                    text.text = (playerEntity.health_stat-currentHp).ToString();

                    updateFace(ratio);
                }

                currentHp = playerEntity.health_stat;
            }
            
            //Debug.Log("Current HP: " + playerEntity.health_stat + "Set bar to: " + (float)playerEntity.health_stat/maxHp);
        }
        if(maxPp != 0)
        {
            ppBar.fillAmount = playerEntity.power_points/maxPp;
        }

        updateText();

        
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


}
