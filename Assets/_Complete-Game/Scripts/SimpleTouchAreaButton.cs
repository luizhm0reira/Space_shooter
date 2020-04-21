using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    
    public bool touched;
    private int pointerID;
    private bool canFire;
    public Color pressed;
    private Color color;
    
    void Awake ()
    {
        touched = false;
        color = GetComponent<Image>().color;
    }    
    public void OnPointerDown (PointerEventData data) {
        if (!touched) {
            touched = true;
            pointerID = data.pointerId;
            canFire = true;
            GetComponent<Image>().color = pressed;
        }
    }    
    public void OnPointerUp (PointerEventData data) {
        if (data.pointerId == pointerID) {
            canFire = false;
            touched = false;
            GetComponent<Image>().color = color;
        }
    }
    public bool CanFire () {
        return canFire;
    }
}