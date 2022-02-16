using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public PlayerMover playerMover;
    public EnemyMover enemyMover;

    public BattleDialogueManager battleDialogue;
    public BattleEventManager battleEventManager;

    public List<Entity> globalEntityList = new List<Entity>(); //Global battle entity list
    public List<Entity> battleEntityList = new List<Entity>();
    bool readyToRun = false;

    public GameObject endTurnButton;


    public bool pauseBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void InstantiateBattle()
    {
        globalEntityList.Clear();
        battleEntityList.Clear();

        //Add player list and enemy list to global list
        globalEntityList.AddRange(playerMover.playerList);
        globalEntityList.AddRange(enemyMover.enemyList);
    }

    public void EndTurnButton()
    {
        StartCoroutine("EndTurnButtonCoroutine");
    }

    public IEnumerator EndTurnButtonCoroutine()
    {
        if (!pauseBattle)
        {
            if (playerMover.moveThroughPlayers())
            {

                enemyMover.EnemyChoosing();
                SortEntities();
                foreach (Entity current in battleEntityList)
                {
                    current.Run();
                    // With the BDM set up, entities can also actually say text within the move itself.
                    // TODO: make current do an animation
                    battleDialogue.WriteLine(current.moveTextList[current.nextMove](2));
                    battleEventManager.updateGUIS();

                    yield return new WaitForSeconds(0.2f);
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                }

                //This second loop is for debug, this is not the right way to do this
             /*   foreach (Entity current in battleEntityList)
                {
                    print("Entity: " + current.name);
                    print("Health: " + current.health_stat);
                }*/
                    battleEntityList.Clear();

                // check if all players are dead and end battle
                if (playerMover.isAllDefeated())
                {
                    print("you are dead");
                    EndBattle();
                    battleDialogue.WriteLine("You Lose!");
                }
                // check if all enemies are dead and end battle
                if (enemyMover.isAllDefeated())
                {
                    //End battle
                    EndBattle();
                    battleDialogue.WriteLine("You Win!");
                }
                endTurnButton.SetActive(true);
            }
            else
            {
                battleDialogue.WriteLine("You have not selected all moves.");
                endTurnButton.SetActive(true);
            }
        }

    }

    //Sort entity list by speed stat (largest to smallest) using selection sort
    void SortEntities()
    {
        int listLength = battleEntityList.Count;

        //List must be 2 or greater in length
        if (listLength > 1)
        {
            // One by one move boundary of unsorted subarray
            for (int i = 0; i < listLength - 1; i++)
            {
                // Find the largest element in unsorted array
                int largest = i;
                for (int j = i + 1; j < listLength; j++)
                {
                    if (battleEntityList[j].speed_stat > battleEntityList[largest].speed_stat)
                    {
                        largest = j;
                    }
                }


                // Swap the found largest element with the first element
                Entity temp = battleEntityList[largest];
                battleEntityList[largest] = battleEntityList[i];
                battleEntityList[i] = temp;

            }//for
        }
        
    }

    //Run each entity's move
    private IEnumerator MoveExecute()
    {
        foreach (Entity current in battleEntityList)
        {
            //Pause until pauseBattle is false (to have in between events if wanted)
            yield return new WaitUntil(() => pauseBattle == false);
            current.Run();

        }
    }

    //Add entity to BattleManager (global) entity list
    public void AddEntity(Entity entity)
    {
        battleEntityList.Add(entity);
    }

    public void EndBattle()
    {
        //  while(true)
        // {
        //     DialougeTrigger.instance.
        //  }
        print("Battle is over");
        pauseBattle = true;
        battleDialogue.WriteLine("Battle is over.");
    }
}
