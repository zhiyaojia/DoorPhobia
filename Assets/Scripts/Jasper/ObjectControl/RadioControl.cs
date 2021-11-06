using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    [Header("Button Settings")]
    public Transform turningButton;
    public float turningSpeed;
    private float currentRotation = 0.0f;
    private Vector3 turningButtonRot;

    [Header("Indicator Settings")]
    public Transform indicator;
    public float startPosition;
    public float endPosition;
    private Vector3 indicatorPosition;

    [Header("AudioSettings")]
    public AudioClip paddingClip;
    public List<AudioClip> audioClipList;
    private AudioSource audioSource;
    private int currentIndex = 0;
    private float paddingLength;
    private int unitRotation;
    private IEnumerator audioCoroutine = null;
    private bool alreadySwitched = false;

    private void Awake()
    {
        if (audioClipList == null || audioClipList.Count <= 0)
        {
            Debug.LogError("Empty Clip List");
        }

        turningButtonRot = turningButton.localRotation.eulerAngles;
        indicatorPosition = indicator.localPosition;

        audioSource = GetComponent<AudioSource>();
        paddingLength = paddingClip.length;
        unitRotation = 360 / audioClipList.Count;
    }

    private void OnEnable()
    {
        audioSource.clip = audioClipList[0];
        audioSource.Play();
    }

    private void OnDisable()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
        }
    }

    private void Update()
    {
        float action = Input.GetAxis("Horizontal");
        if (Mathf.Abs(action) < 0.001f)
        {
            return;
        }

        float targetRotation = currentRotation + action * turningSpeed * Time.deltaTime;
        if (targetRotation >= 360.0f || targetRotation < 0.0f)
        {
            return;
        }

        float currentRemainder = currentRotation % unitRotation;
        float targetRemainder = targetRotation % unitRotation;
        if (Mathf.Abs(currentRemainder - targetRemainder) > 20.0f && alreadySwitched == false)
        {
            currentIndex += currentRemainder > targetRemainder ? 1 : -1;
            audioCoroutine = SwitchMusic();
            StartCoroutine(audioCoroutine);
        }

        currentRotation = targetRotation;
        turningButtonRot.z = currentRotation;
        turningButton.localRotation = Quaternion.Euler(turningButtonRot);

        indicatorPosition.x = Mathf.Lerp(startPosition, endPosition, currentRotation / 360.0f);
        indicator.localPosition = indicatorPosition;
    }

    IEnumerator SwitchMusic()
    {
        audioSource.clip = paddingClip;
        audioSource.Play();
        alreadySwitched = true;

        yield return new WaitForSeconds(paddingLength);

        audioSource.clip = audioClipList[currentIndex];
        audioSource.Play();
        alreadySwitched = false;
    }
}