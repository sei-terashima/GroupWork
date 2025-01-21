using UnityEngine;

public class a_MainCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("カメラの回転速度を制御する係数")]
    public float cameraMovementFactor = 0.5f;

    [Tooltip("垂直方向の回転角度の最大制限（正負の角度）")]
    public float verticalRotationLimit = 80f;

    private Vector2 lastMousePosition = Vector2.zero; // マウスの最後の位置
    private Vector2 joystickInput = Vector2.zero; // ジョイスティックの入力値
    private bool isUsingMouse = true; // 現在の操作モードがマウスかどうか

    void Start()
    {
        // マウスの初期位置を記録
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // 入力モードを切り替える
        if (Input.touchCount > 0) // タッチ操作が検出された場合
        {
            SwitchToTouchMode();
            HandleTouchInput(); // タッチ操作を処理
        }
        else if (Input.GetMouseButton(0)) // マウス操作が検出された場合
        {
            SwitchToMouseMode();
            // マウス操作時は何もしない
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 1つ目のタッチを取得

            // タッチの移動量を計算
            Vector2 deltaTouchPosition = touch.deltaPosition;

            // 水平方向の回転
            transform.Rotate(Vector3.up, -deltaTouchPosition.x * cameraMovementFactor, Space.World);

            // 垂直方向の回転
            transform.Rotate(Vector3.right, deltaTouchPosition.y * cameraMovementFactor, Space.Self);

            // 垂直方向の回転角度を制限
            ClampVerticalRotation();
        }
    }

    private void ClampVerticalRotation()
    {
        Vector3 eulerAngles = transform.eulerAngles;

        // オイラー角を-180度～180度に変換して制限
        float normalizedX = (eulerAngles.x > 180f) ? eulerAngles.x - 360f : eulerAngles.x;
        normalizedX = Mathf.Clamp(normalizedX, -verticalRotationLimit, verticalRotationLimit);

        // 修正後の回転角度を適用（Z軸は固定）
        transform.eulerAngles = new Vector3(normalizedX, eulerAngles.y, 0f);
    }

    private void SwitchToTouchMode()
    {
        if (isUsingMouse)
        {
            isUsingMouse = false;
            // タッチ操作が有効な場合は特にジョイスティックの表示などは不要なのでそのまま
        }
    }

    private void SwitchToMouseMode()
    {
        if (!isUsingMouse)
        {
            isUsingMouse = true;
            // マウス操作時には何もしない
        }
    }
}
