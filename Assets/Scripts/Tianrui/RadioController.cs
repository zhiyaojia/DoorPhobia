using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    public GameObject TuningKnob;
    public GameObject Indicator;
    private AudioSource Audio1;
    private AudioSource Audio2;
    private AudioSource Audio3;
    private float KnobSpeed = -0.01f;
    private float IndicatorSpeed = 18f;
    private float KnobPosition = 0f;
    private float IndicatorPosition = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (Indicator.transform.localPosition.x >= -0.1f)
            {
                TuningKnob.transform.Rotate(0, 0, 0.18f);
                Vector3 vector = Indicator.transform.localPosition;
                Indicator.transform.localPosition = new Vector3(vector.x - 0.0001f, vector.y, vector.z);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Indicator.transform.localPosition.x <= 0.1f)
            {
                TuningKnob.transform.Rotate(0, 0, -0.18f);
                Vector3 vector = Indicator.transform.localPosition;
                Indicator.transform.localPosition = new Vector3(vector.x + 0.0001f, vector.y, vector.z);
            }
        }
        Debug.Log("1");
        if (TuningKnob.transform.localEulerAngles.z>=0&& TuningKnob.transform.localEulerAngles.z < 120)
        {
            Debug.Log("play Audio1");
        }
        else if(TuningKnob.transform.localEulerAngles.z>=120 && TuningKnob.transform.localEulerAngles.z < 240)
        {
            Debug.Log("play Audio2");
        }
        else if(TuningKnob.transform.localEulerAngles.z>=240&& TuningKnob.transform.localEulerAngles.z < 360)
        {
            Debug.Log("play Audio3");
        }
    }
}
