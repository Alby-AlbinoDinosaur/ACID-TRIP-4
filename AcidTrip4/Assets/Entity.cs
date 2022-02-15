using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Entity Stats
    public int speed_stat = 0;
    public int health_stat = 100;
    public int defense_stat = 0;
    public int attack_stat = 0;
    public int ability_stat = 0;
    public int power_points = 0;
    public string name = "uh oh, we forgot to name this one";

    //These three functions make up the three parts of a move
    //moveExecute executes the move on selectedTargets when called
    //moveTargets returns all the targets that would be affected by a move if that entity were clicked on as a target
    //moveText spits out the right text for the situation given the context, possibly using selectedTargets
    public delegate void moveExecute(Entity target);
    public delegate bool moveTarget(Entity potentialTarget);
    public delegate string moveText(int context);

    public List<moveExecute> moveExecuteList;
    public List<moveTarget> moveTargetList;
    public List<moveText> moveTextList;

    public int nextMove = -1;
    public Entity selectedTarget;

    //hasMoved means it has already used the nextMove & selectedTarget
    public bool nextMoveHasAlreadyBeenRun = true;


    public void Run()
    {
        moveExecuteList[nextMove](selectedTarget);
        nextMoveHasAlreadyBeenRun = true;
        
    }

    public bool IsDefeated()
    {
        return health_stat <= 0;
    }


    //Get the list of moves for ui
    public List<moveTarget> GetMoveList()
    {
        return moveTargetList;
    }

    //For enemies
    public abstract void AutoChooseNextMove(List<Entity> playerList, List<Entity> enemyList);



    //Default constructor: call by hand before start in individual enemies
    public void initialize()
    {
        moveExecuteList = new List<moveExecute>();
        moveTargetList = new List<moveTarget>();
        moveTextList = new List<moveText>();
    }




}
