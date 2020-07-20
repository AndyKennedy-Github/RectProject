using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeScript : MonoBehaviour
{
    public enum edgeType {left, right};
    public edgeType currentEdge;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.name.Contains("We"))
        {
            if (other.gameObject.GetComponent<MoveCarriage>() != null)
            {
                other.gameObject.GetComponentInParent<MoveCarriage>().isTouchingEdge = true;
                if (currentEdge == edgeType.left)
                {
                    other.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(1);
                }
                else if (currentEdge == edgeType.right)
                {
                    other.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(2);
                }

            }
            else if (other.gameObject.GetComponent<MoveCarriage>() == null && other.transform.parent.gameObject.GetComponent<MoveCarriage>() != null)
            {
                other.transform.parent.gameObject.GetComponent<MoveCarriage>().isTouchingEdge = true;
                Debug.Log(other.transform.parent.gameObject.GetComponent<MoveCarriage>().isTouchingEdge);
                if (currentEdge == edgeType.left)
                {
                    other.transform.parent.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(1);
                }
                else if (currentEdge == edgeType.right)
                {
                    other.transform.parent.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(2);
                }
            }
            else if (other.gameObject.GetComponent<MoveCarriage>() == null && other.transform.parent.gameObject.GetComponent<MoveCarriage>() == null)
            {
                other.transform.parent.parent.gameObject.GetComponent<MoveCarriage>().isTouchingEdge = true;
                if (currentEdge == edgeType.left)
                {
                    other.transform.parent.parent.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(1);
                }
                else if (currentEdge == edgeType.right)
                {
                    other.transform.parent.parent.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(2);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MoveCarriage>() != null)
        {
            other.gameObject.GetComponentInParent<MoveCarriage>().isTouchingEdge = false;
            other.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(0);
        }
        else if (other.gameObject.GetComponent<MoveCarriage>() == null)
        {
            other.transform.parent.parent.gameObject.GetComponentInParent<MoveCarriage>().isTouchingEdge = false;
            other.transform.parent.parent.gameObject.GetComponentInParent<MoveCarriage>().SetEdge(0);
        }
    }
}
