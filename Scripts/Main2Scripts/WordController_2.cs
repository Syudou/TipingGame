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
            // ���͂��Ԉ�����ꍇ���Z�b�g
            currentInput = "";
            wordRenderer.material.color = Color.red; // �ԐF�Ō���͂�\��
            return false;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{


    //    WordController_2 hitWord2 = collision.gameObject.GetComponent<WordController_2>();

    //    if (hitWord2 != null && hitWord2 == romaji)
    //    {
    //        Debug.Log($"Target hit: {hitWord2.textDisplay.text}");

    //        // GameManager_2 �ɐڐG���𑗐M
    //        GameManager_2 gameManager = FindObjectOfType<GameManager_2>();
    //        if (gameManager != null)
    //        {
    //            gameManager.OnWordHit(hitWord2); // �ڐG�����P���n��
    //        }

    //        // �P��ƒe��j��
    //        Destroy(hitWord2.gameObject);
    //        Destroy(gameObject);
    //    }
    //}
}
