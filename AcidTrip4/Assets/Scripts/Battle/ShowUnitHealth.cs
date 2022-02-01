using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnitHealth : ShowUnitStat 
{
    override protected float newStatValue () 
    {
        return unit.GetComponent<UnitStats>().health;
    }
}
