using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            Drag draggableItem = dropped.GetComponent<Drag>();
            draggableItem.parentAfterDrag = transform;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

}
