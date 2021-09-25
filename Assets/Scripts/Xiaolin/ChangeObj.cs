using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObj : MonoBehaviour
{
    public GameObject Inspection;
    public InspectionObj inspectionObj;
    //public int index;
    private bool BagState;

    // Update is called once per frame
    void Update()
    {
        changeObj();
    }

    private void changeObj()
    {
        BagState = Inspection.activeSelf;
        if (Input.GetKeyDown(KeyCode.A) && BagState == true)
        {
            if (inspectionObj.getCurrIndex() == 0)
            {
                inspectionObj.TurnOnInspection();
            }
            else
            {
                inspectionObj.TurnOffInspection();
                inspectionObj.minusCurrIndex();
                inspectionObj.TurnOnInspection();

            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && BagState == true)
        {
            if (inspectionObj.getCurrIndex() == inspectionObj.getCount() - 1)
            {
                inspectionObj.TurnOnInspection();
            }
            else
            {
                inspectionObj.TurnOffInspection();
                inspectionObj.addCurrIndex();
                inspectionObj.TurnOnInspection();
            }
        }

    }
}
