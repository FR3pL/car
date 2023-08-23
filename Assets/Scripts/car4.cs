using UnityEngine;

public class ManualTransmissionCarController : MonoBehaviour
{
    public float[] gearSpeeds; // 每個檔位對應的速度
    public float accelerationRate = 5.0f; // 加速度
    public float brakeForce = 10.0f; // 剎車力
    public float maxTurnAngle = 45.0f; // 最大轉彎角度
    public float maxSpeedForGearChange = 40.0f; // 需要達到的最小速度才能換檔

    private Rigidbody rb;
    private int currentGear = 1; // 當前檔位
    private bool isClutchEngaged = false; // 離合器是否啟動
    public int GetCurrentGear()
    {
        return currentGear;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 檔位控制
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentGear = 0; // 空檔
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            currentGear = -1; // 倒車檔
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentGear < gearSpeeds.Length)
        {
            currentGear++;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentGear > -1)
        {
            currentGear--;
        }

        // 離合器控制
        isClutchEngaged = Input.GetKey(KeyCode.LeftControl);
    }

    void FixedUpdate()
    {
        // 轉向控制
        float turnAngle = Input.GetAxis("Horizontal") * maxTurnAngle;
        transform.rotation = Quaternion.Euler(0, turnAngle, 0);

        // 行駛和剎車
        float acceleration = Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;
        float targetSpeed = currentGear >= 0 ? gearSpeeds[currentGear] : gearSpeeds[0] * 0.5f; // 倒車速度為一半
        if (acceleration > 0 && rb.velocity.magnitude < targetSpeed)
        {
            rb.AddForce(transform.forward * accelerationRate * acceleration, ForceMode.Acceleration);
        }
        else if (acceleration < 0)
        {
            rb.AddForce(-transform.forward * brakeForce * Mathf.Abs(acceleration), ForceMode.Force);
        }

        // 根據離合器狀態控制剎車
        if (isClutchEngaged)
        {
            rb.velocity *= 0.95f; // 模擬剎車時的慣性
        }
    }
}
