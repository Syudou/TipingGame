using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManagerTutorial : MonoBehaviour
{
    
    public TextMeshProUGUI TimeText;  // 60�b�J�E���g�_�E����UI
    public TextMeshProUGUI targetLetterText;  // �ڕW�̕���
    public TextMeshProUGUI scoreText;  // �X�R�A�\���p��UI
    public TextMeshProUGUI GameOverText;  // �X�R�A�\���p��UI
    public Button RetryButton;  // �X�R�A�\���p��UI
    public Button TitleButton;  // �X�R�A�\���p��UI
    public GameObject keyboardPanel; //  ���z�L�[�{�[�h�̃p�l��
    public GameObject fingerPanel;   //  �w�̃p�l��


    public VirtualKeyboardTutorial virtualKeyboard;  // ���z�L�[�{�[�h�̎Q��
    public FingerIndicatorTutorial fingerIndicator;  // �w�̕\���X�N���v�g
    public AudioManagerTutorial audioManager;

    private float timeRemaining = 60f;
    private char currentLetter;
    private bool gameStarted = false;
    private bool pressedF = false;
    private bool pressedJ = false;
    private int score = 0;  // �X�R�A

    void Start()
    {
        TimeText.text="";
        targetLetterText.gameObject.SetActive(true);
        keyboardPanel.SetActive(true); //  �L�[�{�[�h�p�l����\��
        fingerPanel.SetActive(true);   //  �w�̃p�l����\��
        GameOverText.gameObject.SetActive(false); // �Q�[���I�[�o�[�e�L�X�g���\��
        RetryButton.gameObject.SetActive(false);  // ���g���C�{�^�����\��
        TitleButton.gameObject.SetActive(false);  // �^�C�g���{�^�����\��
        targetLetterText.text = "������OK�ł����H<br>�uf�v�Ɓuj�v�𓯎������ŊJ�n";
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

        virtualKeyboard.ResetKeyColors();
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
                    score++;  // �X�R�A���Z
                    scoreText.text = "Score: " + score;  // �X�R�A�\���X�V

                    virtualKeyboard.ResetKeyColors();
                    GenerateRandomLetter();
                    break;
                }
            }
        }
    }

    public void GameOver()
    {
        keyboardPanel.SetActive(false); //  �L�[�{�[�h�p�l�����\��
        fingerPanel.SetActive(false);   //  �w�̃p�l�����\��
        //targetLetterText.text = "Game Over!";
        targetLetterText.text = "";
        TimeText.text = "";
        virtualKeyboard.ResetKeyColors();
        audioManager.PlayGameOverSE();
        targetLetterText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true); // �Q�[���I�[�o�[�e�L�X�g���\��
        RetryButton.gameObject.SetActive(true);  // ���g���C�{�^�����\��
        TitleButton.gameObject.SetActive(true);  // �^�C�g���{�^�����\��



    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���݂̃V�[�����ēǂݍ���
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene"); // �^�C�g����ʂ̃V�[����ǂݍ��ށi�V�[�����͓K�X�ύX�j
    }
}
