using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileController : MonoBehaviour
{
    public static event Action<WordController, ProjectileController> OnProjectileHitWord; // �C�x���g��`

    private WordController targetWord; // �^�[�Q�b�g�̕���
    public float speed = 10f; // �e�̈ړ����x

    //public AudioClip hitSound; // �e�������ɓ��������Ƃ��̌��ʉ�
    //private AudioSource audioSource;

    //void Awake()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //}
    public void Initialize(WordController target)
    {
        targetWord = target; // �^�[�Q�b�g��ݒ�
    }

    void Update()
    {
        if (targetWord == null)
        {
            // �^�[�Q�b�g�����ɔj�󂳂�Ă�����e������
            Destroy(gameObject);
            return;
        }

        // �^�[�Q�b�g�Ɍ������Ĉړ�
        Vector3 targetPosition = targetWord.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // �^�[�Q�b�g�ɓ��B������
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {

            // �C�x���g���s
            OnProjectileHitWord?.Invoke(targetWord, this);

            // �ՓˑΏۂƒe��j��
            Destroy(targetWord.gameObject);
            Destroy(gameObject);
        }



    }






}
