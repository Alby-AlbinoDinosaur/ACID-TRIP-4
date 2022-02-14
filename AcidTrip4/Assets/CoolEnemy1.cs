using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolEnemy1 : Entity
{
    

    // Start is called before the first frame update
    void Start()
    {   //Add each move to list
        base.initialize();

        base.name = "Dummy";
        base.moveExecuteList.Add(Move1);
        base.moveExecuteList.Add(Move2);

        base.moveTargetList.Add(Move1Targets);
        base.moveTextList.Add(Move1Text);


        base.nextMove = 0;
        base.selectedTarget = this;
        base.Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Move1(Entity target)
    {
            // is there anyone behind target?
            print("HEYYY!");  
    }

    private void Move2(Entity target)
    {

    }

    private bool Move1Targets(Entity target)
    {
        return true;
    }
    private string Move1Text(int context)
    {
        switch (context)
        {
            case 0: return "this is the move name, Move1, for the menu";
            case 1: return "This is the text we would show once you click it, before selecting targets";
            case 2: return "And this is the text that the move says as the moves are resolving";
        }
        return "code should not ever get to here";
    }


    /*
    private void Move1(Entity target)
    {
            // do i hit anyone else ?
            print("HEYYY!");  
    }
    private bool Move1Targets(Entity target)
    {
       is move 1 legal? if so
        return true;
       else return false;
    }

     */


    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //set selected targets and next move
        int moveCount = base.moveExecuteList.Count - 1;

        base.nextMove = Random.Range(0, moveCount);
        base.hasMoved = false;
    }
}
