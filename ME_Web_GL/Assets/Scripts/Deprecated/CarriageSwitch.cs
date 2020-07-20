using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageSwitch : MonoBehaviour
{

    public UIManager uim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                uim.SetCurrentCarriage(hit.collider.gameObject.transform.parent.transform.parent.gameObject);
            }
        }
    }
}
