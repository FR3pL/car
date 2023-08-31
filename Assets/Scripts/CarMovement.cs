using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public ManualTransmission transmissionScript; // 參考到 ManualTransmission 腳本
    public Transform frontWheels; // 前輪的 Transform
    public Transform rearWheels; // 後輪的 Transform
    public float maxSteeringAngle = 45.0f; // 最大轉彎角度
    public float brakeForce = 500.0f; // 剎車力道

    private Rigidbody carRigidbody; // 車輛的剛體
    private float currentSteeringAngle = 0.0f; // 目前轉彎角度
    private bool isBraking = false; // 是否正在剎車

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>(); // 取得車輛的剛體組件
    }

    private void Update()
    {
        // 轉向邏輯
        if (transmissionScript.gear != 0) // 只有在不在 N 檔時才轉向
        {
            float horizontalInput = Input.GetAxis("Horizontal"); // 玩家的水平輸入
            currentSteeringAngle = horizontalInput * maxSteeringAngle; // 根據輸入計算轉彎角度
        }
    }

    private void FixedUpdate()
    {
        // 加速和剎車邏輯
        if (transmissionScript.gear != 0) // 只有在不在 N 檔時才移動
        {
            if (Input.GetKey(KeyCode.W)) // 按下前進鍵
            {
                // 根據檔位調整加速力道
                if (transmissionScript.gear == 1)
                {
                    carRigidbody.AddForce(transform.forward * 1000.0f); // 調整加速力道
                }
                else if (transmissionScript.gear == 2)
                {
                    carRigidbody.AddForce(transform.forward * 1500.0f); // 調整加速力道
                }
                else if (transmissionScript.gear == 3)
                {
                    carRigidbody.AddForce(transform.forward * 2000.0f); // 調整加速力道
                }
                else if (transmissionScript.gear == 8) // R 檔
                {
                    carRigidbody.AddForce(-transform.forward * 500.0f); // 調整後退力道
                }
            }

            if (Input.GetKey(KeyCode.S)) // 按下剎車鍵
            {
                isBraking = true;
                carRigidbody.AddForce(-transform.forward * brakeForce); // 剎車力道
            }
            else
            {
                isBraking = false;
            }
        }
    }

    private void LateUpdate()
    {
        // 將轉向角度應用到輪子的轉向
        frontWheels.localEulerAngles = new Vector3(0, currentSteeringAngle, 0); // 設定前輪轉向角度
        //rearWheels.localEulerAngles = new Vector3(0, currentSteeringAngle, 0); // 設定後輪轉向角度
    }
}

