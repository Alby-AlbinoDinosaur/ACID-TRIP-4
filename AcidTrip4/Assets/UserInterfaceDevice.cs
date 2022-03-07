using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInterfaceDevice : MonoBehaviour
{
    public static UserInterfaceDevice instance;
    private bool mouseMode = false; //true: mouse mode for ui buttons; false: controller or keyboard mode for ui buttons

    private GameObject selectedObj; //The last thing that was selected by SetSelectedUI()


    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Start()
    {
        //When a scene starts
        selectedObj = EventSystem.current.firstSelectedGameObject;
    }

    void Update()
    {
        //0 left click, 1 right click, 2 middle click
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (!mouseMode)
            {
                //Set to mousemode for ui buttons
                mouseMode = true;
            }
            
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
        {
            //Horizontal and Vertical works with arrow keys, wasd, and controller by default

            if (mouseMode)
            {
                //Set to keyboard or controller mode for ui buttons
                mouseMode = false;

                if (selectedObj != null)
                {
                    SetSelectedUI(selectedObj);
                }
            }

            
        }

    }

    public bool isMouseMode()
    {
        return mouseMode;
    }


    //Selects a UI interactable object
    public void SetSelectedUI(GameObject gameObj)
    {
        selectedObj = gameObj;

        if (!mouseMode)
        {
            //Select Button
            EventSystem.current.SetSelectedGameObject(gameObj);

            //Highlight Button
            gameObj.GetComponent<Selectable>().OnSelect(null);
        }
        
    }

    //Deselects a UI object
    public void DeselectUI()
    {
        selectedObj = null;

        //Deselect Button
        EventSystem.current.SetSelectedGameObject(null);
    }

    //Sets player or enemy button selected; Selects the first player or enemy that is not defeated in the list
    //playerOrEnemy: true, player, false: enemy
    public void SetEntitySelected(bool playerOrEnemy)
    {
        List<Entity> entityList = null;

        if (playerOrEnemy)
        {
            //get player list
            entityList = BattleManager.instance.playerMover.playerList;

        }
        else
        {
            //get enemy list
            entityList = BattleManager.instance.enemyMover.enemyList;
        }

        if (entityList != null)
        {
            //Select the first undefeated enemy/player in the list of entities
            foreach (Entity entity in entityList)
            {
                if (!entity.IsDefeated())
                {
                    //Select the child object button of the enemy/player
                    GameObject buttonObj = entity.GetComponentInChildren<Button>().gameObject;
                    SetSelectedUI(buttonObj);

                    break;
                }
            }
        }
        else
        {
            Debug.Log("EntityList is Null!");
        }

    }

    public GameObject GetLastSelected()
    {
        return selectedObj;
    }

}


