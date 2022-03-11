using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConkhEnemy : Entity
{
    private int scratchDamageDealt;
    public EnemyMover enemyMover;
    bool debugflag = false;
    // Start is called before the first frame update
    void Start()
    {   //Add each move to list

        base.initialize();
        base.moveExecuteList.Add(Bash);
        base.moveExecuteList.Add(Song);
        base.moveTargetList.Add(BashTargets);
        base.moveTargetList.Add(SongTargets);
        base.moveTextList.Add(BashText);
        base.moveTextList.Add(SongText);

        base.health_stat = 300;
        base.defense_stat = 20;
        base.spdefense_stat = 30;
        base.attack_stat = 30;
        base.ability_stat = 25;
        base.power_points = 20;
        base.speed_stat = 5;


        base.nextMove = 0;
        base.selectedTarget = this;
        base.name = "Conche";
        enemyMover.addEnemy(this);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Bash(Entity target)
    {

        scratchDamageDealt = Mathf.Max(base.attack_stat + Random.Range(0, 30) - target.defense_stat, 0);
        // Calculate damage however here
        target.health_stat -= scratchDamageDealt;

        this.defense_stat -= 30;
        this.spdefense_stat -= 10;
        // Calculate damage however here
        this.thisTurnEffects.Enqueue((Entity self) =>
        {
            self.defense_stat += 30;
            self.spdefense_stat += 10;
            return self.name + " puts its shell back on.";
        });
        //print("did a scratch");
    }


    private bool BashTargets(Entity target)
    {
        return target != this;
    }

    private string BashText(int context)
    {
        switch (context)
        {
            case 0: return "Shell Bash";
            case 1: return "Shell Bash: A strong attack, but leaves the user vulnerable.";
            case 2: return base.name + " bashes " + base.selectedTarget.name + " ! It does " + scratchDamageDealt + " damage!";
        }
        return "code should not ever get to here";
    }

    private void Song(Entity target)
    {
        if (base.power_points >= 5)
        {
            base.power_points -= 5;
            foreach (Entity enemy in enemyMover.enemyList)
            {
                if (enemy == this)
                {
                    int healAmount = Random.Range(1, 3);
                    target.health_stat = Mathf.Min(target.health_stat + this.ability_stat * healAmount, target.max_health);
                }
                else
                {
                    int healAmount = Random.Range(1, 3);
                    enemy.health_stat = Mathf.Min(enemy.health_stat + this.ability_stat * healAmount, enemy.max_health);
                }
            }
            
        }
    }



    private bool SongTargets(Entity target)
    {
        if (this.power_points >= 5)
        {
            foreach (Entity enemy in enemyMover.enemyList)
            {
                if (enemy.health_stat < enemy.max_health) { return true; }
            }
        }
        return false;
    }

    private string SongText(int context)
    {
        switch (context)
        {
            case 0: return "Song";
            case 1: return "Song: Heals entire enemy team.";
            case 2: return base.name + " soothes your opponents with its voice!";
        }
        return "code should not ever get to here";
    }



    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //set selected targets and next move
        int moveCount = base.moveExecuteList.Count;
        base.selectedTarget = this;
        base.nextMove = 0;
        while (!base.moveTargetList[base.nextMove](base.selectedTarget))
        {
            base.nextMove = Random.Range(0, moveCount);

            print("Enemy selected move");
            int nextTarget = Random.Range(0, 3);

            if (base.nextMove == 1)
            {
                base.selectedTarget = this;
            }
            else
            {
                base.selectedTarget = enemyMover.playerMover.playerList[nextTarget];
            }
            print("Enemy selected target");
        }
        base.nextMoveHasAlreadyBeenRun = false;
    }
}