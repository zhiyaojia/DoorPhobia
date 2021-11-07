using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance { get; set; }

    public GameObject Menu;
    public GameObject Tutorial;
    public GameObject GameEnd;
    private bool isPaused = false;

    public AudioSource playingAudio;

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
        //Tutorial.SetActive(true);
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

    public void GameEnds()
    {
        GameEnd.SetActive(true);
    }
}
