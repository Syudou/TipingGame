using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main2BulletController : MonoBehaviour
{
    public float speed = 5.0f;  // �e�̑��x
    private Transform targetWord;  // �ڕW�̒P��

    void Update()
    {
        if (targetWord != null)
        {
            // �ڕW�Ɍ������Ēe��i�߂�
            transform.position = Vector3.MoveTowards(transform.position, targetWord.position, speed * Time.deltaTime);

            // �ڕW�ɓ��B�����珈��
            if (transform.position == targetWord.position)
            {
                targetWord.GetComponent<Main2WordController>().DestroyWord();
                Destroy(gameObject);  // �e���폜
            }
        }
    }

    // �ڕW�ƂȂ�P���ݒ�
    public void SetTarget(Transform target)
    {
        targetWord = target;
    }
}

