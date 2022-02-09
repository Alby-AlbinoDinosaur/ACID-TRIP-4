using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public List<Entity> playerList;

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

}
