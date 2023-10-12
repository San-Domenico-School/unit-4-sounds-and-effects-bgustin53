using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/******************************************************
* This class is a component of the Scorekeeper GameObject
* and is designed to keeps track of and display the 
* score for the game
******************************************************/
public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreboardText;  //Reference to the Scoreboard TextMeshProUGUI GameObject
    private static float score;                               //Player's current score
    private static int collectible;                           //Player's current score

    private void Update()
    {
        DisplayScore();
    }

    // Displays rounded score to UI by updating scoreboardText
    public void DisplayScore()
    {
        int roundedScore = Mathf.RoundToInt(score);
        scoreboardText.text = "Score: " + roundedScore.ToString();
    }

    //Inputs horizontal axis value received from the playerController script to an exponential
    //function whose values range from 0.00 to 0.10.  
    public static void ChangeScore(int change)
    {
        //float exponentialScale = Mathf.Pow(axisInput, 2) * 0.10f;
        score += change;
    }
}