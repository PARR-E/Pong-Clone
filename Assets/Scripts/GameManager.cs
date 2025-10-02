using UnityEngine;
using System;

//Things to add/fix:
//  - Trail not showing up.
//  - Spark effect to only happen on a collision.
//  - Ball to not bounce at certain angles (never bounce too straight or too vertically).
//  - Multiple difficulties for the CPU (easy, normal, caffine)

public class GameManager : MonoBehaviour
{
    //Variables:
    int scoreOfPlayer1 = 0;
    int scoreOfPlayer2 = 0;
    int winScore = 5;
    public PlayMode playMode;

    public enum PlayMode
    {
        PlayerVsCPU,
        PlayerVsPlayer
    }

    public static GameManager _instance;            //This is used for the singleton.
    public Action<int, int> OnScoreChanged;         //The service game manager will provide for incrementing score. <int, int> means methods we call with this need two int parameters.
    public Action<int> OnWinDetected;               //Service for handling when game ends.
    public Action OnGameStart;                      //Service game manager will provide for starting the game.

    //Method for being a singleton:
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    //This is also needed to be a singleton:
    private void Awake()
    {
        //Alternate code to try and prevent objects from not being cleaned up.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        //Dr. Zheng's version:
        /*if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);*/
    }

    //If scored in left zone, increment right player's score. Else if scored in right zone, increment left score.
    public void SetScores(string zoneTag)
    {
        if (zoneTag == "LeftZone")
        {
            scoreOfPlayer2++;
        }
        else if (zoneTag == "RightZone")
        {
            scoreOfPlayer1++;
        }
        //Call UpdateScoreBoard in GameUIController
        OnScoreChanged?.Invoke(scoreOfPlayer1, scoreOfPlayer2);     //In GameUI, add subscription to this signal. Subscription version of: gameUI.UpdateScoreBoard(scoreOfPlayer1, scoreOfPlayer2);
    }

    //Reset both scores back to 0:
    public void ResetScores()
    {
        scoreOfPlayer1 = 0;
        scoreOfPlayer2 = 0;
        OnScoreChanged?.Invoke(scoreOfPlayer1, scoreOfPlayer1);     //Make sure UI is updated to reflect this.
    }

    //Check if either player has gotten the number of points needed to win.
    public bool CheckWin()
    {
        int winnerID = scoreOfPlayer1 == winScore ? 1 : scoreOfPlayer2 == winScore ? 2 : 0;
        //              if (statement)   equal this. else (statement),           equal this.
        //If player 1 has max points, winnerID equals 1. Else if player 2 has max points, winnerID equals 1. Else, winnerID equals 0. 

        //If a player has won, run the OnWin() method from GameUIController.
        if (winnerID != 0)
        {
            OnWinDetected?.Invoke(winnerID);        //Subscription version of: gameUI.OnWin(winnerID);
            return true;
        }
        return false;
    }


}
