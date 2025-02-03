using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManagerTutorial : MonoBehaviour
{
    
    public TextMeshProUGUI TimeText;  // 60秒カウントダウンのUI
    public TextMeshProUGUI targetLetterText;  // 目標の文字
    public TextMeshProUGUI scoreText;  // スコア表示用のUI
    public VirtualKeyboardTutorial virtualKeyboard;  // 仮想キーボードの参照
    public FingerIndicatorTutorial fingerIndicator;  // 指の表示スクリプト

    private float timeRemaining = 60f;
    private char currentLetter;
    private bool gameStarted = false;
    private bool pressedF = false;
    private bool pressedJ = false;
    private int score = 0;  // スコア

    void Start()
    {
        targetLetterText.text = "Press F and J to Start";
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        if (!gameStarted)
        {
            CheckStartKeys();
        }
        else
        {
            CheckTyping();
        }
    }

    void CheckStartKeys()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            pressedF = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            pressedJ = true;
        }

        if (pressedF && pressedJ)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        StartCoroutine(CountdownTimer());
        GenerateRandomLetter();
    }

    IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0)
        {
            TimeText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        GameOver();
    }

    void GenerateRandomLetter()
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        currentLetter = letters[Random.Range(0, letters.Length)];
        targetLetterText.text = currentLetter.ToString();

        virtualKeyboard.HighlightKey(currentLetter);
        fingerIndicator.UpdateFingerDisplay(currentLetter);
    }

    void CheckTyping()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (c == currentLetter)
                {
                    score++;  // スコア加算
                    scoreText.text = "Score: " + score;  // スコア表示更新
                    GenerateRandomLetter();
                    break;
                }
            }
        }
    }

    void GameOver()
    {
        TimeText.text = "Game Over!";
        targetLetterText.text = "";
        virtualKeyboard.DisableAllKeys();
    }
}
