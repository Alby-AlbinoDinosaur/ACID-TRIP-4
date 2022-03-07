using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class RouteSelection : Selectable, ISelectHandler// required interface when using the OnSelect method.
{
    public Selectable[] selectableUIList;


   
    override
    public void OnSelect(BaseEventData eventData)
    {
        bool selectPrevious = true;

        //Reroute selection to first active selectable object
        foreach (Selectable element in selectableUIList)
        {
            //If gameobject and it's parent is active & if the selectable component is active
            //& if the selectable component has interactable set to true
            if (element.gameObject.activeInHierarchy && element.IsActive() && element.IsInteractable())
            {
                selectPrevious = false;
                StartCoroutine(SetSelected(element));
                //print(element.gameObject.name);
                break;
            }

        }

        if (selectPrevious)
        {
            selectPrevious = false;
            StartCoroutine(SetSelected(UserInterfaceDevice.instance.GetLastSelected().GetComponent<Selectable>()));
        }

    }

    private IEnumerator SetSelected(Selectable element)
    {
        //Wait one frame so that it does not try to select when another object is still selected
        yield return new WaitForEndOfFrame();
        UserInterfaceDevice.instance.SetSelectedUI(element.gameObject);
    }


}


