using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance { get; set; }

    [Header("Basic Settings")]
    public GameObject Menu;
    public GameObject Tutorial;
    private bool isPaused = false;

    [Header("Blink Settings")]
    public bool hasBlink = true;
    private BlinkControl blinkControl;

    private AudioSource playingAudio;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Tutorial.SetActive(true);

        blinkControl = PlayerControl.Instance.playerCamera.GetComponent<BlinkControl>();
        if (hasBlink == true)
        { 
            blinkControl.enabled = true;
            blinkControl.OpenEye();
        }
        else
        {
            PlayerControl.Instance.SetCrossHair(true);
            PlayerControl.Instance.TurnOnControl();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            PauseGame();

            playingAudio = null;
            AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audio in sources)
            {
                if (audio.isPlaying == true)
                {
                    playingAudio = audio;
                    audio.Pause();
                }
            }
        }
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Menu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

        if (playingAudio != null)
        {
            playingAudio.UnPause();
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Menu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void GameEnd()
    {
        StartCoroutine(StartEndGame());
    }

    IEnumerator StartEndGame()
    {
        PlayerControl.Instance.TurnOffControl();
        yield return new WaitForSeconds(2.0f);
        blinkControl.enabled = true;
        blinkControl.CloseEye();
    }
}
