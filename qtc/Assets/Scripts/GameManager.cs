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

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.SplitScreen;
        silentMode = false;
        shhText.enabled = false;

        StartCoroutine(SilentModeAvailable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SilentModeAvailable()
    {

        yield return new WaitForSeconds(60f);

        silentMode = true;
        shhText.enabled=true;

        yield return new WaitForSeconds(1.5f);

        shhText.enabled = false;
    }
    void EnableSilentMode()
    {
        silentMode = true;
        shhText.enabled = false;

        ChangeState(GameState.SplitScreen);
        Debug.Log("silent mode");
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


                Invoke("EnableSilentMode", 1.5f);
            }
        }
        else
        {
            gameState = newState;

        }
    }
}
