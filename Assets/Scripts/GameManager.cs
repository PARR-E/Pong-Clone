using UnityEngine;

public class GameManager : MonoBehaviour
{
    int scoreOfPlayer1 = 0;
    int scoreOfPlayer2 = 0;
    //public ScoreTextController scoreTextLeft, scoreTextRight;
    public GameUIController gameUI;
    int winScore = 5;
    
    public void SetScores(string zoneTag)
    {
        if(zoneTag == "LeftZone"){
            scoreOfPlayer2 ++;
        }
        else if(zoneTag == "RightZone"){
            scoreOfPlayer1 ++;
        }

        gameUI.UpdateScoreBoard(scoreOfPlayer1, scoreOfPlayer2);
    }

    public bool CheckWin(){
        int winnerID = scoreOfPlayer1 == winScore ? 1 : scoreOfPlayer2 == winScore ? 2 : 0;
        //              if (statement)   equal this. else (statement),           equal this. 
        
        if(winnerID != 0){
            gameUI.OnWin(winnerID);
            return true;
        }
        return false;
    }
}
