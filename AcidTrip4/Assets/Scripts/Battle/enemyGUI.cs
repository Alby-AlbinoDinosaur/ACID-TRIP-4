using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGUI : MonoBehaviour
{

    // Start is called before the first frame update
    public Image hpBar;
    
    public Button selector;

    public Entity enemyEntity;
    private int maxHp = 0;

    void Start()
    {
        BattleEventManager.OnEnemyRevealSelect+= enableSelector;
        BattleEventManager.OnEnemySelected+= disableSelector;
        BattleEventManager.OnGUIUpdate+= updateGUI;
        selector.interactable = false;
        maxHp = enemyEntity.health_stat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void enableSelector()
    {
        selector.interactable = true;
    }

    void disableSelector()
    {
        selector.interactable = false;
    }

    void updateGUI()
    {
        if(maxHp != 0)
        {
            hpBar.fillAmount = (float)enemyEntity.health_stat/maxHp;
        }
    }
}
