using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main2GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // �X�R�A�\���p
    private int score = 0;  // �X�R�A

    public AudioSource bgmAudioSource;  // BGM�p�I�[�f�B�I�\�[�X
    public AudioClip shootingSE;  // �e������SE
    public AudioClip wordDestroyedSE;  // �P�ꂪ������SE

    void Start()
    {
        bgmAudioSource.Play();  // BGM���Đ�
    }

    void Update()
    {
        // �X�R�A���X�V
        scoreText.text = "Score: " + score;
    }

    // �X�R�A�����Z
    public void AddScore(int points)
    {
        score += points;
        PlaySE(wordDestroyedSE);  // �P�������SE���Đ�
    }

    // �Q�[���I�[�o�[�����i��j
    public void GameOver()
    {
        // �Q�[���I�[�o�[�����i�����ɃQ�[���I�[�o�[�̏�����ǉ��j
        Debug.Log("Game Over!");
    }

    // SE�Đ�
    public void PlaySE(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    // �e������SE
    public void PlayShootingSE()
    {
        PlaySE(shootingSE);
    }
}

