using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{       
    int CurrentChosenWheel = 0;
    public GameObject Wheel1;
    public GameObject Wheel2;
    public GameObject Wheel3;
    public GameObject MetalPiece;
    public int PasswordDigit1;
    public int PasswordDigit2;
    public int PasswordDigit3;
    int Wheel1Num = 0;
    int Wheel2Num = 0;
    int Wheel3Num = 0;
    float RotateDegree = 0;
    float TranslateDistance = 0;
    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.W))
        {
            CurrentChosenWheel--;
            if (CurrentChosenWheel < 0) CurrentChosenWheel = 2;
        }        
        if (Input.GetKeyDown(KeyCode.S))
        {
            CurrentChosenWheel++;
            if (CurrentChosenWheel > 2) CurrentChosenWheel = 0;
        }        
        if (Input.GetKeyDown(KeyCode.A))
        {
            switch (CurrentChosenWheel)
            {
                case 0:
                    LockRotate(Wheel1, true);
                    Wheel1Num--;
                    if (Wheel1Num < 0) Wheel1Num = 9;
                    break;
                case 1:
                    LockRotate(Wheel2, true);
                    Wheel2Num--;
                    if (Wheel2Num < 0) Wheel2Num = 9;
                    break;
                case 2:
                    LockRotate(Wheel3, true);
                    Wheel3Num--;
                    if (Wheel3Num < 0) Wheel3Num = 9;
                    break; 
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            switch (CurrentChosenWheel)
            {
                case 0:
                    LockRotate(Wheel1, false);
                    Wheel1Num++;
                    if (Wheel1Num > 9) Wheel1Num = 0;
                    break;
                case 1:
                    LockRotate(Wheel2, false);
                    Wheel2Num++;
                    if (Wheel2Num > 9) Wheel2Num = 0;
                    break;
                case 2:
                    LockRotate(Wheel3, false);
                    Wheel3Num++;
                    if (Wheel3Num > 9) Wheel3Num = 0;
                    break; 
            }
        }
        if (ValidatePassword() == true) 
        {
            Unlock();
        }
    }
    void LockRotate(GameObject Wheel, bool Direction)
    {
        if (Direction == false) 
        {   
            Wheel.transform.Rotate(0, 36, 0);                     
        }
        else
        {            
            Wheel.transform.Rotate(0, -36, 0);            
        }
    }
    bool ValidatePassword()
    {
        if (Wheel1Num == PasswordDigit1 && Wheel2Num == PasswordDigit2 && Wheel3Num == PasswordDigit3) 
        {
            return true;
            Debug.Log("CurrentChosenWheel:" + CurrentChosenWheel);
            Debug.Log("Correct");
        }
        return false;
    }
    void Unlock()
    {
        StartCoroutine("TranslateAndRotateMetal");
    }
    IEnumerator TranslateAndRotateMetal()
    {   
        while (TranslateDistance < 0.01f) {
            MetalPiece.transform.Translate(0, 0.0001f, 0);
            TranslateDistance += 0.0001f;
            yield return null;
        }
        
        float RotateSpeed = 0.001f;        
        while (RotateDegree < 180) 
        {
            MetalPiece.transform.Rotate(0, RotateSpeed, 0);
            RotateDegree += RotateSpeed;
            yield return null;
        }
    }
}
