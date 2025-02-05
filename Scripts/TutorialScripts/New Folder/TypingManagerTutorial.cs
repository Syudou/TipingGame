using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TypingManagerTutorial : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // 60�b�J�E���g�_�E����UI
    public TextMeshProUGUI targetLetterText;  // �ڕW�̕���
    public VirtualKeyboardTutorial virtualKeyboard;  // ���z�L�[�{�[�h�̎Q��
    public FingerIndicatorTutorial fingerIndicator;  // �w�̕\���X�N���v�g
    public GameManagerTutorial GameManager;

    private float timeRemaining = 60f;
    private char currentLetter;

    void Start()
    {
        GenerateRandomLetter();
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0)
        {
            countdownText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        GameManager.GameOver();
    }

    void GenerateRandomLetter()
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        currentLetter = letters[Random.Range(0, letters.Length)];
        targetLetterText.text = currentLetter.ToString();

        virtualKeyboard.HighlightKey(currentLetter);
        fingerIndicator.UpdateFingerDisplay(currentLetter);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (c == currentLetter)
                {
                    GenerateRandomLetter();
                    break;
                }
            }
        }
    }

    //void GameOver()
    //{
    //    countdownText.text = "Game Over!";
    //    targetLetterText.text = "";
    //    virtualKeyboard.ResetKeyColors();
    //}
}
