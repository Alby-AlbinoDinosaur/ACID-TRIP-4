using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class EnemyTest : MonoBehaviour
{

    public GameObject damagePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(int dmg)
    {
        GameObject canvas = Instantiate(damagePrefab, gameObject.transform.position, Quaternion.identity);
        TextMeshProUGUI text = canvas.transform.GetChild(0).gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        text.text = dmg.ToString();


    }
}
