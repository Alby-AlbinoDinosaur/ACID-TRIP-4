using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public static List<Entity> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    //Move through enemy list and have 
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
                    enemy.AutoChooseNextMove(PlayerMover.playerList, enemyList);
            }
        }
        
    }
}
