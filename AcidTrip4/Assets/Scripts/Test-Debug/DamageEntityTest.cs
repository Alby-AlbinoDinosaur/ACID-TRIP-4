using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEntityTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Entity entity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageEntity(int dmg){
        entity.health_stat -= dmg;
    }

    public void healEntity(int heal){
        entity.health_stat += heal;
    }
}
