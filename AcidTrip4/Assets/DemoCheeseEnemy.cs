using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCheeseEnemy : Entity
{

    public EnemyMover enemyMover;
    // Start is called before the first frame update
    void Start()
    {   //Add each move to list
        base.initialize();
        base.moveExecuteList.Add(Scratch);
        base.moveExecuteList.Add(Bite);
        base.moveTargetList.Add(ScratchTargets);
        base.moveTargetList.Add(BiteTargets);
        base.moveTextList.Add(ScratchText);
        base.moveTextList.Add(BiteText);


        base.nextMove = 0;

        enemyMover.addEnemy(this);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Scratch(Entity target)
    {
        int damage = 5;
        // Calculate damage however here
        target.health_stat -= damage;
        print("did a scratch");
    }


    private bool ScratchTargets(Entity target)
    {
        return target != this;
    }

    private string ScratchText(int context)
    {
        switch (context)
        {
            case 0: return "Scratch";
            case 1: return "Scratch: Does a basic physical attack to the target.";
            case 2: return "Cheese Enemy moves Scratch " + base.selectedTarget.name + " !";
        }
        return "code should not ever get to here";
    }

    private void Bite(Entity target)
    {

    }

    private bool BiteTargets(Entity target)
    {
        return true;
    }

    private string BiteText(int context)
    {
        return "placeholder";
    }
    


    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //set selected targets and next move
        int moveCount = base.moveExecuteList.Count - 1;

        base.nextMove = Random.Range(0, moveCount);

        print("Enemy selected move");
        int nextTarget = Random.Range(0, 0);

        base.selectedTarget = enemyMover.playerMover.playerList[nextTarget];
        print("Enemy selected target");

        base.nextMoveHasAlreadyBeenRun = false;
    }
}