using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main2WordController : MonoBehaviour
{
    public string word;  // �\������P��
    public string romaji;  // �^�C�s���O�ŕK�v�ȃ��[�}��
    public float speed = 2.0f;  // �P��̈ړ����x

    private Main2GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<Main2GameManager>();
    }

    void Update()
    {
        // �P����J�����Ɍ������Đi�܂���
        transform.position += Vector3.back * speed * Time.deltaTime;

        // �v���C���[�ɓ��B�����ꍇ�Q�[���I�[�o�[�i��j
        if (transform.position.z <= 0)
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }

    // �e�ƏՓ˂��������
    public void DestroyWord()
    {
        gameManager.AddScore(10);  // �X�R�A���Z
        Destroy(gameObject);  // �P��I�u�W�F�N�g�폜
    }
}
