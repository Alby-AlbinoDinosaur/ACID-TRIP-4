using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemyGUI : MonoBehaviour
{

    // Start is called before the first frame update
    public Image hpBar;
    
    
    public Button selector;
    public GameObject damagePrefab;

    

    public Entity enemyEntity;
    private int maxHp = 0;
    
    private int currentHp = 0;

    void Start()
    {
        BattleEventManager.OnEnemyRevealSelect+= enableSelector;
        BattleEventManager.OnEnemySelected+= disableSelector;
        BattleEventManager.OnGUIUpdate+= updateGUI;
        BattleEventManager.OnEnemySelectCancel += disableSelector;
        selector.interactable = false;
        maxHp = enemyEntity.health_stat;
        //Debug.Log("max hp initialized to: " + maxHp);
        currentHp = maxHp;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void enableSelector()
    {
        selector.interactable = true;
    }

    void disableSelector()
    {
        selector.interactable = false;
    }

    void updateGUI()
    {
        if(maxHp != 0)
        {
            //Debug.Log("CALLED CURRENHP = " + currentHp + "ENEMY HP: " + enemyEntity.health_stat + "MAX HP: " + maxHp);

            hpBar.fillAmount = (float)enemyEntity.health_stat/maxHp;

            if(enemyEntity.health_stat<currentHp){
                //Debug.Log("DAMAGED");
                GameObject canvas = Instantiate(damagePrefab, gameObject.transform.position, Quaternion.identity);
                //canvas.transform.parent = gameObject.transform;
                TextMeshProUGUI text = canvas.transform.GetChild(0).gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
                text.text = (enemyEntity.health_stat-currentHp).ToString();
            }
            currentHp = enemyEntity.health_stat;
            
        }
        
    }

    private void OnDestroy()
    {
        BattleEventManager.OnEnemyRevealSelect-= enableSelector;
        BattleEventManager.OnEnemySelected-= disableSelector;
        BattleEventManager.OnEnemySelectCancel -= disableSelector;
        BattleEventManager.OnGUIUpdate-= updateGUI;

    }

    
}
