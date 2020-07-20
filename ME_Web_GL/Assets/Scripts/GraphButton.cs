using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GraphButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public List<Carriage> carriages = new List<Carriage>();

    public void StartStopGraph()
    {
        switch (buttonText.text)
        {
            case "Start Graph":
                StartGraphing();
                buttonText.text = "Stop Graph";
                break;
            case "Stop Graph":
                StopGraphing();
                buttonText.text = "Start Graph";
                break;
        }
    }

    private void StartGraphing()
    {
        int numcarriages = carriages.Count;
        for(int c = 0; c < numcarriages; c++)
        {
            if (!carriages[c].GetLockedStatus())
            {
                carriages[c].StartGraph();
            }
        } 
    }

    private void StopGraphing()
    {
        int numcarriages = carriages.Count;
        for (int c = 0; c < numcarriages; c++)
        {
            carriages[c].StopGraph();
        }
    }
}
