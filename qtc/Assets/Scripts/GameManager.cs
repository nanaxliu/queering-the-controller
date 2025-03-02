using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeState()
    {

    }
}
