using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Entity> enemyList;
    public PlayerMover playerMover;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns true if all enemies in enemyList are defeated, else false
    bool isAllDefeated()
    {
        int defeatCount = 0;
        foreach (Entity enemy in enemyList)
        {
            if (enemy.IsDefeated())
                defeatCount++;
        }

        return defeatCount == enemyList.Count;
    }

    //Move through enemy list and have each enemy autochoose their targets and next move
    void EnemyChoosing()
    {
        if (isAllDefeated())
        {
            //End battle
        }
        else
        {
            foreach (Entity enemy in enemyList)
            {
                if (!enemy.IsDefeated())
                {
                    enemy.AutoChooseNextMove(playerMover.playerList, enemyList);
                    //Show enemy intent in battle manager
                }
            }
        }
        
    }
}
