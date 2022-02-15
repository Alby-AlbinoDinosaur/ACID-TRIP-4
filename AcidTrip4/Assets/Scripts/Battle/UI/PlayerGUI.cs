using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button plateButton;

    public Button attackButton;
    public Button itemsButton;
    public Button abilitiesButton;
    public Button guardButton;
    public Button basicCancelButton;
    public Animator plateAnimator;
    void Start()
    {
        BattleEventManager.OnEnemySelected += finishSelection;
        BattleEventManager.OnEnemyRevealSelect += startSelection;
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



}
