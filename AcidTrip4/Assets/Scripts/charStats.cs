using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Creation")]
public class charStats : MonoBehaviour
{
    public string charName;
    public int attack;
    public int defense;
    public int speed;
    public int maxHealth;

    // test method to debug
    public void PrintMessage()
    {
        Debug.Log("The " + charName)
    }

    // Randomizes stats 	
    public void RandomizeStats() 
    {
        attack = Random.Range(1, 20);
        defense = Random.Range(1, 20);
        speed = Random.Range(1, 20);
        maxHealth = Random.Range(1, 20);
    }
}
