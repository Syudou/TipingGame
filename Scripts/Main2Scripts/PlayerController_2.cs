using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_2 : MonoBehaviour
{
    public static event Action<int> OnPlayerTakeDamage; // �v���C���[�_���[�W�C�x���g

    public int maxHP = 100; // �v���C���[�̍ő�HP
    private int currentHP;
    public AudioClip damageSound; // �_���[�W���󂯂��Ƃ��̌��ʉ�
    private AudioSource audioSource;

    

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP; // HP��������
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Word"))
        {
            // �_���[�W�C�x���g�𔭍s
            OnPlayerTakeDamage?.Invoke(10); // �_���[�W�ʂ�ʒm

            // ���ʉ����Đ�
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }

            // ������j��
            Destroy(collision.gameObject);
        }
    }

    
}
