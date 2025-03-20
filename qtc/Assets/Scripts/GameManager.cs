using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GameState
{
    MovingTogether,
    SplitScreen,
    ScreenShake,
    Results
}
public class GameManager : MonoBehaviour
{
    public GameState gameState;

    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    public bool silentMode;

    public Animator platformAnim;

    public TextMeshProUGUI shhText;
    public TextMeshProUGUI timerText;

    public float timerDeci;
    public int seconds;

    // Start is called before the first frame update
    void Start()
    {
        timerDeci = 0;
        seconds = 60;

        //Invoke("SilentTimer", .01f);
        Invoke("SilentSeconds", 1f);

        gameState = GameState.SplitScreen;
        silentMode = false;
        shhText.enabled = false;

        StartCoroutine(SilentModeAvailable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SilentTimer()
    {
        if (timerDeci < 1)
        {
            timerDeci++;

            timerText.text = seconds.ToString() + ":" + timerDeci.ToString();
        }
        else
        {
            timerDeci = 0;

            timerText.text = seconds.ToString() + ":00";
        }

        Invoke("SilentTimer", .01f);
    }

    public void SilentSeconds()
    {
        seconds--;
        timerText.text = seconds.ToString() + ":00";

        if (seconds > 0)
        {
            Invoke("SilentSeconds", 1f);
        }
        else
        {
            timerText.gameObject.SetActive(false);
        }
    }

    IEnumerator SilentModeAvailable()
    {

        yield return new WaitForSeconds(60f);

        silentMode = true;
        shhText.enabled=true;
        Debug.Log("silent mode");

        yield return new WaitForSeconds(1.5f);

        shhText.enabled = false;
    }
    void EnableSilentMode()
    {
        silentMode = true;

        ChangeState(GameState.SplitScreen);
        Debug.Log("silent mode");
    }

    void NoShhText()
    {
        shhText.enabled = false;
    }

    public void ChangeState(GameState newState)
    {
        if(newState == GameState.ScreenShake)
        {
            if (silentMode)
            {
                gameState = GameState.ScreenShake;
                silentMode = false;
                platformAnim.Play("platform-shake");
                shhText.enabled = true;

                timerText.gameObject.SetActive(true);
                seconds = 15;
                SilentSeconds();

                Invoke("NoShhText", 1.5f);
                Invoke("EnableSilentMode", 15f);
            }
        }
        else
        {
            gameState = newState;

        }
    }
}
