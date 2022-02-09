using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolEnemy1 : Entity
{
    

    // Start is called before the first frame update
    void Start()
    {   //Add each move to list
        base.initialize();

        base.name = "Dummy :)";
        base.moveExecuteList.Add(Move1);
        base.moveTargetsList.Add(Move1Targets);
        base.moveTextList.Add(Move1Text);
        base.nextMove = 0;
        base.selectedTargets = Move1Targets(this);
        base.Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move1(List<Entity> targetList)
    {
            print("HEYYY!");  
    }

    private List<Entity> Move1Targets(Entity target)
    {
        return new List<Entity>();
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

    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //set selected targets and next move
        base.nextMove = 0;//temp
    }
}
