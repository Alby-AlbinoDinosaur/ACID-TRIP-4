using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCheeseEnemy : Entity
{

    private int scratchDamageDealt;
    public EnemyMover enemyMover;
    bool debugflag = false;
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
        base.attack_stat = 15;
        base.ability_stat = 15;
        base.power_points = 30;
        base.speed_stat = 10;


        base.nextMove = 0;
        base.selectedTarget = this;
        base.name = "Shreddar";
        enemyMover.addEnemy(this);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Scratch(Entity target)
    {

        scratchDamageDealt = Mathf.Max(base.attack_stat + Random.Range(0, 30) - target.defense_stat, 0);
        // Calculate damage however here
        target.health_stat -= scratchDamageDealt;
        //print("did a scratch");
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
            case 2: return base.name + " scratches " + base.selectedTarget.name + " ! It does " + scratchDamageDealt + " damage!";
        }
        return "code should not ever get to here";
    }

    private void Bite(Entity target)
    {
        if (base.power_points >= 5)
        {
            base.power_points -= 5;

            if (target.isCheesed) { return; }

            target.isCheesed = true;
            endTurnEffect action = null; //= (Entity self) => { return "dummytext"; };


            action = (Entity self) =>
            {
                int damage = this.ability_stat * Random.Range(0, 4);
                if (damage <= 0)
                {
                    self.isCheesed = false;
                    return "The cheese touch wears off on "+ self.name + ".";
                }
                else
                {
                    self.nextTurnEffects.Enqueue(action);
                    self.health_stat -= (damage);
                    return self.name + " takes " + (damage) + " from the Cheese Touch!";
                }

            };
            target.thisTurnEffects.Enqueue(action);
        }
    }



    private bool BiteTargets(Entity target)
    {
        return (!target.isCheesed && this.power_points >= 5 && target != this);
    }

    private string BiteText(int context)
    {
        switch (context)
        {
            case 0: return "Bite";
            case 1: return "Bite: Infects target with the Cheese Touch.";
            case 2: return base.name + " bites " + base.selectedTarget.name + "! He is cheese touch'd!";
        }
        return "code should not ever get to here";
    }
    


    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //set selected targets and next move
        int moveCount = base.moveExecuteList.Count;
        base.selectedTarget = this;
        while (!base.moveTargetList[base.nextMove](base.selectedTarget))
        {
            base.nextMove = Random.Range(0, moveCount);


            print("Enemy selected move");
            int nextTarget = Random.Range(0, 3);
            print(Random.Range(0, 3));
            print(Random.Range(0, 3));
            print(Random.Range(0, 3));
            print(Random.Range(0, 3));
            print(Random.Range(0, 3));
            print(Random.Range(0, 3));
            print(Random.Range(0, 3));

            base.selectedTarget = enemyMover.playerMover.playerList[nextTarget];
            print("Enemy selected target");
        }
        base.nextMoveHasAlreadyBeenRun = false;
    }
}