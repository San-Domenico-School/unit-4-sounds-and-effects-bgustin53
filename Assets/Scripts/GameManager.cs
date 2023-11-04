using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreboardText;
    [SerializeField] private TextMeshProUGUI timeRemainingText;
    [SerializeField] private GameObject toggleGroup;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem dirtSplatter;

    public static bool gameOver = true;
    private static float score;
    private int timeRemaining = 60;
    private bool timedGame;

    // Update is called once per frame
    void Update()
    {
        DisplayUI();
    }

    private void DisplayUI()
    {
        scoreboardText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        if(timedGame)
        {
            if(timeRemaining == 0)
            {
                timeRemainingText.text = "Game\nOver";
            }
            else
            {
                timeRemainingText.text = timeRemaining.ToString();
            }
        }
    }

    private void TimeCountdown()
    {
        timeRemaining--;
        if (gameOver || timeRemaining == 0)
        {
            gameOver = true;
            playerAnimator.SetFloat("Speed_f", 0);
            playerAnimator.SetBool("BeginGame_b", false);
            CancelInvoke();
        }

    }

    public void StartGame()
    {
        toggleGroup.SetActive(false);
        startButton.SetActive(false);
        if(timedGame)
        {
            timeRemainingText.gameObject.SetActive(true);
            InvokeRepeating("TimeCountdown", 1, 1);
        }
        gameOver = false;
        spawnManager.SetActive(true);
        playerAnimator.SetBool("BeginGame_b", true);
        dirtSplatter.Play();
    }

    public void SetTimed(bool timed)
    {
        timedGame = timed;
    }

    public static void ChangeScore(int changed)
    {
        score += changed;
    }



}
