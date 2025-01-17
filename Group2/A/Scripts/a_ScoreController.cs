using UnityEngine;

public class a_ScoreController : MonoBehaviour
{
    public int score = 0; // スコアの初期値
    public string targetTag; // 指定されたタグ

    // オブジェクトがトリガーに入った時に呼び出される
    void OnTriggerEnter(Collider other)
    {
        // オブジェクトが指定されたタグを持っているか確認
        if (other.gameObject.tag == targetTag)
        {
            score++;
            Debug.Log("現在のスコア: " + score);
        }
    }
}
