using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main2PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;  // �e�̃v���n�u
    public Transform bulletSpawnPoint;  // �e�̔��ˈʒu

    private string currentInput = "";  // ���[�U�[�̌��݂̓���

    void Update()
    {
        // �L�[�{�[�h���͂��󂯎��
        foreach (char c in Input.inputString)
        {
            currentInput += c;

            // ���݂̓��͂����P���T��
            Main2WordController[] words = FindObjectsOfType<Main2WordController>();
            foreach (Main2WordController word in words)
            {
                if (word.romaji.StartsWith(currentInput))
                {
                    if (word.romaji == currentInput)
                    {
                        // �P�������������
                        ShootBullet(word);
                        currentInput = "";  // ���͂����Z�b�g
                    }
                    return;
                }
            }
        }
    }

    void ShootBullet(Main2WordController targetWord)
    {
        // �e�𐶐����Ĕ���
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Main2BulletController>().SetTarget(targetWord.transform);
    }
}

