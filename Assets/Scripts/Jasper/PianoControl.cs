using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PianoControl : MonoBehaviour
{
    public List<int> LyricSequence;
    public List<KeyCode> KeycodeSequence;

    public Text text; // temporal debug

    private int CurrentIndex;
    private PianoInteract pianoInteract;

    void Start()
    {
        CurrentIndex = 0;
        pianoInteract = GetComponent<PianoInteract>();
    }

    void Update()
    {
        for (int i = 0; i < KeycodeSequence.Count; i++)
        {
            if (Input.GetKeyDown(KeycodeSequence[i]) == true)
            {
                if (LyricSequence[CurrentIndex] == i + 1)
                {
                    CurrentIndex++;
                }
                else
                {
                    CurrentIndex = 0;
                }
            }
        }
        if (CurrentIndex == LyricSequence.Count)
        {
            CurrentIndex = 0;
            StartCoroutine(FinishLyric());
        }
    }

    IEnumerator FinishLyric()
    {
        yield return new WaitForSeconds(1);
        pianoInteract.StopFocuOnView();
        enabled = false;
    }
}
