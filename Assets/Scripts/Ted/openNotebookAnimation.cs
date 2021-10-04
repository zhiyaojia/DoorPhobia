using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openNotebookAnimation : MonoBehaviour
{
    public GameObject leftBookPaper;
    public GameObject rightBookPaper;
    public GameObject leftBookCover;
    public GameObject rightBookCover;
    float RotateDegreePaper = 0;
    float RotateDegreeCover = 0;
    float TranslateDistance = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            StartCoroutine("openBook");
        }
        
    }
    IEnumerator openBook()
    {   
        // 旋转速度
        float RotateSpeedCover = 1.1f;
        float RotateSpeedPaper = 1f;
        // 旋转180度
        while (RotateDegreePaper < 85 || RotateDegreeCover < 90) 
        {   
            while (TranslateDistance < 0.055f) {
                leftBookPaper.transform.Translate(0, 0.005f, 0);
                rightBookPaper.transform.Translate(0, 0.005f, 0);
                TranslateDistance += 0.005f;
                yield return null;
            }
            if (RotateDegreePaper < 85) 
            {
                leftBookPaper.transform.Rotate(0, 0, RotateSpeedPaper);
                rightBookPaper.transform.Rotate(0, 0, -RotateSpeedPaper);
                RotateDegreePaper += RotateSpeedPaper;
            }
            if ( RotateDegreeCover < 90) 
            {
                leftBookCover.transform.Rotate(0, 0, -RotateSpeedCover);
                rightBookCover.transform.Rotate(0, 0, RotateSpeedCover);
                RotateDegreeCover += RotateSpeedCover;
            }            
            yield return null;
        }
    }
}
