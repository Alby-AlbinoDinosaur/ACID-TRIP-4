using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Entity Stats
    public int speed_stat;
    public int health_stat;

    //These three functions make up the three parts of a move
    //moveExecute executes the move on selectedTargets when called
    //moveTargets returns all the targets that would be affected by a move if that entity were clicked on as a target
    //moveText spits out the right text for the situation given the context, possibly using selectedTargets
    public delegate void moveExecute(List<Entity> selectedTargets);
    public delegate List<Entity> moveTargets(Entity potentialTarget);
    public delegate string moveText(int context);

    public List<moveExecute> moveExecuteList;
    public List<moveTargets> moveTargetsList;
    public List<moveText> moveTextList;

    public int nextMove;
    public List<Entity> selectedTargets;

    public void Run()
    {
        moveExecuteList[nextMove](selectedTargets);
    }

    public bool IsDefeated()
    {
        return health_stat <= 0;
    }


    //Get the list of moves for ui
    public List<moveTargets> GetMoveList()
    {
        return moveTargetsList;
    }

    //For enemies
    public abstract void AutoChooseNextMove(List<Entity> possibleTargets);



    //Default constructor: call by hand before start in individual enemies
    public void initialize()
    {
        moveExecuteList = new List<moveExecute>();
        moveTargetsList = new List<moveTargets>();
        moveTextList = new List<moveText>();
    }




}
