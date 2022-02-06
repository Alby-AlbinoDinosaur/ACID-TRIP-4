using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolEnemy1 : Entity
{
    

    // Start is called before the first frame update
    void Start()
    {   //Add each move to list
        base.moveList = new List<Move>();
        base.moveList.Add(Move1);
       
        base.Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool Move1(List<Entity> targetList, bool shouldExecute)
    {
        if (shouldExecute)
        {
            print("HEYYY!");
        }

        if (targetList.Count == 0)
            return true;
        else
            return false;
    }

    public override void AutoChooseNextMove(List<Entity> possibleTargets)
    {
        //set selected targets and next move
        base.nextMove = Move1;//temp
    }
}
