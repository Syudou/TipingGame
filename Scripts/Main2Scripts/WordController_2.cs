using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class WordController_2 : MonoBehaviour
{
    public TextMeshPro textDisplay; // 日本語とローマ字を表示するTextMeshPro
    public string romajiWord; // タイピングに必要なローマ字
    public string currentInput = ""; // 現在のプレイヤー入力

    private Vector3 targetPosition; // プレイヤーの位置（目標地点）
    private float speed; // 移動速度
                         
    public string character;// 文字を保持するフィールド

    private Renderer wordRenderer;

    public WordManager_2 wordManager; // WordManager_2 の参照

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

        if (Input.anyKeyDown) // キーが押されたら
        {
            string input = Input.inputString; // 入力された文字を取得
            if (!string.IsNullOrEmpty(input))
            {
                wordManager.UpdateTypedText(input); // WordManager_2 に送る
            }
        }
    }


    // プレイヤー入力を処理
    private void ProcessKeyboardInput()
    {
        // キー入力を監視（ローマ字）
        foreach (char c in "abcdefghijklmnopqrstuvwxyz")
        {
            if (Input.GetKeyDown(c.ToString()))
            {
                bool isComplete = ProcessInput(c);
                if (isComplete)
                {
                    // 入力完了時に弾を発射
                    TypingController_2 typingController = FindObjectOfType<TypingController_2>();
                    if (typingController != null)
                    {
                        typingController.Shoot(this); // 弾を発射
                    }
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
            if (romajiWord == currentInput)
            {
                // 正しい入力が完了
                return true;
            }
            return false;
        }
        else
        {
            Debug.Log("リセット");
            // 入力が間違った場合リセット
            currentInput = "";
            wordRenderer.material.color = Color.red; // 赤色で誤入力を表示
            return false;
        }
    }

    
}
