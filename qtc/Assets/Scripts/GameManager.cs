using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TextMeshProUGUI zeroes;

    public GameObject shhIcon;

    public float timerDeci;
    public int seconds;

    public Sprite redF1;
    public Sprite redF2;
    public Sprite yellowF1;
    public Sprite yellowF2;
    public Sprite greenF1;
    public Sprite greenF2;
    public Sprite blueF1;
    public Sprite blueF2;

    public GameObject[] redButtons;
    public GameObject[] yellowButtons;
    public GameObject[] greenButtons;
    public GameObject[] blueButtons;

    public AudioSource shhSFX;
    

    // Start is called before the first frame update
    void Start()
    {
        timerDeci = 0;
        seconds = 60;

        redButtons = GameObject.FindGameObjectsWithTag ("redButton");
        yellowButtons = GameObject.FindGameObjectsWithTag ("yellowButton");
        greenButtons = GameObject.FindGameObjectsWithTag ("greenButton");
        blueButtons = GameObject.FindGameObjectsWithTag ("blueButton");

        //Invoke("SilentTimer", .01f);
        Invoke("SilentSeconds", 1f);

        gameState = GameState.SplitScreen;
        silentMode = false;
        shhText.enabled = false;
        shhIcon.gameObject.SetActive(false);

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

            timerText.text = seconds.ToString();
        }

        Invoke("SilentTimer", .01f);
    }

    public void SilentSeconds()
    {
        seconds--;
        timerText.text = seconds.ToString();

        if (seconds > 0)
        {
            Invoke("SilentSeconds", 1f);
        }
        else
        {
            timerText.gameObject.SetActive(false);
            zeroes.gameObject.SetActive(false);
        }
    }

    IEnumerator SilentModeAvailable()
    {

        yield return new WaitForSeconds(60f);

        silentMode = true;
        shhText.enabled=true;
        shhIcon.gameObject.SetActive(true);
        shhSFX.Play();
        Debug.Log("silent mode");

        yield return new WaitForSeconds(1.5f);

        shhText.enabled = false;
        shhIcon.gameObject.SetActive(false);
    }
    void EnableSilentMode()
    {
        silentMode = true;

        Debug.Log("silent mode");
    }

    void NoShhText()
    {
        shhText.enabled = false;
        shhIcon.gameObject.SetActive(false);
        ChangeState(GameState.SplitScreen);
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
                shhIcon.gameObject.SetActive(true);
                shhSFX.Play();
                buttonflashAnimation();

                timerText.gameObject.SetActive(true);
                zeroes.gameObject.SetActive(true);
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

    void buttonflashAnimation()
    {
        StartCoroutine(redflashAnim());
        StartCoroutine(yellowflashAnim());
        StartCoroutine(greenflashAnim());
        StartCoroutine(blueflashAnim());
    }

    void flashAnimation(GameObject[] buttonType, Sprite frame)
    {
        buttonType[0].GetComponent<Image>().sprite = frame;
        buttonType[1].GetComponent<Image>().sprite = frame;
    }


    IEnumerator redflashAnim()
    {
        for (int i = 0; i < 5; i ++)
        {
            flashAnimation(redButtons, redF1);
            yield return new WaitForSeconds(0.1f);
            flashAnimation(redButtons, redF2);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator yellowflashAnim()
    {
        for (int i = 0; i < 5; i ++)
        {
            flashAnimation(yellowButtons, yellowF2);
            yield return new WaitForSeconds(0.1f);
            flashAnimation(yellowButtons, yellowF1);
            yield return new WaitForSeconds(0.1f);
        }
        flashAnimation(yellowButtons, yellowF2);
    }

    IEnumerator greenflashAnim()
    {
        for (int i = 0; i < 5; i ++)
        {
            flashAnimation(greenButtons, greenF1);
            yield return new WaitForSeconds(0.1f);
            flashAnimation(greenButtons, greenF2);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator blueflashAnim()
    {
        for (int i = 0; i < 5; i ++)
        {
            flashAnimation(blueButtons, blueF2);
            yield return new WaitForSeconds(0.1f);
            flashAnimation(blueButtons, blueF1);
            yield return new WaitForSeconds(0.1f);
        }
        flashAnimation(blueButtons, blueF2);
    }
}
