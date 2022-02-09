using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public static List<Entity> playerList;
    public GameObject enemyMover;


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
            // End battle
        }

        foreach (Entity current in playerList)
        {
            int currentSelection = 0;
            while (true)
            {
                // Show possible moves in menu
                // When click possible move in menu


                // Do the UI stuff


                break;
            }
        }


        //put in place in battlemanager list
    }

}
