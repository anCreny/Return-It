
using UnityEngine;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform platform;
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        gameObject.transform.position = mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponentInChildren<Animator>().SetBool("OnBeginDrag", true);
        GetComponentInChildren<Animator>().SetBool("OnEndDrag", false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponentInChildren<Animator>().SetBool("OnEndDrag", true);
        GetComponentInChildren<Animator>().SetBool("OnBeginDrag", false);
    }
}