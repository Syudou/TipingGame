using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WordControllerTutorial : MonoBehaviour
{
    private TextMeshPro textComponent;

    public string character { get; private set; } // �O������ǂݎ��\�ȃv���p�e�B

    private Vector3 targetPosition; // �v���C���[�̈ʒu
    private float moveSpeed; // �ړ����x

    void Awake()
    {
        // TextMeshPro �R���|�[�l���g���擾
        textComponent = GetComponent<TextMeshPro>();
    }

    

    public void Initialize(char charToSet)
    {
        character = charToSet.ToString(); // ���͕������v���p�e�B�ɃZ�b�g
        // �\�����镶����ݒ�
        textComponent.text = character; // �\���������X�V
    }

    public void SetTarget(Vector3 target, float speed)
    {
        targetPosition = target;
        moveSpeed = speed;
    }

    void Update()
    {
        // �^�[�Q�b�g�Ɍ������Ĉړ�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �^�[�Q�b�g�ɓ��B�����玩�g��j��i�K�v�Ȃ�폜�j
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
