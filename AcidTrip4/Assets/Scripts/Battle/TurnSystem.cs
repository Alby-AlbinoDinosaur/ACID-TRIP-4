using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour 
{
    // Need to get rid of this variable later when connecting this sceen w/ other scenes
    public GameObject playerParty;
    
    private List<UnitStats> unitsStats;
    [SerializeField]
    private GameObject actionsMenu, enemyUnitsMenu;
    void Start ()
    {
        unitsStats = new List<UnitStats>();
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        
        foreach(GameObject playerUnit in playerUnits) 
        {
            UnitStats currentUnitStats = playerUnit.GetComponent<UnitStats>();
            currentUnitStats.calculateNextActTurn(0);
            unitsStats.Add (currentUnitStats);
        }
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach(GameObject enemyUnit in enemyUnits) 
        {
            UnitStats currentUnitStats = enemyUnit.GetComponent<UnitStats>();
            currentUnitStats.calculateNextActTurn(0);
            unitsStats.Add(currentUnitStats);
        }
        unitsStats.Sort();
        this.actionsMenu.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        this.nextTurn();
    }
    public void nextTurn () 
    {
        UnitStats currentUnitStats = unitsStats [0];
        unitsStats.Remove(currentUnitStats);
        if(!currentUnitStats.isDead ()) 
        {
            GameObject currentUnit = currentUnitStats.gameObject;
            currentUnitStats.calculateNextActTurn(currentUnitStats.nextActTurn);
            unitsStats.Add(currentUnitStats);
            unitsStats.Sort();
            if(currentUnit.tag == "PlayerUnit") 
            {
                Debug.Log("Player unit acting");
                this.playerParty.GetComponent<SelectUnit>().selectCurrentUnit(currentUnit.gameObject);
            } 
            else 
            {
                currentUnit.GetComponent<EnemyUnitAction>().act();
            }
        } 
        else 
        {
            this.nextTurn();
        }
    }
}