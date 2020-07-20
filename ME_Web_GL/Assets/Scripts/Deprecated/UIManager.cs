using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject carriage;

    public GameObject GetCurrentCarriage()
    {
        return carriage;
    }

    public void SetCurrentCarriage(GameObject g)
    {
        if(g.name.Contains("Carriage"))
        {
            carriage = g;
        }
    }

    public bool GetCarriageLockStatus()
    {
        return carriage.GetComponent<Carriage>().GetLockedStatus();
    }
}
