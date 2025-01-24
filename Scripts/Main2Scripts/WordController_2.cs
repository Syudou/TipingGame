using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordController_2 : MonoBehaviour
{
    public TextMeshPro textDisplay; // 日本語とローマ字を表示するTextMeshPro
    public string romajiWord; // タイピングに必要なローマ字
    public string currentInput = ""; // 現在のプレイヤー入力

    private Vector3 targetPosition; // プレイヤーの位置（目標地点）
    private float speed; // 移動速度

    private Renderer wordRenderer;

    // 初期化（日本語とローマ字のセット）
    public void Initialize(string japaneseWord, string romaji)
    {
        romajiWord = romaji;
        textDisplay.text = $"{japaneseWord}\n<color=yellow>{romaji}</color>"; // 日本語とローマ字を表示
        wordRenderer = GetComponent<Renderer>(); // Rendererを取得
    }

    // 目標地点と速度を設定
    public void SetTarget(Vector3 target, float moveSpeed)
    {
        targetPosition = target;
        speed = moveSpeed;
    }

    void Update()
    {
        // 単語をプレイヤーに向かって移動させる
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // プレイヤーに到達した場合（ゲームオーバー）
        if (transform.position == targetPosition)
        {
            FindObjectOfType<GameManager_2>().GameOver();
            Destroy(gameObject);
        }

        // キーボード入力を処理（ローマ字入力を監視）
        ProcessKeyboardInput();
    }

    // プレイヤー入力を処理
    private void ProcessKeyboardInput()
    {
        // キー入力を監視（ローマ字）
        foreach (char c in "abcdefghijklmnopqrstuvwxyz") // ローマ字の英小文字
        {
            if (Input.GetKeyDown(c.ToString())) // 入力されたキーがあれば
            {
                bool isCorrect = ProcessInput(c); // 入力処理
                if (isCorrect)
                {
                    Debug.Log($"正しい入力: {currentInput}"); // コンソールに表示
                    wordRenderer.material.color = Color.green; // 単語を緑色に変更（正しい入力）
                }
                else
                {
                    Debug.Log($"入力中: {currentInput}"); // コンソールに表示
                    wordRenderer.material.color = Color.yellow; // 単語を黄色に変更（入力中）
                }
            }
        }
    }

    // ローマ字入力を処理する
    public bool ProcessInput(char input)
    {
        currentInput += input; // 入力された文字を追加

        if (romajiWord.StartsWith(currentInput))
        {
            // 入力が正しい場合
            if (romajiWord == currentInput)
            {
                Destroy(gameObject); // 正解したら単語を削除
                return true; // 正しい入力として処理
            }
            return false; // 入力中
        }
        else
        {
            // 入力が間違っている場合
            currentInput = ""; // 入力をリセット
            wordRenderer.material.color = Color.red; // 単語を赤色に変更（間違った入力）
            return false;
        }
    }
}
