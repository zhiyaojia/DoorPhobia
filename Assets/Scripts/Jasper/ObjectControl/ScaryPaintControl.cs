using UnityEngine;

public class ScaryPaintControl : MonoBehaviour
{
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 playerForward = PlayerControl.Instance.playerCamera.transform.forward;
        playerForward.y = 0.0f;
        Vector3 paintForward = transform.up;
        paintForward.y = 0.0f;
        float dot = Vector3.Dot(playerForward, paintForward);
        if (dot >= 0.5f)
        {
            audio.Play();
            enabled = false;
        }
    }
}
