using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBag : MonoBehaviour
{
    public GameObject Inspection;
    public InspectionObj inspectionObj;
    public int index;

    // Update is called once per frame
    void Update()
    {
        if (Inspection.active)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Inspection.SetActive(false);
            inspectionObj.TurnOffInspection();
        }
    }
}
