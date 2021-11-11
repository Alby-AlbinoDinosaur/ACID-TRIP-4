using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private List<CharStats2> charStats;
    
    [SerializeField]
    private GameObject actionsMenu, enemyUnitsMenu;
    // Start is called before the first frame update
    void Start()
    {
        charStats = new List<CharStats2>();
        GameObject[] playerCharacters = GameObject.FindGameObjectsWithTag("PlayerCharacter");

        // calculates turn order for units if there are multiple characters
        foreach(GameObject playerCharacter in playerCharacters){
            CharStats2 currentCharStats = playerCharacter.GetComponent<CharStats2>();
            currentCharStats.calculateNextActTurn(0);
            charStats.Add(currentCharStats);
        }

        GameObject[] enemyCharacters = GameObject.FindGameObjectsWithTag("EnemyCharacter");

        // calculates turn order for enemy units
        foreach(GameObject enemyCharacter in enemyCharacters){
            CharStats2 currentCharStats = enemyCharacter.GetComponent<CharStats2>();
            currentCharStats.calculateNextActTurn(0);
            charStats.Add(currentCharStats);
        }

        charStats.Sort();

        this.actionsMenu.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        
        this.nextTurn();
    }

    // Update is called once per frame
    void nextTurn()
    {
        CharStats2 currCharStats = charStats[0];
        charStats.Remove(currCharStats);

        if(!currCharStats.isDead()){
            GameObject currChar = currCharStats.gameObject;

            currCharStats.calculateNextActTurn(currCharStats.nextActTurn);
            charStats.Add(currCharStats);
            charStats.Sort();

            if(currChar.tag == "PlayerChar"){
                Debug.Log("Player Unit attacks!");
            }
            else {
                Debug.Log("Enemy Unit attacks!");
            }
        }
        else {
            this.nextTurn();
        }
    }
}
