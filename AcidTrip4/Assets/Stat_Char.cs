using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Char : Entity
{

    public PlayerMover playerMover;
    string zeroedStat;
    // Start is called before the first frame update
    void Start()
    {
        //Add each move to list
        base.initialize();

        //Add abilities
        base.moveExecuteList.Add(Attack);
        base.moveExecuteList.Add(Defend);
        base.moveExecuteList.Add(Ability_1);
        base.moveExecuteList.Add(Ability_2);

        //Add targets for each ability
        base.moveTargetList.Add(AttackTargets);
        base.moveTargetList.Add(DefendTargets);
        base.moveTargetList.Add(Ability_1_Targets);
        base.moveTargetList.Add(Ability_2_Targets);

        //Add ability text
        base.moveTextList.Add(AttackText);
        base.moveTextList.Add(DefendText);
        base.moveTextList.Add(Ability_1_Text);
        base.moveTextList.Add(Ability_2_Text);

        base.name = "Statistician";

        base.health_stat = 100;
        base.defense_stat = 8;
        base.spdefense_stat = 20;
        base.attack_stat = 10;
        base.ability_stat = 30;
        base.power_points = 50;
        base.speed_stat = 20;

        base.nextMove = 0;

        playerMover.addPlayer(this);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Attack --------------------------------------------------------------------------------
    private void Attack(Entity target)
    {
        int damage = Mathf.Max(base.attack_stat + 15 - target.defense_stat, 0);
        // Calculate damage however here
        target.health_stat -= damage;
    }

    private bool AttackTargets(Entity target)
    {
        return target != this;
    }

    private string AttackText(int context)
    {
        switch (context)
        {
            case 0: return "Attack";
            case 1: return "Attack: Does a basic physical attack to the target.";
            case 2: return base.name + " moves to attack " + base.selectedTarget.name + " !";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------

    //Defend --------------------------------------------------------------------------------
    private void Defend(Entity target)
    {
        target.defense_stat += 20;
        target.beforeMoveEffects.Enqueue((Entity self) =>
        {
            self.defense_stat -= 20;
            return self.name + "'s defense wears off.";
        });
    }

    private bool DefendTargets(Entity target)
    {
        return target == this;
    }

    private string DefendText(int context)
    {
        switch (context)
        {
            case 0: return "Defend";
            case 1: return "Defend: protects oneself from physical attacks .";
            case 2: return base.name + " moves to defend!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------

    //Ability_1 -----------------------------------------------------------------------------
    private void Ability_1(Entity target)
    {
        base.power_points -= 15;
        int random = Random.Range(0, 8);
        switch (random)
        {
        case 0: target.health_stat = 0;
            zeroedStat = "health";
            return;
        case 1:
            target.speed_stat = 0;
            zeroedStat = "speed";
            return;
        case 2:
            target.defense_stat = 0;
            zeroedStat = "defense";
            return;
        case 3:
            target.spdefense_stat = 0;
            zeroedStat = "special defense";
            return;
        case 4:
            target.attack_stat = 0;
            zeroedStat = "attack";
            return;
        case 5:
            target.ability_stat = 0;
            zeroedStat = "ability";
            return;
        case 6:
            target.power_points = 0;
            zeroedStat = "power points";
            return;
        case 7:
            target.name = "0";
            zeroedStat = "name";
            return;
        }
    }

    private bool Ability_1_Targets(Entity target)
    {
        return base.power_points >= 15;
    }

    private string Ability_1_Text(int context)
    {
        switch (context)
        {
            case 0: return "Divide by zero";
            case 1: return "Sets one of the target's stats at random to zero.";
            case 2: return base.selectedTarget.name + "'s " + zeroedStat + " was set to 0!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------

    //Ability_2 -----------------------------------------------------------------------------
    private void Ability_2(Entity target)
    {
        base.power_points -= 7;
        int damage = Mathf.Max((int)((base.ability_stat * Mathf.PI /5) * (float)(100 - target.spdefense_stat) / 100f), 0);
        // Calculate damage however here
        target.health_stat -= damage;
    }

    private bool Ability_2_Targets(Entity target)
    {
        return base.power_points >= 7;
    }

    private string Ability_2_Text(int context)
    {
        switch (context)
        {
            case 0: return "Maethstrom";
            case 1: return "Hits all enemies for decreasing damage.";
            case 2: return base.name + " starts doing math spins!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------


    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //Nothing
    }
}