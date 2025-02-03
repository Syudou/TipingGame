using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerIndicatorTutorial : MonoBehaviour
{
    public Image leftPinky, leftRing, leftMiddle, leftIndex;
    public Image rightIndex, rightMiddle, rightRing, rightPinky, thumbs;

    private Dictionary<char, Image> fingerMapping;

    void Start()
    {
        fingerMapping = new Dictionary<char, Image>()
        {
            // �����w
            {'1', leftPinky}, {'Q', leftPinky}, {'A', leftPinky}, {'Z', leftPinky}, {'\t', leftPinky}, {'\n', leftPinky},
            
            // ����w
            {'2', leftRing}, {'W', leftRing}, {'S', leftRing}, {'X', leftRing},
            
            // �����w
            {'3', leftMiddle}, {'E', leftMiddle}, {'D', leftMiddle}, {'C', leftMiddle},
            
            // ���l�����w
            {'4', leftIndex}, {'5', leftIndex}, {'R', leftIndex}, {'T', leftIndex}, {'F', leftIndex}, {'G', leftIndex}, {'V', leftIndex}, {'B', leftIndex},

            // �E�l�����w
            {'6', rightIndex}, {'7', rightIndex}, {'Y', rightIndex}, {'U', rightIndex}, {'H', rightIndex}, {'J', rightIndex}, {'N', rightIndex}, {'M', rightIndex},

            // �E���w
            {'8', rightMiddle}, {'I', rightMiddle}, {'K', rightMiddle}, {',', rightMiddle},
            
            // �E��w
            {'9', rightRing}, {'O', rightRing}, {'L', rightRing}, {'.', rightRing},
            
            // �E���w
            {'0', rightPinky}, {'P', rightPinky}, {'@', rightPinky}, {'-', rightPinky}, {':', rightPinky}, {';', rightPinky}, {'\b', rightPinky}
        };
    }

    public void UpdateFingerDisplay(char key)
    {
        ResetFingerColors();
        char upperKey = char.ToUpper(key); // ��������啶���ɕϊ�

        if (fingerMapping.ContainsKey(upperKey))
        {
            fingerMapping[upperKey].color = Color.red;  // �Ή�����w��Ԃ�����
        }
    }

    void ResetFingerColors()
    {
        foreach (var finger in fingerMapping.Values)
        {
            finger.color = Color.white;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
