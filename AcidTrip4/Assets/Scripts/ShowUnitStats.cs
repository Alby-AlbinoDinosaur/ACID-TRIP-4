using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShowUnitStat : MonoBehaviour 
{
    [SerializeField]
    protected GameObject unit;
    [SerializeField]
    private float maxValue;
    private Vector2 initialScale;
    void Start () 
    {
        this.initialScale = this.gameObject.transform.localScale;
    }
    void Update () 
    {
        if(this.unit) 
        {
            float newValue = this.newStatValue();
            float newScale = (this.initialScale.x * newValue) / this.maxValue;
            this.gameObject.transform.localScale = new Vector2(newScale, this.initialScale.y);
        }
    }
    public void changeUnit (GameObject newUnit)
    {
        this.unit = newUnit;
    }
    abstract protected float newStatValue();
}
