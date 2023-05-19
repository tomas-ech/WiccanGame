using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public GameObject referenceObj;
    private SpawnManager spawnManager;

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
            referenceObj = draggableItem.reference;
            Debug.Log(referenceObj);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

}
