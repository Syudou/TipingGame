using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class WordController_2 : MonoBehaviour
{
    public TextMeshPro textDisplay; // ���{��ƃ��[�}����\������TextMeshPro
    public string romajiWord; // �^�C�s���O�ɕK�v�ȃ��[�}��
    public string currentInput = ""; // ���݂̃v���C���[����

    private Vector3 targetPosition; // �v���C���[�̈ʒu�i�ڕW�n�_�j
    private float speed; // �ړ����x
                         
    public string character;// ������ێ�����t�B�[���h

    private Renderer wordRenderer;

    public WordManager_2 wordManager; // WordManager_2 �̎Q��

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

        if (Input.anyKeyDown) // �L�[�������ꂽ��
        {
            string input = Input.inputString; // ���͂��ꂽ�������擾
            if (!string.IsNullOrEmpty(input))
            {
                wordManager.UpdateTypedText(input); // WordManager_2 �ɑ���
            }
        }
    }


    // �v���C���[���͂�����
    private void ProcessKeyboardInput()
    {
        // �L�[���͂��Ď��i���[�}���j
        foreach (char c in "abcdefghijklmnopqrstuvwxyz")
        {
            if (Input.GetKeyDown(c.ToString()))
            {
                bool isComplete = ProcessInput(c);
                if (isComplete)
                {
                    // ���͊������ɒe�𔭎�
                    TypingController_2 typingController = FindObjectOfType<TypingController_2>();
                    if (typingController != null)
                    {
                        typingController.Shoot(this); // �e�𔭎�
                    }
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
            if (romajiWord == currentInput)
            {
                // ���������͂�����
                return true;
            }
            return false;
        }
        else
        {
            Debug.Log("���Z�b�g");
            // ���͂��Ԉ�����ꍇ���Z�b�g
            currentInput = "";
            wordRenderer.material.color = Color.red; // �ԐF�Ō���͂�\��
            return false;
        }
    }

    
}
