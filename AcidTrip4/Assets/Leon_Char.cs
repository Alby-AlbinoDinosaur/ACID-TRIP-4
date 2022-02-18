using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leon_Char : Entity
{

    Entity healTarget;
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

        base.health_stat = 150;
        base.defense_stat = 15;
        base.spdefense_stat = 15;
        base.attack_stat = 15;
        base.ability_stat = 15;
        base.power_points = 30;
        base.speed_stat = 15;

        healTarget = this;

        base.name = "Leon Bradley";

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
        base.defense_stat += 20;
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
        base.power_points -= 10;
        endTurnEffect action = null; //= (Entity self) => { return "dummytext"; };


        action = (Entity self) => {

            int damage = (int)((this.ability_stat * 0.3)/100 * self.health_stat);
            if (damage <= 0)
            {
                return self.name + " shakes off paint thinner.";
            }
            else
            {
                self.nextTurnEffects.Enqueue(action);
                self.health_stat -= damage;
                return self.name + " takes " + damage + " from the paint thinner!";
            }
            return "uuuh thinner machine broke";

        };
        target.thisTurnEffects.Enqueue(action);
    }

    private bool Ability_1_Targets(Entity target)
    {
        return target != this && base.power_points >= 10;
    }

    private string Ability_1_Text(int context)
    {
        switch (context)
        {
            case 0: return "Paint Thinner";
            case 1: return "Paint Thinner: Applies damage each turn.";
            case 2: return base.name + " hits " + base.selectedTarget.name + " with thinner!";
        }
        return "oh no, this one's a pain when it breaks";
    }
    //---------------------------------------------------------------------------------------

    //Ability_2 -----------------------------------------------------------------------------
    private void Ability_2(Entity target)
    {
        base.power_points -= 10;
        foreach (Entity current in playerMover.playerList)
        {
            if (current.max_health - current.health_stat <target.max_health - target.health_stat)
            { target = current; }
        }

        healTarget = target;
        healTarget.health_stat = Mathf.Min(target.health_stat + this.ability_stat, target.max_health);

    }

    private bool Ability_2_Targets(Entity target)
    {
        return target == this && base.power_points >= 10;
    }

    private string Ability_2_Text(int context)
    {
        switch (context)
        {
            case 0: return "Healing Paints";
            case 1: return "Healing Paints: Heals the most damaged teammate.";
            case 2: return base.name + " heals " + healTarget.name + "!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------


    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //Nothing
    }
}