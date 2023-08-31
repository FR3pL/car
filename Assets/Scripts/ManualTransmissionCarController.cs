using UnityEngine;

public class ManualTransmissionCarController : MonoBehaviour
{
    public float maxTurnAngle = 45.0f; // 最大轉彎角度
    public float maxBrakeForce = 20.0f; // 最大剎車力
    public float acceleration = 10.0f; // 油門加速度
    public float maxSpeed = 50.0f; // 最大速度
    public float[] gearSpeeds; // 每個檔位的速度

    private Rigidbody carRigidbody;
    private int currentGear = 0; // 目前檔位，0為空檔
    private bool clutchPressed = false;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    // 新增這個方法以返回目前檔位
    public int GetCurrentGear()
    {
        return currentGear;
    }

    private void Update()
    {
        // 控制檔位
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentGear = 0; // 空檔
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentGear = 1; // 一檔
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentGear = 2; // 二檔
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentGear = 3; // 三檔
        }

        // 控制離合器
        clutchPressed = Input.GetKey(KeyCode.LeftControl);

        // 控制轉彎
        float turnAngle = Input.GetAxis("Horizontal") * maxTurnAngle;
        transform.rotation *= Quaternion.Euler(0, turnAngle * Time.deltaTime, 0);

        // 控制剎車和油門
        float brakeForce = Input.GetKey(KeyCode.S) ? maxBrakeForce : 0;
        float throttle = Input.GetKey(KeyCode.W) ? acceleration : 0;

        // 根據離合器狀態計算速度
        float targetSpeed = gearSpeeds[currentGear] * throttle;
        if (!clutchPressed)
        {
            targetSpeed = Mathf.Min(targetSpeed, carRigidbody.velocity.magnitude);
        }

        // 更新速度
        Vector3 velocity = transform.forward * targetSpeed;
        velocity.y = carRigidbody.velocity.y; // 保持垂直速度不變
        carRigidbody.velocity = velocity;

        // 換檔時增加速度檢查
        if (currentGear > 0 && throttle > 0 && carRigidbody.velocity.magnitude >= gearSpeeds[currentGear])
        {
            currentGear--;
        }
    }
}
