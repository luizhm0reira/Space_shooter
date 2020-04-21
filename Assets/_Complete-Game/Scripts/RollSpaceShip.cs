using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollSpaceShip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
 
public bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
    public Done_PlayerController control;
    // Start is called before the first frame update
    void Start()
    {        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Roll()
    {
        control.Roll();
        var go = EventSystem.current.currentSelectedGameObject;
        if (go != null)
            Debug.Log("Clicked on : " + go.name);
        else
            Debug.Log("currentSelectedGameObject is null");
    }
}
