using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public PlayerMover playerMover;
    public EnemyMover enemyMover;
//    public DialougeTrigger dtrigger;

    public List<Entity> globalEntityList = new List<Entity>(); //Global battle entity list
    public List<Entity> battleEntityList = new List<Entity>();
    public Dialogue text;

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
        print("Battle over");
    }
}
