using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitAction : MonoBehaviour 
{
    [SerializeField]
    private GameObject physicalAttack;
    [SerializeField]
    private GameObject magicalAttack;
    private GameObject currentAttack;
    
    public void selectAttack (bool physical) 
    {
        this.currentAttack = (physical) ? this.physicalAttack : this.magicalAttack;
    }
    void Awake () 
    {
        this.physicalAttack = Instantiate(this.physicalAttack, this.transform) as GameObject;
        this.magicalAttack = Instantiate(this.magicalAttack, this.transform) as GameObject;
        this.physicalAttack.GetComponent<AttackTarget>().owner = this.gameObject;
        this.magicalAttack.GetComponent<AttackTarget>().owner = this.gameObject;
        this.currentAttack = this.physicalAttack;
    }
    public void act (GameObject target) 
    {
        this.currentAttack.GetComponent<AttackTarget>().hit(target);
    }
}