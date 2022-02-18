using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_Char : Entity
{

    public PlayerMover playerMover;

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


        base.name = "Snail";

        base.health_stat = 250;
        base.defense_stat = 20;
        base.spdefense_stat = 8;
        base.attack_stat = 30;
        base.ability_stat = 10;
        base.power_points = 20;
        base.speed_stat = 10;

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
        print("did a scratch");
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
            case 1: return "Defend: protects oneself from physical attacks.";
            case 2: return base.name + " moves to defend!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------

    //Ability_1 -----------------------------------------------------------------------------
    private void Ability_1(Entity target)
    {
        base.power_points -= 3;
        target.beforeMoveEffects.Enqueue((Entity self) =>
        {
            self.selectedTarget = this;
            return self.name + " is enraged at " + this.name + "!";
        });
    }

    private bool Ability_1_Targets(Entity target)
    {
        return target != this && base.power_points >= 3;
    }

    private string Ability_1_Text(int context)
    {
        switch (context)
        {
            case 0: return "Taunt";
            case 1: return "Taunt: forces the enemy to attack " + this.name +".";
            case 2: return base.name + " taunts " +base.selectedTarget.name + "!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------

    //Ability_2 -----------------------------------------------------------------------------
    private void Ability_2(Entity target)
    {
        base.power_points -= 5;
        int damage = Mathf.Max((int)((float)base.defense_stat * 0.75f) - target.defense_stat, 0);
        // Calculate damage however here
        target.health_stat -= damage;
    }

    private bool Ability_2_Targets(Entity target)
    {
        return target != this && base.power_points >= 5;
    }

    private string Ability_2_Text(int context)
    {
        switch (context)
        {
            case 0: return "Shell Bash";
            case 1: return "Shell Bash: attacks with defense stat.";
            case 2: return base.name + " bonks " + base.selectedTarget.name + "!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------


    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //Nothing
    }
}