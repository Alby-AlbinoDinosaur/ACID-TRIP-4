using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<Entity> entityList = new List<Entity>();
    public List<Entity> targetList = new List<Entity>();

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
        entityList.Clear();
    }

    //Sort entity list by speed stat (largest to smallest) using selection sort
    void SortEntities()
    {
        int listLength = entityList.Count;

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
                    if (entityList[j].speed_stat > entityList[largest].speed_stat)
                    {
                        largest = j;
                    }
                }


                // Swap the found largest element with the first element
                Entity temp = entityList[largest];
                entityList[largest] = entityList[i];
                entityList[i] = temp;

            }//for
        }
        
    }

    //Run each entity's move
    private IEnumerator MoveExecute()
    {
        foreach (Entity current in targetList)
        {
            //Pause until pauseBattle is false
            yield return new WaitUntil(() => pauseBattle == false);
            current.Run();
        }
    }

    //Add entity to BattleManager (global) entity list
    public void AddEntity(Entity entity)
    {
        entityList.Add(entity);
    }
}
