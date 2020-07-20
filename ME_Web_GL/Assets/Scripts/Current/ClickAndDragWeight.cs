using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDragWeight : MonoBehaviour
{
    public GameObject activateWeight;
    private Vector3 mouseOffset;
    private Vector3 scanPos;
    private Vector3 offset;
    private float mouseZ;

    void OnMouseDown()
    {
        Instantiate(activateWeight, transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousepoint = Input.mousePosition;
        mousepoint.z = mouseZ;
        return Camera.main.ScreenToWorldPoint(mousepoint);

    }

    void OnMouseDrag()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        transform.position = GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        if(gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }
}
