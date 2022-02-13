using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMover : MonoBehaviour
{
    public GameObject snailPlate;
    public GameObject leonPlate;
    public GameObject statPlate;
    public Transform focusPoint;

    private Vector3 snailOrigin;
    //private Transform leonOrigin;
    //private Transform statOrigin;


    

    // Start is called before the first frame update
    void Start()
    {
        snailOrigin = snailPlate.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void snailMove()
    {
        Debug.Log("CALLED");
        while(snailPlate.transform.position != focusPoint.position)
        {
            snailPlate.transform.position = Vector3.MoveTowards(snailPlate.transform.position,focusPoint.position,(float)0.1);
            Debug.Log("AA");
        }
    }
    public void snailBack()
    {
        while(snailPlate.transform.position != snailOrigin)
        {
            snailPlate.transform.position = Vector3.MoveTowards(snailPlate.transform.position,snailOrigin,(float)0.1);
        }
    }
}
