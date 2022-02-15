using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Char : Entity
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

        base.name = "Statistician";

        base.nextMove = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Attack --------------------------------------------------------------------------------
    private void Attack(Entity target)
    {
        int damage = 5;
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
            case 0: return "Scratch";
            case 1: return "Scratch: Does a basic physical attack to the target.";
            case 2: return "Cheese Enemy moves Scratch " + base.selectedTarget.name + " !";
        }
        return "code should not ever get to here";
    }
    //---------------------------------------------------------------------------------------

    //Defend --------------------------------------------------------------------------------
    private void Defend(Entity target)
    {

    }

    private bool DefendTargets(Entity target)
    {
        return true;
    }

    private string DefendText(int context)
    {
        return "placeholder";
    }
    //---------------------------------------------------------------------------------------

    //Ability_1 -----------------------------------------------------------------------------
    private void Ability_1(Entity target)
    {

    }

    private bool Ability_1_Targets(Entity target)
    {
        return true;
    }

    private string Ability_1_Text(int context)
    {
        return "placeholder";
    }
    //---------------------------------------------------------------------------------------

    //Ability_2 -----------------------------------------------------------------------------
    private void Ability_2(Entity target)
    {

    }

    private bool Ability_2_Targets(Entity target)
    {
        return true;
    }

    private string Ability_2_Text(int context)
    {
        return "placeholder";
    }
    //---------------------------------------------------------------------------------------


    public override void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList)
    {
        //Nothing
    }
}