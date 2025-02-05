using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class VirtualKeyboardTutorial : MonoBehaviour
{
    public Dictionary<char, SpriteRenderer> keyMapping;
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        keyMapping = new Dictionary<char, SpriteRenderer>();

        foreach (SpriteRenderer key in GetComponentsInChildren<SpriteRenderer>())
        {
            char keyChar = char.ToLower(key.gameObject.name[0]); //  �������œ��ꂵ�ēo�^

            if (!keyMapping.ContainsKey(keyChar))
            {
                keyMapping.Add(keyChar, key);
            }
        }
    }

    //  �^�C�s���O�Ώۂ̕����ɉ����ăL�[�����点��
    public void HighlightKey(char key)
    {
        ResetKeyColors();

        char lowerKey = char.ToLower(key);

        if (keyMapping.ContainsKey(lowerKey))
        {
            keyMapping[lowerKey].color = highlightColor;
        }
    }

    public void ResetKeyColors()
    {
        foreach (var key in keyMapping.Values)
        {
            key.color = defaultColor;
        }
    }
}
