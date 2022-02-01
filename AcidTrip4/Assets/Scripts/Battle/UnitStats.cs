using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;

// Used for non-scripatble objects
public class UnitStats : MonoBehaviour, IComparable 
{
    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed;

    public int nextActTurn;
    private bool dead = false;
    
    public void calculateNextActTurn (int currentTurn)
    {
        this.nextActTurn = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
    }
    public int CompareTo (object otherStats)
    {
        return nextActTurn.CompareTo(((UnitStats)otherStats).nextActTurn);
    }
    public bool isDead ()
    {
        return this.dead;
    }
    public void receiveDamage (float damage) 
    {
        this.health -= damage;
        // animator.Play("Hit");
        // GameObject HUDCanvas = GameObject.Find("HUDCanvas");
        // GameObject damageText = Instantiate(this.damageTextPrefab, HUDCanvas.transform) as GameObject;
        // damageText.GetComponent<Text>().text = "" + damage;
        // damageText.transform.localPosition = this.damageTextPosition;
        // damageText.transform.localScale = new Vector2(1.0f, 1.0f);
        if(this.health <= 0)
        {
            this.dead = true;
            this.gameObject.tag = "DeadUnit";
            Destroy(this.gameObject);
        }
    }
}