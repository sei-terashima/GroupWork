using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class a_GameManager : MonoBehaviour
{
    public static string gameState; // ゲームの状態
    public TextMeshProUGUI scoreText; // スコア
    public TextMeshProUGUI timeText; // 時間
    public GameObject statusText; // 開始・終了の表示
    public GameObject statusPanel; // パネル
    public a_BallShooter shooter; // Ballshooterスクリプト
    public a_ScoreController scoreController; // ScoreControllerスクリプト
    public float gameTime = 30.0f; // カウントダウン時間
    public string sceneToLoad = "a_Title"; // インスペクタから変更可能なシーン名

    public AudioClip winSound; // 勝利の効果音
    public AudioClip loseSound; // 敗北の効果音
    private AudioSource audioSource; // AudioSourceコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to the GameObject.");
        }

        Invoke("HideStatusText", 1.0f); // 1秒後にテキストを非表示にする
        gameState = "playing"; // ゲーム中にする
    }

    void HideStatusText()
    {
        statusText.SetActive(false);
        statusPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing") // プレイ状態でなければ何もしない
        {
            return;
        }
        gameTime -= Time.deltaTime; // カウントダウン
        timeText.text = gameTime.ToString("F0"); // タイム更新

        scoreText.text = scoreController.score + "/5"; // スコアを更新

        if (scoreController.score >= 5)
        {
            GameClear();
        }

        if (gameTime <= 0.0f)
        {
            TimeOver();
        }
    }

    void GameClear()
    {
        gameState = "GameClear"; // ゲームの状態をタイムオーバーに変更

        statusText.GetComponent<TextMeshProUGUI>().text = "YOU WIN!!!";
        statusText.SetActive(true);

        if (winSound != null)
        {
            audioSource.PlayOneShot(winSound); // 効果音を再生

            Invoke("ChangeScene", 3.0f); // 3秒後にシーンを変更
        }
    }

    void TimeOver()
    {
        gameState = "timeover"; // ゲームの状態をタイムオーバーに変更

        statusText.GetComponent<TextMeshProUGUI>().text = "YOU LOSE!";
        statusText.SetActive(true);

        if (loseSound != null)
        {
            audioSource.PlayOneShot(loseSound); // 効果音を再生

            Invoke("ChangeScene", 3.0f); // 3秒後にシーンを変更
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad); // インスペクタで指定されたシーンに移行
    }
}
