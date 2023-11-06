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
    private AudioSource audioSource;
    private static float score;
    private int timeRemaining = 60;
    private bool timedGame;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        DisplayUI();
        EndGame();
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
    }

    public void StartGame()
    {
        audioSource.Play();
        toggleGroup.SetActive(false);
        startButton.SetActive(false);
        if(timedGame)
        {
            timeRemainingText.gameObject.SetActive(true);
            InvokeRepeating("TimeCountdown", 1, 1);
        }
        gameOver = false;
        spawnManager.SetActive(true);
        playerAnimator.SetFloat("Speed_f", 1.0f);
        playerAnimator.SetBool("BeginGame_b", true);
        dirtSplatter.Play();
    }

    public void EndGame()
    {
        if (gameOver || timeRemaining == 0)
        {
            gameOver = true;
            playerAnimator.SetFloat("Speed_f", 0);
            playerAnimator.SetBool("BeginGame_b", false);
            audioSource.Stop();
            CancelInvoke();
        }

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
