using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveCarriage : MonoBehaviour
{
    public GameObject carriage, detectCollider, carriageUI, weight;
    private GameObject moveWeight;
    private Vector3 mouseOffset;
    private Vector3 scanPos;
    private Vector3 offset;
    private float mouseZ;
    private bool isLocked, canMove;
    public bool isTouchingEdge;
    public Carriage cScript;
    private Rigidbody rb;
    public int edgeInt = 0;
    public Transform carRight, carLeft;

    void Start()
    {
        SetEdge(0);
        rb = gameObject.GetComponent<Rigidbody>();

        if(carRight == null)
        {
            return;
        }

        if (carLeft == null)
        {
            return;
        }
    }

    void Update()
    {
        GenerateUIPos();
        EdgeCorrection();
    }

    void OnMouseDown()
    {
        if(cScript.canReset == false)
        {
            if (!isLocked)
            {
                mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mouseOffset = gameObject.transform.position - GetMouseWorldPos();

            }
            else if (isLocked && cScript.weightTotal > 0)
            {
                Debug.Log(transform.position);
                moveWeight = Instantiate(weight, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                moveWeight.GetComponent<Collider>().enabled = false;
                mouseZ = Camera.main.WorldToScreenPoint(moveWeight.transform.position).z;
                mouseOffset = moveWeight.transform.position - GetMouseWorldPos();
                cScript.RemoveWeight();
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousepoint = Input.mousePosition;
        mousepoint.z = mouseZ;
        return Camera.main.ScreenToWorldPoint(mousepoint);
    }

    void OnMouseDrag()
    {
        if (cScript.canReset == false)
        {
            if (canMove)
            {
                if (!isLocked)
                {
                    transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, transform.position.y, transform.position.z);
                }
                if (isLocked && moveWeight != null)
                {
                    moveWeight.transform.position = GetMouseWorldPos();
                }
            }
            else if (!canMove)
            {
                if (GetMouseWorldPos().magnitude < weight.transform.position.magnitude || GetMouseWorldPos().magnitude > weight.transform.position.magnitude)
                {
                    if (GetMouseWorldPos().magnitude == weight.transform.position.magnitude)
                    {
                        canMove = true;
                    }
                }
            }
        }     
    }

    void OnMouseUp()
    {
        canMove = true;
        if(isLocked)
        {
            if (moveWeight != null && moveWeight.activeInHierarchy)
            {
                Destroy(moveWeight);
            }
        }
    }

    public void SetEdge(int i)
    { 
       edgeInt = i;
    }

    void EdgeCorrection()
    {
        if(cScript.carriageNumber == 0)
        {
            if(edgeInt == 1)
            {
                transform.position = new Vector3(.52f, transform.position.y, transform.position.z);
                canMove = false;
            }
            if(edgeInt == 2)
            {
                transform.position = new Vector3(carLeft.position.x, transform.position.y, transform.position.z);
                canMove = false;
            }
            
        }

        if(cScript.carriageNumber == 1)
        {
            if (edgeInt == 1)
            {
                transform.position = new Vector3(carRight.position.x, transform.position.y, transform.position.z);
                canMove = false;
            }
            if (edgeInt == 2)
            {
                transform.position = new Vector3(carLeft.position.x, transform.position.y, transform.position.z);
                canMove = false;
            }
        }

        if(cScript.carriageNumber == 2)
        {
            if (edgeInt == 1)
            {
                transform.position = new Vector3(carRight.position.x, transform.position.y, transform.position.z);
                canMove = false;
            }
            if (edgeInt == 2)
            {
                transform.position = new Vector3(-.52f, transform.position.y, transform.position.z);
                canMove = false;
            }
        }
        if(isLocked)
        {
            canMove = true;
        }
    }

    void GenerateUIPos()
    {
        carriageUI.transform.position = new Vector3(gameObject.transform.position.x, carriageUI.transform.position.y, gameObject.transform.position.z);
        isLocked = cScript.GetLockedStatus();
        if (!isLocked)
        {
            detectCollider.SetActive(false);
            carriageUI.transform.GetChild(1).gameObject.SetActive(true);
            carriageUI.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (isLocked)
        {
            detectCollider.SetActive(true);
            carriageUI.transform.GetChild(1).gameObject.SetActive(false);
            carriageUI.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
