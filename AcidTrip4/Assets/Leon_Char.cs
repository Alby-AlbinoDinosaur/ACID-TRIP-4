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
        isAttacking = true;
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
        base.power_points = Mathf.Min(this.power_points + 3, this.max_pp);
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
            case 0: return "Guard";
            case 1: return "Guard: protects oneself from physical attacks.";
            case 2: return base.name + " moves to defend!";
        }
        return "uh oh move broke";
    }
    //---------------------------------------------------------------------------------------

    //Ability_1 -----------------------------------------------------------------------------
    private void Ability_1(Entity target)
    {
        isAttacking = true;
        base.power_points -= 10;
        target.thinnerstacks++;
        if (target.thinnerstacks == 1)
        {
            endTurnEffect action = null; //= (Entity self) => { return "dummytext"; };
            action = (Entity self) => {

                int damage = (int)((this.ability_stat * 0.6) / 100 * self.health_stat);
                int wearoff = Random.Range(0, 4);
                if (wearoff == 0) {
                    self.thinnerstacks--;
                }
                if (self.thinnerstacks > 0)
                {
                    self.nextTurnEffects.Enqueue(action);
                    self.health_stat -= damage * self.thinnerstacks;
                    return self.name + " takes " + damage * self.thinnerstacks + " from the thinner!";

                }
                return self.name + " is no longer covered in thinner.";
                return "uuuh thinner machine broke";

            };
            target.nextTurnEffects.Enqueue(action);
        }
        endTurnEffect reaction = null; //= (Entity self) => { return "dummytext"; };


        reaction = (Entity self) =>
        {
            int damage = (int)((this.ability_stat * 0.3) / 100 * self.health_stat);
            self.health_stat -= damage * self.thinnerstacks;
            return self.name + " takes " + damage * self.thinnerstacks + " from being splashed with thinner!";
        };

        target.thisTurnEffects.Enqueue(reaction);
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
            case 1: return "10 PP: Applies damage each turn.";
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
            if (current.max_health - current.health_stat >target.max_health - target.health_stat)
            { target = current; }
        }

        healTarget = target;
        int healAmount = Random.Range(1, 4);
        healTarget.health_stat = Mathf.Min(target.health_stat + this.ability_stat * healAmount, target.max_health);

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
            case 1: return "10 PP: Heals the most damaged teammate.";
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