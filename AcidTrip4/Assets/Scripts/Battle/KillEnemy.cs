using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public GameObject menuItem;

    void OnDestroy(){
        Destroy(this.menuItem);
    }
}
