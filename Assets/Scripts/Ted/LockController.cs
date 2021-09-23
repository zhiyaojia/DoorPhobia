using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{       
    public int CurrentChosenWheel = 0;
    public int CurrentWheelNumber = 0;
    public GameObject Wheel1;
    public GameObject Wheel2;
    public GameObject Wheel3;
    
    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Debug.Log("W down");
            CurrentChosenWheel--;
        }        
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Debug.Log("S down");
            CurrentChosenWheel++;
        }        
        CurrentChosenWheel %= 3;
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Debug.Log("A down");
            Debug.Log("CurrentChosenWheel:" + CurrentChosenWheel);
            CurrentWheelNumber--;
            if (CurrentChosenWheel == 0) LockRotate(Wheel1, true);
            if (CurrentChosenWheel == 1) LockRotate(Wheel2, true);
            if (CurrentChosenWheel == 2) LockRotate(Wheel3, true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Debug.Log("D down");
            Debug.Log("CurrentChosenWheel:" + CurrentChosenWheel);
            CurrentWheelNumber++;
            if (CurrentChosenWheel == 0) LockRotate(Wheel1, false);
            if (CurrentChosenWheel == 1) LockRotate(Wheel2, false);
            if (CurrentChosenWheel == 2) LockRotate(Wheel3, false);
        }
    }
    void LockRotate(GameObject Wheel, bool Direction){
        float Degree = 0;
        if (Direction == false) 
        {   
            Wheel.transform.Rotate(0, 36, 0);
            Degree += 1;           
        }
        else
        {            
            Wheel.transform.Rotate(0, -36, 0);
            Degree += 1;             
        }
    }
}
