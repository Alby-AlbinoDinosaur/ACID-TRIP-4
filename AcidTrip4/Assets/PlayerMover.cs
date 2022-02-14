using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public List<Entity> playerList;
    public EnemyMover enemyMover;
    public GameObject endturnbutton;
    public BattleManager battleManager;
    public BattleDialogueManager battleDialogue;

    public Entity SelectedTarget;
    public Entity CurrentSelectedPlayer;
    public int selectedMove;

    public bool EndTurnPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSelectedPlayer(Entity current)
    {
        //To be triggered by the first set of buttons, the ones on your dude's faceplates
        CurrentSelectedPlayer = current;
    }

   public void setSelectedMove(Entity current)
    {
        //To be triggered by the first set of buttons, the ones on your dude's faceplates
        CurrentSelectedPlayer = current;
    }

    bool isAllDefeated()
    {
        int defeatCount = 0;
        foreach (Entity player in playerList)
        {
            if (player.IsDefeated())
                defeatCount++;
        }

        return defeatCount == playerList.Count;
    }

   public void playMoveDescription()
    {
        // To be triggered by any of the move buttons, before pulling up target button list
        battleDialogue.WriteLine(CurrentSelectedPlayer.moveTextList[selectedMove](1));
    }

    public void checkMoveTarget(Entity possible)
    {
        // To be triggered when a player clicks a button in the target list
        if (CurrentSelectedPlayer.moveTargetList[selectedMove](possible))
        {
            SelectedTarget = possible;
            CurrentSelectedPlayer.selectedTarget = possible;
            CurrentSelectedPlayer.nextMove = selectedMove;
            CurrentSelectedPlayer.hasMoved = false;
        }
        else
        {
            battleDialogue.WriteLine("That is an illegal target for that move.");
        }

    }

    public void endTurnButton()
    {
        // to be triggered when "end turn" is hit
        EndTurnPressed = true;
    }

    public bool moveThroughPlayers()
    {
        //Because we have to make players selectable out of order, this function is now just
        //to tell the BattleManager if all players have picked a move.
        //This former loop is mostly in the BattleManager's update function now.

        if (isAllDefeated())
        {
            battleDialogue.WriteLine("You Lose!");
            battleManager.EndBattle();
            return false;
        }

        foreach (Entity current in playerList)
        {
                if (!current.IsDefeated() && current.hasMoved == false)
                { return false; }

        }//foreach

        // TO ADD: Activate end Turn button here

        if (!EndTurnPressed)
        { return false; }

        foreach (Entity current in playerList)
        {
            //put in place in battlemanager list
            BattleManager.instance.AddEntity(current);
        }


        // TO ADD: Deactivate End Turn button here

        return true;
        }

}
