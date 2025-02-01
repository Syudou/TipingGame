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
    public int index; //���ʔԍ�

    private Renderer wordRenderer;

    public WordManager_2 wordManager; // WordManager_2 �̎Q��
    void Start()
    {
        wordManager = FindObjectOfType<WordManager_2>();
        if (wordManager == null)
        {
            Debug.LogError("wordManager ��������܂���I Hierarchy �� WordManager_2 �����邩�m�F���Ă��������B");
        }
        else
        {
            Debug.Log("wordManager �𐳏�Ɏ擾���܂����I");
        }
    }
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
        if (index != GameManager_2.masterIndex) return; //�Y���L�[���[�h�łȂ��ꍇ�͉����N�����Ȃ�
       
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

                        GameManager_2.masterIndex++; //�^�[�Q�b�g�ԍ���1�i�߂�@
                    }
                }
            }
        }
    }

    // ���[�}�����͂���������
    public bool ProcessInput(char input)
    {
        if (wordManager == null)
        {
            //Debug.LogError("wordManager �� null �ł��I");
            return false;
        }

        currentInput += input; // ���͂��ꂽ������ǉ�
        //Debug.Log($"���݂̓���: {currentInput}"); 

        //wordManager.UpdateTypedText(currentInput); //  �ύX: 1�����ł͂Ȃ� `currentInput` �S�̂�n���I

        if (romajiWord.StartsWith(currentInput))
        {
            wordManager.UpdateTypedText(currentInput); //UI�ɓ��͒��̕������o��

            if (romajiWord == currentInput) // ���������͊��������ꍇ
            {
                //Debug.Log("���͊���: " + currentInput);
                wordManager.ResetTypedText();
                currentInput = ""; //  ���̓��Z�b�g�I
                return true;
            }
            return false;
        }
        else
        {
            

            Debug.Log("���Z�b�g");
            currentInput = ""; // �Ԉ�����ꍇ�̓��Z�b�g
            wordManager.ResetTypedText(); //  UI �����Z�b�g
            //wordRenderer.material.color = Color.red;
            return false;
        }
    }


}
