using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    public GameObject lockImage, unlockImage;

    public Carriage carriage;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(carriage.GetLockedStatus())
        {
            lockImage.SetActive(true);
            unlockImage.SetActive(false);
        }
        else if(!carriage.GetLockedStatus())
        {
            lockImage.SetActive(false);
            unlockImage.SetActive(true);
        }
    }
}
