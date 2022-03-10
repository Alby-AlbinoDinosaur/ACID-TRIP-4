using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndersnailEnemy : Entity
{

    private int scratchDamageDealt;
    public EnemyMover enemyMover;
    bool debugflag = false;
    // Start is called before the first frame update
    void Start()
    {   //Add each move to list

        base.initialize();
        base.moveExecuteList.Add(Bones);
        base.moveExecuteList.Add(Buff);
        base.moveTargetList.Add(BonesTargets);
        base.moveTargetList.Add(BuffTargets);
        base.moveTextList.Add(BonesText);
        base.moveTextList.Add(BuffText);

        base.health_stat = 300;
        base.defense_stat = 20;
        base.spdefense_stat = 30;
        base.attack_stat = 30;
        base.ability_stat = 25;
        base.power_points = 40;
        base.speed_stat = 10;


        base.nextMove = 0;
        base.selectedTarget = this;
        base.name = "Mans";
        enemyMover.addEnemy(this);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Bones(Entity target)
    {
        foreach (Entity enemy in enemyMover.playerMover.playerList)
        {

            int damage = Mathf.Max((int)((base.ability_stat * 2) * (float)(100 - enemy.spdefense_stat) / 100f), 0);
            // Calculate damage however here
            enemy.health_stat -= damage;
        }
        int recoil = Mathf.Max((int)((base.attack_stat * 2) - target.defense_stat), 0);
        // Calculate damage however here
        target.health_stat -= recoil;
    }


    private bool BonesTargets(Entity target)
    {
        return target != this;
    }

    private string BonesText(int context)
    {
        switch (context)
        {
            case 0: return "Summons Bones";
            case 1: return "Summon Bones: Deals collateral damage to entire party.";
            case 2: return base.name + " Summons Bones around " + base.selectedTarget.name + " !";
        }
        return "code should not ever get to here";
    }

    private void Buff(Entity target)
    {
        if (base.power_points >= 5)
        {
            base.power_points -= 5;
            target.attack_stat += 30;
            target.ability_stat += 30;
            target.nextTurnEffects.Enqueue((Entity self) =>
            {
                self.nextTurnEffects.Enqueue((Entity me) =>
                {
                    me.attack_stat -= 30;
                    me.ability_stat -= 30;
                    return me.name + "'s buff wears off.";
                });
                return self.name + "is buffed up!";
            });
        }
    }



    private bool BuffTargets(Entity target)
    {
        return (this.power_points >= 5);
    }

    private string BuffText(int context)
    {
        switch (context)
        {
            case 0: return "Boast";
            case 1: return "Boast: Buffs the next stats ";
            case 2: return base.name + " powers up " + base.selectedTarget.name +"'s next move!";
        }
        return "code should not ever get to here";
    }



    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //set selected targets and next move
        int moveCount = base.moveExecuteList.Count;
        base.selectedTarget = this;
        base.nextMove = 1;
        while (!base.moveTargetList[base.nextMove](base.selectedTarget))
        {
            base.nextMove = Random.Range(0, moveCount);


            print("Enemy selected move");
            if (base.nextMove == 1)
            {
                base.selectedTarget = this;

            }
            else
            {
                int nextTarget = Random.Range(0, 2);

                base.selectedTarget = enemyMover.playerMover.playerList[nextTarget];
                print("Enemy selected target");
            }
        }
        base.nextMoveHasAlreadyBeenRun = false;
    }
}