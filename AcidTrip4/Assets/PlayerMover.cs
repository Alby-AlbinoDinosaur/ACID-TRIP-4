using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public List<Entity> playerList;
    public EnemyMover enemyMover;


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
        foreach (Entity player in playerList)
        {
            if (player.IsDefeated())
                defeatCount++;
        }

        return defeatCount == playerList.Count;
    }


    void moveThroughPlayers()
    {
        if (isAllDefeated())
        {
            // tell BM to End battle
        }

        foreach (Entity current in playerList)
        {
            int currentSelection = 0;

            while (true)
            {
                List<Entity> possibleTargets = new List<Entity>();
                // Show possible moves in menu
                // When click possible move in menu
                // currentSelection = clicked move;
                foreach (Entity possible in playerList)
                {
                    if (current.moveTargetList[currentSelection](possible))
                    { possibleTargets.Add(possible); }
                }
                foreach (Entity possible in enemyMover.enemyList)
                {
                    if (current.moveTargetList[currentSelection](possible))
                    { possibleTargets.Add(possible); }
                }
                // Do the UI stuff
                // show up all the possible names
                // if you clicked any name but back
                // current.selectedTarget = clicked
                current.nextMove = currentSelection;
                break;
            }

            //put in place in battlemanager list
            BattleManager.instance.AddEntity(current);
        }//foreach

    }

}
