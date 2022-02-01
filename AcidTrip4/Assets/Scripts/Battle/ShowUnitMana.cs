using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnitMana : ShowUnitStat 
{
    override protected float newStatValue () 
    {
        return unit.GetComponent<UnitStats>().mana;
    }
}
