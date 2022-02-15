using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventManager : MonoBehaviour
{
    public delegate void Reveal();
    public static event Reveal OnEnemyRevealSelect;
    public static event Reveal OnEnemySelected;

    // Start is called before the first frame update
    public void revealEnemySelector()
    {
        
        if(OnEnemyRevealSelect != null)
        {
            OnEnemyRevealSelect();
        }
        
    }

    public void hideEnemySelector()
    {
        
        if(OnEnemySelected != null)
        {
            OnEnemySelected();
        }
        
    }



}
