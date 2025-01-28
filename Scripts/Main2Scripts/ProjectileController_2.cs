using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileController_2 : MonoBehaviour
{

    public static event Action<WordController_2, ProjectileController_2> OnProjectileHitWord; // �C�x���g��`
    private WordController_2 target; // �^�[�Q�b�g�̕���
    public float speed = 10f; // �e�̈ړ����x

    public AudioManager_2 audioManager; // AudioManager���Q��

    public void Initialize(WordController_2 targetWord)
    {
        target = targetWord; // �^�[�Q�b�g��ݒ�
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // �^�[�Q�b�g��j�󂵃X�R�A�����Z
            GameManager_2 gameManager = FindObjectOfType<GameManager_2>();
            if (gameManager != null)
            {
                gameManager.IncreaseScore(10); // �X�R�A�����Z
            }

            Destroy(target.gameObject);
            Destroy(gameObject);
            AudioManager_2 audioManager = FindObjectOfType<AudioManager_2>();
            audioManager.PlayhitSE();

        }

        

}

   

   
        

     


    }
