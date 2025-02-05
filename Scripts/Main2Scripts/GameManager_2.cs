using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager_2 : MonoBehaviour
{
    public int maxHP = 100; // �v���C���[�̍ő�HP
    private int currentHP; // ���݂�HP
    public int score = 0; // ���݂̃X�R�A

    public static int masterIndex = 1; //�^�[�Q�b�g�ԍ�

    public TextMeshProUGUI hpText; // HP �\���p�� Text (UI)
    public TextMeshProUGUI scoreText; // �X�R�A�\���p�� Text (UI)
    public TextMeshProUGUI gameOverText; // �Q�[���I�[�o�[�\���p
    public TextMeshProUGUI readyText; // ���������̃e�L�X�g
    public TextMeshProUGUI TypedText;

    public GameObject retryButton; // ���g���C�{�^��
    public GameObject titleButton; // �^�C�g���{�^��
    public AudioManager_2 audioManager; // AudioManager���Q��

    public AudioClip hitSound; // ���ʉ�
    private AudioSource audioSource;

    public bool isGameOver = false; // �Q�[���I�[�o�[��Ԃ̊Ǘ�
    private bool isGameStarted = false; // �Q�[���J�n��Ԃ̊Ǘ�

    // Start is called before the first frame update
    void Start()
    {
        masterIndex = 1;

        audioSource = GetComponent<AudioSource>(); // AudioSource �R���|�[�l���g���擾

        // �����ݒ�
        currentHP = maxHP;
        

        // UI�v�f�̏�����
        UpdateHPText();
        UpdateScoreText();
        gameOverText.gameObject.SetActive(false); // �Q�[���I�[�o�[��\��
        retryButton.SetActive(false);
        titleButton.SetActive(false);
        readyText.gameObject.SetActive(true); // ���������e�L�X�g�\��
        TypedText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���J�n�t���O�������Ă��Ȃ��ꍇ�A�������������m
        if (!isGameStarted)
        {
            CheckForGameStart();
        }
    }

    private void CheckForGameStart()
    {
        // �uF�v�ƁuJ�v�̓������������m
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        isGameStarted = true;
        readyText.gameObject.SetActive(false); // ���������e�L�X�g���\��
        Debug.Log("Game Started!");
    }

    private void OnEnable()
    {
        //ProjectileController_2.OnProjectileHitWord += HandleProjectileHitWord;
        PlayerController_2.OnPlayerTakeDamage += HandlePlayerTakeDamage;


    }

    

    private void OnDisable()
    {
        //ProjectileController_2.OnProjectileHitWord -= HandleProjectileHitWord;
        PlayerController_2.OnPlayerTakeDamage -= HandlePlayerTakeDamage;
    }

    private void HandleProjectileHitWord(WordController_2 word, ProjectileController_2 projectile)
    {
        if (isGameOver) return;

        string wordText = word.textDisplay.text;
        Debug.Log($"Word '{wordText}' was hit by a projectile!");

        // �X�R�A�����Z
        score += 10;
        UpdateScoreText();

        // ���ʉ����Đ�
        if (audioSource != null && hitSound != null)
        {
            Debug.Log("�Ђ��ƁI���ʉ����Đ����܂��B");
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogError("AudioSource �܂��� hitSound �� null �ł��B");
        }
    }

    // �v���C���[���_���[�W���󂯂��Ƃ��̏���
    private void HandlePlayerTakeDamage(int damage)
    {
        if (isGameOver) return;

        currentHP -= damage;
        UpdateHPText();

        // HP��0�ȉ��ɂȂ����ꍇ�̏���
        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    // HP�e�L�X�g���X�V
    private void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
    }

    // �X�R�A�e�L�X�g���X�V
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // �Q�[���I�[�o�[���̏���
    public void GameOver()
    {

        isGameOver = true;
        Debug.Log("Game Over!");

        // WordManager�̃X�|�[�����~���A������S�ď���
        WordManager_2 wordManager = FindObjectOfType<WordManager_2>();
        if (wordManager != null)
        {
            wordManager.enabled = false;
            wordManager.ClearAllWords(); // ���ׂĂ̕���������
        }

        // �v���C���[�̑�����~
        PlayerController_2 player = FindObjectOfType<PlayerController_2>();
        if (player != null)
        {
            player.enabled = false;
        }

        gameOverText.gameObject.SetActive(true); // �Q�[���I�[�o�[��\��
        retryButton.SetActive(true);
        titleButton.SetActive(true);

        // �Q�[���I�[�o�[����SE���Đ�
        audioManager.PlayGameOverSE();

        TypedText.gameObject.SetActive(false); //�^�C�s���O�͏���
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    // ���g���C�{�^���p�̏���
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���݂̃V�[�����ēǂݍ���
    }

    // �^�C�g���{�^���p�̏���
    public void GoToTitle()
    {
        // �^�C�g���V�[�������[�h����i"TitleScene"���V�[�����̏ꍇ�j
        SceneManager.LoadScene("TitleScene");
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // �P��ƒe�̏Փ˂���������֐�
    public void OnWordHit(WordController_2 hitWord)
    {
        if (isGameOver) return;

        Debug.Log($"Word '{hitWord.textDisplay.text}' was hit!");

        // �X�R�A�����Z
        //score += 10;
        //UpdateScoreText();

        // ���ʉ����Đ�
        if (audioSource != null && hitSound != null)
        {
            Debug.Log("���ʉ����Đ����܂�");
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogError("AudioSource �܂��� hitSound �� null �ł��B");
        }
    }
}
