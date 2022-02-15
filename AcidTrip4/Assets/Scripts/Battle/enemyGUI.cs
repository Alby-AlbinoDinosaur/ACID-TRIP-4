using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGUI : MonoBehaviour
{

    // Start is called before the first frame update
    public Image hpBar;
    
    public Button selector;
    void Start()
    {
        BattleEventManager.OnEnemyRevealSelect+= enableSelector;
        BattleEventManager.OnEnemySelected+= disableSelector;
        BattleEventManager.OnGUIUpdate+= updateGUI;
        selector.interactable = false;
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
        hpBar.fillAmount -= 0.2f;
    }
}
