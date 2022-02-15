using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGUI : MonoBehaviour
{

    // Start is called before the first frame update
    
    public Button selector;
    void Start()
    {
        BattleEventManager.OnEnemyRevealSelect+= enableSelector;
        BattleEventManager.OnEnemySelected+= disableSelector;
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
}
