using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main2AudioManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;  // BGM�p�I�[�f�B�I�\�[�X
    public AudioClip bgmClip;  // BGM�̃I�[�f�B�I�N���b�v
    public AudioClip shootingSE;  // �e������SE
    public AudioClip wordDestroyedSE;  // �P�ꂪ������SE

    void Start()
    {
        // BGM�Đ�
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    // ����SE
    public void PlayShootingSE()
    {
        AudioSource.PlayClipAtPoint(shootingSE, transform.position);
    }

    // �P�����SE
    public void PlayWordDestroyedSE()
    {
        AudioSource.PlayClipAtPoint(wordDestroyedSE, transform.position);
    }
}

