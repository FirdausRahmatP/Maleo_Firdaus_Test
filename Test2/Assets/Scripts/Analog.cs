using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Analog : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public float range;
    public GameObject analog,joystick;
    public PointerEventData pointer;

    public UnityEvent onAnalogDown, onAnalogUp;
    public UnityEvent<Vector2> onAnalogDrag;


    private void Update()
    {
        if(pointer != null)
        {
            Vector2 center = (Vector2)analog.transform.position;
            Vector2 direction = (pointer.position - center).normalized;
            if (Vector2.Distance(pointer.position, center) <= range)
                joystick.transform.position = pointer.position;
            else
            {
                joystick.transform.position = center + (direction*range);

            }
            onAnalogDrag?.Invoke(direction);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointer = eventData;
        analog.transform.position = eventData.position;
        joystick.transform.position = eventData.position;
        analog.SetActive(true);
        onAnalogDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointer = null;
        analog.SetActive(false);
        onAnalogUp?.Invoke();
    }

}
