//RUN EVENTS WHEN ON START FUNCTION IS CALLED - Good for scene initializations and such

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class OnStartEvents : MonoBehaviour
{
    //Event for when conversation begins -------------------------------------------
    [System.Serializable]
    public class StartFunctionEvent : UnityEvent { }

    [FormerlySerializedAs("onStart")]
    [SerializeField]
    private StartFunctionEvent onStart = new StartFunctionEvent();
    //--------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        onStart.Invoke();   
    }
}
