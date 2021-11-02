using UnityEngine;

public class GhostDoorControl : MonoBehaviour
{
    public AudioClip suddenDoorCloseSound;

    private Animation doorAnimation;
    private AudioSource doorAudioSource;
    public SingleOpenDoorInteract doorInteract;

    void Start()
    {
        doorAnimation = GetComponentInParent<Animation>();
        doorAudioSource = GetComponentInParent<AudioSource>();
        //doorInteract = transform.parent.gameObject.GetComponentInChildren<SingleOpenDoorInteract>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            doorAnimation.Play("SuddenLeftClose");
            doorAudioSource.clip = suddenDoorCloseSound;
            doorAudioSource.Play();
            gameObject.SetActive(false);
            doorInteract.enabled = true;
            doorInteract.Lock();
        }
    }
}
