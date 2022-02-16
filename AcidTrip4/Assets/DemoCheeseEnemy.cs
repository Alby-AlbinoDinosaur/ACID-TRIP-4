using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCheeseEnemy : Entity
{

    private int scratchDamageDealt;
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

        base.health_stat = 150;
        base.defense_stat = 15;
        base.spdefense_stat = 15;
        base.attack_stat = 10;
        base.ability_stat = 15;
        base.power_points = 50;
        base.speed_stat = 10;


        base.nextMove = 0;
        base.name = "Shreddar";
        enemyMover.addEnemy(this);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Scratch(Entity target)
    {

        scratchDamageDealt = base.attack_stat + 30 - target.defense_stat;
        // Calculate damage however here
        target.health_stat -= Mathf.Min(scratchDamageDealt, 0);
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
            case 2: return base.name +" scratches " + base.selectedTarget.name + " ! It does " + Mathf.Min(scratchDamageDealt,0) + " damage!";
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
        base.nextMove = 0;

        print("Enemy selected move");
        int nextTarget = Random.Range(0, 2);

        base.selectedTarget = enemyMover.playerMover.playerList[nextTarget];
        print("Enemy selected target");

        base.nextMoveHasAlreadyBeenRun = false;
    }
}