
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private float _screenHeight;

    private void Start()
    {
        _screenHeight = Camera.main.ScreenToWorldPoint( new Vector3(0,Camera.main.pixelHeight * 0.35f,0)).y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.y > _screenHeight)
        {
            mousePosition.y = _screenHeight;
        }
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