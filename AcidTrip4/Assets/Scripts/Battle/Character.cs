using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharStats stats;
    public int health;
    public int level;



    public Character()
    {
        stats.RandomizeStats();
    }

    public void Attack(Character enemy)
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
