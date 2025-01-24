using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordController_2 : MonoBehaviour
{
    public TextMeshPro textDisplay; // ���{��ƃ��[�}����\������TextMeshPro
    public string romajiWord; // �^�C�s���O�ɕK�v�ȃ��[�}��
    public string currentInput = ""; // ���݂̃v���C���[����

    private Vector3 targetPosition; // �v���C���[�̈ʒu�i�ڕW�n�_�j
    private float speed; // �ړ����x

    private Renderer wordRenderer;

    // �������i���{��ƃ��[�}���̃Z�b�g�j
    public void Initialize(string japaneseWord, string romaji)
    {
        romajiWord = romaji;
        textDisplay.text = $"{japaneseWord}\n<color=yellow>{romaji}</color>"; // ���{��ƃ��[�}����\��
        wordRenderer = GetComponent<Renderer>(); // Renderer���擾
    }

    // �ڕW�n�_�Ƒ��x��ݒ�
    public void SetTarget(Vector3 target, float moveSpeed)
    {
        targetPosition = target;
        speed = moveSpeed;
    }

    void Update()
    {
        // �P����v���C���[�Ɍ������Ĉړ�������
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // �v���C���[�ɓ��B�����ꍇ�i�Q�[���I�[�o�[�j
        if (transform.position == targetPosition)
        {
            FindObjectOfType<GameManager_2>().GameOver();
            Destroy(gameObject);
        }

        // �L�[�{�[�h���͂������i���[�}�����͂��Ď��j
        ProcessKeyboardInput();
    }

    // �v���C���[���͂�����
    private void ProcessKeyboardInput()
    {
        // �L�[���͂��Ď��i���[�}���j
        foreach (char c in "abcdefghijklmnopqrstuvwxyz") // ���[�}���̉p������
        {
            if (Input.GetKeyDown(c.ToString())) // ���͂��ꂽ�L�[�������
            {
                bool isCorrect = ProcessInput(c); // ���͏���
                if (isCorrect)
                {
                    Debug.Log($"����������: {currentInput}"); // �R���\�[���ɕ\��
                    wordRenderer.material.color = Color.green; // �P���ΐF�ɕύX�i���������́j
                }
                else
                {
                    Debug.Log($"���͒�: {currentInput}"); // �R���\�[���ɕ\��
                    wordRenderer.material.color = Color.yellow; // �P������F�ɕύX�i���͒��j
                }
            }
        }
    }

    // ���[�}�����͂���������
    public bool ProcessInput(char input)
    {
        currentInput += input; // ���͂��ꂽ������ǉ�

        if (romajiWord.StartsWith(currentInput))
        {
            // ���͂��������ꍇ
            if (romajiWord == currentInput)
            {
                Destroy(gameObject); // ����������P����폜
                return true; // ���������͂Ƃ��ď���
            }
            return false; // ���͒�
        }
        else
        {
            // ���͂��Ԉ���Ă���ꍇ
            currentInput = ""; // ���͂����Z�b�g
            wordRenderer.material.color = Color.red; // �P���ԐF�ɕύX�i�Ԉ�������́j
            return false;
        }
    }
}
