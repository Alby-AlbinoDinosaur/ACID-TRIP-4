using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Entity> enemyList;
    public PlayerMover playerMover;
    public BattleDialogueManager battleDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addEnemy(Entity current)
    {
        enemyList.Add(current);
    }

    //Returns true if all enemies in enemyList are defeated, else false
    public bool isAllDefeated()
    {
        int defeatCount = 0;
        foreach (Entity enemy in enemyList)
        {
            if (enemy.IsDefeated())
            {
                defeatCount++;
                print("an enemy is dead");
            }
        }

        return defeatCount == enemyList.Count;
    }

    //Move through enemy list and have each enemy autochoose their targets and next move
    public void EnemyChoosing()
    {
        /*
        if (isAllDefeated())
        {
            //End battle
            battleDialogue.WriteLine("You Win!");
            BattleManager.instance.EndBattle();
        }*/
            foreach (Entity current in enemyList)
            {
                if (!current.IsDefeated())
                {
                print("gotcha!");
                    current.AutoChooseNextMove(playerMover.playerList, enemyList);
                    //Show enemy intent in battle manager
                }

                //Add enemy to battle manager
                BattleManager.instance.AddEntity(current);
            }
        
    }
}
