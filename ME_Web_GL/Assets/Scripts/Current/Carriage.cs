using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Carriage : MonoBehaviour
{
    public int weightTotal = 4, carriageNumber;
    public List<GameObject> weights = new List<GameObject>();
    public Collider leftCol, rightCol;
    private Rigidbody rb;
    private bool isLocked;
    public bool canReset;
    public float amountPerWeight = 1.0f, baseWeight = 1.0f;
    public UIManager uim;
    public TextMeshProUGUI weightText, distText;

    public Vector3 startingPosition;
    public GraphManager graphManager;       // the graph manager for this carriage

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isLocked = true;
        startingPosition = gameObject.transform.position;
    }

    void Update()
    {
        DisplayDistance();
        EnableColliders();
    }

    void FixedUpdate()
    { 
        rb.mass = baseWeight + (1 * weightTotal * amountPerWeight);
        if(weightTotal == 3)
        {
            weightText.text = "40 gram(s)";
        }
        else
        {
            weightText.text = (Mathf.Ceil(rb.mass * 1000)).ToString() + " gram(s)";
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ActivateWeight") && weightTotal < 4)
        {
            AddWeight();
            Destroy(other.gameObject);
        }
    }

    void OnMouseDown()
    {
        if(!canReset)
        {
            uim.SetCurrentCarriage(gameObject);
        }
    }

    void DisplayDistance()
    {
        if (Vector3.Distance(startingPosition, this.transform.position) < 0.001)
        {
            distText.text = "0 cm";
        }
        else
        {
            distText.text = (Mathf.Floor(Vector3.Distance(startingPosition, this.transform.position) * 100)).ToString() + " cm";
        }
    }

    void EnableColliders()
    {
        if(uim.GetCurrentCarriage() != this.gameObject)
        {
            if(canReset)
            {
                leftCol.isTrigger = false;
                rightCol.isTrigger = false;
            }
            else if(!canReset)
            {
                leftCol.isTrigger = true;
                rightCol.isTrigger = true;
            }
        }
        else
        {
            leftCol.isTrigger = false;
            rightCol.isTrigger = false;
        }
    }

    public void RemoveWeight()
    {
         if (weightTotal > 0)
         {
             weights[weightTotal - 1].SetActive(false);
             weightTotal--;
         }
    }

    public void RemoveWeight(int n)
    {
        if (weightTotal > 0)
        {
            for(int i = 4; i < n; i--)
            {
                weights[i].SetActive(false);
                weightTotal--;
            }
        }
    }

    public void AddWeight()
    {
         if (weightTotal < 4)
         {
             weightTotal++;
             weights[weightTotal - 1].SetActive(true);
         }
    }

    public void LockUnlock()
    {
         if (isLocked)
         { 
             isLocked = false;
         }
         else
         {
             isLocked = true;
         }
    }

    public void ReleaseAndReset()
    {
        if (uim.GetCurrentCarriage() == gameObject)
        {
            if (rb.constraints == RigidbodyConstraints.FreezeAll)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            }
            else if (rb.constraints != RigidbodyConstraints.FreezePositionX)
            {
                rb.velocity = Vector3.zero;
                gameObject.transform.position = startingPosition;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        canReset = !canReset;
    }

    public bool GetLockedStatus()
    {
        return isLocked;
    }

    /// <summary>
    /// Starts the graphing for this carriage.
    /// </summary>
    public void StartGraph()
    {
        graphManager.ClearGraph();
        graphManager.okToGraph = true;
        graphManager.RestartPointTimer();
    }

    /// <summary>
    /// Stops the graphing of this carriage.
    /// </summary>
    public void StopGraph()
    {
        graphManager.okToGraph = false;
    }
}
