using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class VirtualKeyboardTutorial : MonoBehaviour
{
    public Dictionary<char, Button> keyButtons = new Dictionary<char, Button>();

    void Start()
    {
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            char key = button.GetComponentInChildren<TextMeshProUGUI>().text[0];
            keyButtons[key] = button;
        }
    }

    public void HighlightKey(char key)
    {
        DisableAllKeys();
        if (keyButtons.ContainsKey(key))
        {
            keyButtons[key].image.color = Color.yellow;
        }
    }

    public void DisableAllKeys()
    {
        foreach (var button in keyButtons.Values)
        {
            button.image.color = Color.white;
        }
    }
}
