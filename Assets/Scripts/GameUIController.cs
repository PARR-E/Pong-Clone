using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public ScoreTextController scoreTextPlayer1, scoreTextPlayer2;
    public GameObject gameMenu;
    public BallController ball;
    public TextMeshProUGUI winText;
    
    public void UpdateScoreBoard(int scoreOfPlayer1, int scoreOfPlayer2){
        scoreTextPlayer1.SetScore(scoreOfPlayer1);
        scoreTextPlayer2.SetScore(scoreOfPlayer2);
    }

    public void OnStartButtonClick(){
        gameMenu.SetActive(false);
        ball.Serve();
        
    }

    public void OnWin(int winnerID){
        gameMenu.SetActive(true);
        winText.text = $"Player {winnerID} wins!";
    }
}
