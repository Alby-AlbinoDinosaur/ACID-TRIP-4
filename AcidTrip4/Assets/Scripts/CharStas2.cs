using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;

// Used for non-scripatble objects
public class CharStats2 : MonoBehaviour, IComparable 
{
    public string charName;
    public int attack;
    public int defense;
    public int speed;
    public int maxHealth;
    public int currHealth;

    public int nextActTurn;
    private bool dead = false;
    // test method to debug
    public void PrintMessage()
    {
        Debug.Log("The " + charName);
    }

    // Randomizes stats 	
    public void RandomizeStats() 
    {
        attack = Random.Range(1, 20);
        defense = Random.Range(1, 20);
        speed = Random.Range(1, 20);
        maxHealth = Random.Range(1, 20);
        currHealth = Random.Range(1, 20);
    }

    // Adds specified ints to stats
    public void ChangeStats(int att, int def, int spd, int hp)
    {
        attack += att;
        defense += def;
        speed += spd;
        maxHealth += hp;
        currHealth += hp;
    }

    // used for turn order
    public void calculateNextActTurn(int currentTurn){
        this.nextActTurn = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
    }

    // compares stats to another objects stats
    public int CompareTo (object otherStats)
    {
        return nextActTurn.CompareTo(((CharStats)otherStats).nextActTurn);
    }

    // checks to see if char is alive
    public bool isDead(){
        return this.dead;
    }
}