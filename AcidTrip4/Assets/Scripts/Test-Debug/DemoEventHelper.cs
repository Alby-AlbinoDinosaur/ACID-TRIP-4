using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEventHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject masterGameObject;
    void Start()
    {  
        masterGameObject.SetActive(false);
        DemoEventManager.OnBattleStart +=enableBattle;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void enableBattle()
    {
        masterGameObject.SetActive(true);
    }
}
