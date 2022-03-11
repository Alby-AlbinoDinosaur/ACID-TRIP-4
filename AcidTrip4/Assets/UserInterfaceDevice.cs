using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInterfaceDevice : MonoBehaviour
{
    public static UserInterfaceDevice instance;
    private bool mouseMode = true; //true: mouse mode for ui buttons; false: controller or keyboard mode for ui buttons

    private GameObject selectedObj; //The last thing that was selected by SetSelectedUI().

    //Priority list for player selection once returning to main battle menu state (ex: after picking an attack)
    //Should have all the players in the battle but in order of highest selection priority (top = highest)
    [SerializeField]
    private List<Entity> playerSelectionPriority;

    //Give an initial item to last selected (default to mousemode)
    //To have an initial selection in controller mode, give the EventSystem firstSelectedObject a value
    //Important: EventSystem firstSelectedObject if not null, will override this value
    [SerializeField]
    private GameObject initialSelectedObj; 

    void Awake()
    {
        instance = this;

        //NOOO
        /*DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }*/

    }

    private void Start()
    {
        //Try to use event system's first selected, else try to use the initialSelectedObject of this script
        if (EventSystem.current.firstSelectedGameObject != null)
        {
            selectedObj = EventSystem.current.firstSelectedGameObject;
        }
        else
        {
            selectedObj = initialSelectedObj;
        }
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
            if (playerOrEnemy)
            {
                //FOR PLAYERS
                //New method, go off priority list
                foreach (Entity entity in playerSelectionPriority)
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
                //FOR ENEMIES
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

    //Set a gameobject to be selected once entered controller mode
    public void SetLastSelected(GameObject obj)
    {
        selectedObj = obj;
    }

}


