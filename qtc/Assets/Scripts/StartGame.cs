using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public bool restartGame;
    public MusicManager musicManager;
    public AudioClip audioClip;

    private void Start()
    {
        musicManager = GameObject.FindWithTag("MusicManager").GetComponent<MusicManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (restartGame)
            {
                RestartGame();
            }
            else
            {
                startGame();
                Debug.Log("starting");
            }
        }
    }

    public void RestartGame()
    {
        musicManager.ChangeMusic(audioClip);
        SceneManager.LoadScene("startScreen");

    }
    public void startGame()
    {
        SceneManager.LoadScene("catGotYourTongue");
    }
}
