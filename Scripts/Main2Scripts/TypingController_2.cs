using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingController_2 : MonoBehaviour
{
    public GameObject projectilePrefab; // �e�̃v���n�u
    public Transform shootPoint; // �e�𔭎˂���ʒu

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[�����J�n���Ă��Ȃ���Βe�𔭎˂��Ȃ�
        GameManager_2 gameManager = FindObjectOfType<GameManager_2>();
        if (gameManager != null && !gameManager.IsGameStarted())
        {
            return;
        }

        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                // �V�t�g�L�[��������Ă��Ȃ���Ԃő啶�������͂��ꂽ�ꍇ�A����
                bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

                // �啶���̓��͂���Shift��������Ă��Ȃ��ꍇ�͖���
                if (char.IsUpper(c) && !isShiftPressed)
                {
                    Debug.Log($"�啶�����͂����V�t�g�L�[��������Ă��Ȃ����ߖ���: {c}");
                    continue;
                }

                // ���͂��L���Ȃ�Βe�𔭎�
                CheckAndShoot(c.ToString());
            }
        }
    }

    void CheckAndShoot(string input)
    {
        WordController_2[] words = FindObjectsOfType<WordController_2>();

        foreach (var word in words)
        {
            // ���͂��ꂽ������ word.character ���r�i�������E�啶����킸�j
            if (word.character == input)
            {
                Shoot(word);
                break;
            }
        }
    }

    void Shoot(WordController_2 targetWord)
    {
        // �e�𐶐�
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<ProjectileController_2>().Initialize(targetWord);
    }
}
