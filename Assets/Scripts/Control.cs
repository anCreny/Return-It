
using UnityEngine;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject selectedObject;
    

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        gameObject.transform.position = mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}