using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetButton : MonoBehaviour
{
    public TextMeshProUGUI resetText;

    public Carriage carriage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!carriage.canReset)
        {
            resetText.text = "Release";
        }
        else if (carriage.canReset)
        {
            resetText.text = "Reset";
        }
        
    }
}
