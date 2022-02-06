using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Entity Stats
    public int speed_stat;
    public int health_stat;

    //Return type: 
    public delegate bool Move(List<Entity> selectedTargets, bool shouldExecute);
    public Move nextMove;
    public List<Entity> selectedTargets;
    public List<Move> moveList;

    public void Run()
    {
        nextMove(selectedTargets, true);
    }

    public bool IsDefeated()
    {
        return health_stat <= 0;
    }


    //Get the list of moves for ui
    public List<Move> GetMoveList()
    {
        return moveList;
    }

    //For enemies
    public abstract void AutoChooseNextMove(List<Entity> possibleTargets);

}
