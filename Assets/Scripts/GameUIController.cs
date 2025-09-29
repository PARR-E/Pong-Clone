using UnityEngine;
using TMPro;
using System;

public class GameUIController : MonoBehaviour
{    
    public TextMeshProUGUI winText;                                     //Text that is displayed when the game is completed.
    
    //Subscribers:
    private void OnEnable(){
        GameManager.Instance.OnScoreChanged += UpdateScoreBoard;        //Observer.
        GameManager.Instance.OnWinDetected += OnWin;                    //Oberver.
        GameManager.Instance.OnGameStart += HideMenu;
    }

    //Update the score display according to the current score values of Player 1 and 2:
    public void UpdateScoreBoard(int scoreOfPlayer1, int scoreOfPlayer2){
        TextMeshProUGUI textComponent;

        Transform child1 = transform.GetChild(1);
        Transform child2 = transform.GetChild(2);
        
        textComponent = child1.GetComponent<TextMeshProUGUI>();
        textComponent.text = scoreOfPlayer1.ToString();
        textComponent = child2.GetComponent<TextMeshProUGUI>();
        textComponent.text = scoreOfPlayer2.ToString();
    }

    //Hide the start text:
    private void HideMenu() {
        transform.GetChild(3).gameObject.SetActive(false);  //Hide the starting UI. 3 means the 3rd child of gameUI (indexing from 0).
    }

    //What happens when the start button is clicked:
    public void OnStartButtonClick(){
        GameManager.Instance.ResetScores();                 //Reset scores back to 0.
        GameManager.Instance.OnGameStart?.Invoke();         //Subscriber version of: ball.Serve();
    }

    //Display the winning message when the game ends:
    public void OnWin(int winnerID){
        winText.text = $"Player {winnerID} wins!";
        transform.GetChild(3).gameObject.SetActive(true);  //Show the starting UI. 3 means the 3rd child of gameUI (indexing from 0).
    }

    //Avoid memory leaks:
    private void OnDisable(){
        GameManager.Instance.OnScoreChanged -= UpdateScoreBoard;
        GameManager.Instance.OnWinDetected -= OnWin;
        GameManager.Instance.OnGameStart -= HideMenu;            
    }
}
