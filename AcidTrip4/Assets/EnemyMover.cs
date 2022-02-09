using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Entity> enemyList;

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
        foreach (Entity player in enemyList)
        {
            if (player.IsDefeated())
                defeatCount++;
        }

        return defeatCount == enemyList.Count;
    }

    //void MoveThroughEnemies
}
