using UnityEngine;

public class a_BallShooter : MonoBehaviour
{
    public GameObject ballPrefs; // 生成するボールのプレハブ
    public Transform ballParentTransform; // 生成したボールをまとめる親オブジェクト
    public float shotForce = 10.0f; // ボールを発射する力
    public float shotTorque = 10.0f; // ボールを回転させる力
    public float upForce = 10.0f; // 上向きの力

    private Rigidbody ballRigidbody; // 現在のボールのRigidbodyコンポーネント
    private float holdTime; // マウスボタンが押されている時間

    void Update()
    {
        // 左クリックを押し続けている間の時間を計測
        if (Input.GetButton("Fire1"))
        {
            holdTime += Time.deltaTime; // 長押し時間を計測
        }

        // 左クリックを離した際にボールを発射
        if (Input.GetButtonUp("Fire1"))
        {
            ShootBall(); // ボールを発射
            holdTime = 0f; // 長押し時間をリセット
        }
    }

    void ShootBall()
    {
        // 指定されたボールのプレハブを生成
        GameObject currentBall = Instantiate(ballPrefs, transform.position, transform.rotation);

        // 生成したボールの親オブジェクトを指定
        currentBall.transform.parent = ballParentTransform;

        // 生成したボールのRigidbodyコンポーネントを取得
        ballRigidbody = currentBall.GetComponent<Rigidbody>();

        // ボールのRigidbodyに力を加えて発射
        ballRigidbody.AddForce(transform.forward * shotForce);

        // 長押し時間に基づいて上向きの力を加える
        float upwardForce = holdTime * upForce;
        ballRigidbody.AddForce(Vector3.up * upwardForce);

        // ボールのRigidbodyにトルクを加えて回転させる
        ballRigidbody.AddTorque(new Vector3(0, shotTorque, 0));
    }
}
