using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_MainCamera : MonoBehaviour
{
    public float cameraMovementFactor = 0.5f; // カメラの回転速度を制御するための係数
    private Vector3 lastMousePosition = Vector3.zero; // 最後のマウスの位置
    private Vector3 deltaMousePosition = Vector3.zero; // マウスの移動量

    void Start()
    {
        lastMousePosition = Input.mousePosition; // 最初のマウスの位置を記録する
    }

    // Update is called once per frame
    void Update()
    {
        deltaMousePosition = lastMousePosition - Input.mousePosition; // マウスの移動量を計算する
        lastMousePosition = Input.mousePosition; // 現在のマウスの位置を更新する

        // 水平方向の回転
        transform.Rotate(Vector3.up, -deltaMousePosition.x * cameraMovementFactor, Space.World);

        // 垂直方向の回転
        transform.Rotate(Vector3.right, deltaMousePosition.y * cameraMovementFactor, Space.Self);
    }
}
