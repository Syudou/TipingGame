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
            // 左小指
            {'1', leftPinky}, {'Q', leftPinky}, {'A', leftPinky}, {'Z', leftPinky}, {'\t', leftPinky}, {'\n', leftPinky},
            
            // 左薬指
            {'2', leftRing}, {'W', leftRing}, {'S', leftRing}, {'X', leftRing},
            
            // 左中指
            {'3', leftMiddle}, {'E', leftMiddle}, {'D', leftMiddle}, {'C', leftMiddle},
            
            // 左人差し指
            {'4', leftIndex}, {'5', leftIndex}, {'R', leftIndex}, {'T', leftIndex}, {'F', leftIndex}, {'G', leftIndex}, {'V', leftIndex}, {'B', leftIndex},

            // 右人差し指
            {'6', rightIndex}, {'7', rightIndex}, {'Y', rightIndex}, {'U', rightIndex}, {'H', rightIndex}, {'J', rightIndex}, {'N', rightIndex}, {'M', rightIndex},

            // 右中指
            {'8', rightMiddle}, {'I', rightMiddle}, {'K', rightMiddle}, {',', rightMiddle},
            
            // 右薬指
            {'9', rightRing}, {'O', rightRing}, {'L', rightRing}, {'.', rightRing},
            
            // 右小指
            {'0', rightPinky}, {'P', rightPinky}, {'@', rightPinky}, {'-', rightPinky}, {':', rightPinky}, {';', rightPinky}, {'\b', rightPinky}
        };
    }

    public void UpdateFingerDisplay(char key)
    {
        ResetFingerColors();
        char upperKey = char.ToUpper(key); // 小文字を大文字に変換

        if (fingerMapping.ContainsKey(upperKey))
        {
            fingerMapping[upperKey].color = Color.red;  // 対応する指を赤くする
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
