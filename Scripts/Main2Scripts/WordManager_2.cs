using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordManager_2 : MonoBehaviour
{
    public GameObject wordPrefab; // 単語のプレハブ (TextMeshProを含むオブジェクト)
    public float initialSpawnInterval = 2f; // 単語の最初の出現間隔
    public float spawnAcceleration = 0.98f; // 出現間隔の加速率（1未満の値）
    private float spawnInterval; // 現在の出現間隔

    public float initialWordSpeed = 2f; // 単語の移動速度
    public float speedIncreaseRate = 0.1f; // 速度の増加率
    private float wordSpeed; // 現在の移動速度

    private float timer;

    private List<GameObject> spawnedWords = new List<GameObject>(); // 生成された単語のリスト

    // 単語とローマ字のセット
    private string[,] wordList = new string[,]
    {
        { "さかな", "sakana" },
        { "ねこ", "neko" },
        { "いぬ", "inu" },
        { "はな", "hana" },
        { "やま", "yama" },
        { "かわ", "kawa" },
        { "ひと", "hito" },
        { "ほん","hon" },
        { "いぬ", "inu" },
        { "ねこ", "neko" },
        { "くるま","kuruma" },
        {"いえ","ie" },
        { "がっこう", "gakkou" },
        { "しごと",  "shigoto" },
        { "ともだち",  "tomodachi" },
        { "たべもの", "tabemono" },
        { "みず", "mizu" },
        { "ひかり", "hikari" },
        { "うさぎ", "usagi" },
　　　　{ "しか", "shika" },
        { "かめ", "kame" },
        { "へび", "hebi" },
        { "おに", "oni" },
        { "まほう", "mahou" },
        { "ひこうき", "hikouki" },
        { "くるま", "kuruma" },
        { "でんしゃ", "densha" },
        { "ふね", "fune" },
        { "みち", "michi" },
        { "たび", "tabi" },
        { "きぼう", "kibou" },
        { "つばさ", "tsubasa" },
        { "たまご", "tamago" },
        { "りんご", "ringo" },
        { "みかん", "mikan" },
        { "ぶどう", "budou" },
        { "なし", "nashi" },
        { "もも", "momo" },
        { "すいか", "suika" },
        { "かお", "kao" },
        { "めがね", "megane" },
        { "ぼうし", "boushi" },
        { "かばん", "kaban" },
        { "くつ", "kutsu" },
        { "はし", "hashi" },
        { "てがみ", "tegami" },
        { "えんぴつ", "enpitsu" },
        { "ほん", "hon" },
        { "ざっし", "zasshi" },
        { "しんぶん", "shinbun" },
        { "みせ", "mise" },
        { "はた", "hata" },
        { "つの", "tsuno" },
        { "つめ", "tsume" },
        { "はね", "hane" },
        { "いえ", "ie" },
        { "まど", "mado" },
        { "とびら", "tobira" },
        { "きん", "kin" },
        { "ぎん", "gin" },
        { "たから", "takara" },
        { "こえ", "koe" },
        { "おんがく", "ongaku" },
        { "え", "e" },
        { "うた", "uta" },
        { "はしる", "hashiru" },
        { "あるく", "aruku" },
        { "おどる", "odoru" }
    };

    void Start()
    {
        spawnInterval = initialSpawnInterval; // 出現間隔を初期化
        wordSpeed = initialWordSpeed; // 移動速度を初期化
    }

    void Update()
    {
        // ゲームが開始されていない、またはゲームオーバーの場合は動作しない
        GameManager_2 gameManager = FindObjectOfType<GameManager_2>();
        if (!gameManager.IsGameStarted() || gameManager.isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnWord();
            timer = 0f;

            // 出現間隔を短縮し、最小値を設定
            spawnInterval = Mathf.Max(spawnInterval * spawnAcceleration, 0.5f);

            // 単語の移動速度を増加
            wordSpeed += speedIncreaseRate;
        }
    }

    void SpawnWord()
    {
        Camera cam = Camera.main;

        // カメラ枠内のランダムな位置に単語を生成
        float x = Random.Range(-0.1f, 1.1f); // カメラ枠の左右
        float y = Random.Range(-0.1f, 1.1f); // カメラ枠の上方

        // 確実に枠外にするための調整
        if (x > 0f && x < 1f) x = x > 0.5f ? 1.1f : -0.1f;
        if (y > 0f && y < 1f) y = y > 0.5f ? 1.1f : -0.1f;

        Vector3 spawnPosition = cam.ViewportToWorldPoint(new Vector3(x, y, cam.nearClipPlane + 5));
        spawnPosition.z = 0; // Z位置を0に設定

        // ランダムな単語を取得
        int randomIndex = Random.Range(0, wordList.GetLength(0));
        string japaneseWord = wordList[randomIndex, 0];
        string romajiWord = wordList[randomIndex, 1];

        // 単語のプレハブを生成
        GameObject newWord = Instantiate(wordPrefab, spawnPosition, Quaternion.identity);
        spawnedWords.Add(newWord); // リストに追加

        // WordControllerを使って単語を設定
        WordController_2 wordController_2 = newWord.GetComponent<WordController_2>();
        wordController_2.Initialize(japaneseWord, romajiWord);

        // プレイヤーに向かう動きを設定
        wordController_2.SetTarget(Vector3.zero, wordSpeed);
    }

    // すべての単語を消去
    public void ClearAllWords()
    {
        foreach (GameObject word in spawnedWords)
        {
            if (word != null)
            {
                Destroy(word);
            }
        }
        spawnedWords.Clear(); // リストをクリア
    }

    public TextMeshProUGUI typedTextUI; // 画面に表示する TextMeshPro
    private string typedText = ""; // プレイヤーの入力を保持

    // プレイヤーの入力を更新し、画面に表示する
    public void UpdateTypedText(string input)
    {
        typedText += input; // 文字を追加
        typedTextUI.text = typedText; // UIに反映
    }

    // 入力をリセット（単語が確定した時など）
    public void ResetTypedText()
    {
        typedText = "";
        typedTextUI.text = "";
    }
}
