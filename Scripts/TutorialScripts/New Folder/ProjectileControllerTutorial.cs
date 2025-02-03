using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileControllerTutorial : MonoBehaviour
{
    public static event Action<WordControllerTutorial, ProjectileControllerTutorial> OnProjectileHitWord; // イベント定義

    private WordControllerTutorial targetWord; // ターゲットの文字
    public float speed = 10f; // 弾の移動速度

    
    public void Initialize(WordControllerTutorial target)
    {
        targetWord = target; // ターゲットを設定
    }

    void Update()
    {
        if (targetWord == null)
        {
            // ターゲットが既に破壊されていたら弾も消す
            Destroy(gameObject);
            return;
        }

        // ターゲットに向かって移動
        Vector3 targetPosition = targetWord.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ターゲットに到達したら
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {

            // イベント発行
            OnProjectileHitWord?.Invoke(targetWord, this);

            // 衝突対象と弾を破壊
            Destroy(targetWord.gameObject);
            Destroy(gameObject);
        }



    }






}
