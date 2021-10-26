using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStats : MonoBehaviour
{
    public CharStats charStat;
    public Text nameText;
    public Text attackText;
    public Text defenseText;
    public Text speedText;
    public Text maxHealthText;
    // Start is called before the first frame update
    void Start()
    {
        DisplayStats();
	    charStat.PrintMessage();
    }

     	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charStat.RandomizeStats();
            DisplayStats();
        }
    }

    void DisplayStats()
    {
        nameText.text = "Name: " + charStat.charName;
        attackText.text = "Attack: " + charStat.attack;
        defenseText.text = "Defense: " + charStat.defense;
        speedText.text = "Speed: " + charStat.speed;
        maxHealthText.text = "Max Health: " + charStat.maxHealth;
    }

    
}
