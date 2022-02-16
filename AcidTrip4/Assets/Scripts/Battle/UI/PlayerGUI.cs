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
    public TextMeshProUGUI nextText;

    public Button attackButton;
    public Button itemsButton;
    public Button abilitiesButton;
    public Button guardButton;
    public Button basicCancelButton;
    public Animator plateAnimator;

    private int maxHp = 0;
    private int maxPp = 0;
    void Start()
    {
        BattleEventManager.OnEnemySelected += finishSelection;
        BattleEventManager.OnEnemyRevealSelect += startSelection;
        BattleEventManager.OnGUIUpdate += updateGUI;
        maxHp = playerEntity.health_stat;
        //Debug.Log("maxHp set to " + maxHp);
        maxPp = playerEntity.power_points;

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
        //basicCancelButton.interactable = true;
    }

    void startSelection()
    {
        attackButton.interactable = false;
        itemsButton.interactable = false;
        abilitiesButton.interactable = false;
        guardButton.interactable = false;
        //basicCancelButton.interactable = false;
    }

    void updateGUI()
    {
        
        if(maxHp != 0)
        {
            hpBar.fillAmount = (float)playerEntity.health_stat/maxHp;
            //Debug.Log("Current HP: " + playerEntity.health_stat + "Set bar to: " + (float)playerEntity.health_stat/maxHp);
        }
        if(maxPp != 0)
        {
            ppBar.fillAmount = playerEntity.power_points/maxPp;
        }

        if(playerEntity.nextMove >=0 && !playerEntity.nextMoveHasAlreadyBeenRun)
        {
            nextText.text = playerEntity.moveTextList[playerEntity.nextMove](0);
        }
        else{
            nextText.text = "";
        }
        //nextText.text = "1";
        
    }


}
