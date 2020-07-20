using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWeights : MonoBehaviour
{
    public int weightNumber;
    public Carriage carriage;
    public GameObject activateWeight;
    GameObject moveWeight;
    private Vector3 mouseOffset;
    private Vector3 scanPos;
    private Vector3 offset;
    private float mouseZ;

    void OnMouseDown()
    {
        Debug.Log("I am clicking on the weight I want to remove!");
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = moveWeight.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousepoint = Input.mousePosition;
        mousepoint.z = mouseZ;
        return Camera.main.ScreenToWorldPoint(mousepoint);

    }

    void OnMouseDrag()
    {
        moveWeight = Instantiate(activateWeight);
        Debug.Log(moveWeight.activeInHierarchy);
        carriage.RemoveWeight(weightNumber);
        moveWeight.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        moveWeight.transform.position = GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        if (gameObject.activeSelf)
        {
            Destroy(moveWeight);
        }
    }
}
