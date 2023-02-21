using UnityEngine;
using UnityEngine.EventSystems;

public class RacketTestMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    
    public void OnDrag(PointerEventData eventData)
    {
        var position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Camera.main.nearClipPlane));
        position.z = 0f;
        transform.position = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
