using UnityEngine;

public class GameManager : MonoBehaviour
{
    int scoreOfPlayer1 = 0;
    int scoreOfPlayer2 = 0;
    public ScoreTextController scoreTextLeft, scoreTextRight;
    
    public void SetScores(string zoneTag)
    {
        if(zoneTag == "LeftZone"){
            scoreOfPlayer2 ++;
        }
        else if(zoneTag == "RightZone"){
            scoreOfPlayer1 ++;
        }

        UpdateScoreBoard();
    }

    private void UpdateScoreBoard(){
        scoreTextLeft.SetScore(scoreOfPlayer1);
        scoreTextRight.SetScore(scoreOfPlayer2);
    }
}
