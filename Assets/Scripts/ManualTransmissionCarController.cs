using UnityEngine;

public class ManualTransmissionCarController : MonoBehaviour
{
    public float maxTurnAngle = 45.0f;
    public float maxBrakeForce = 20.0f;
    public float acceleration = 10.0f;
    public float[] gearSpeeds;
    public float maxReverseSpeed = 10.0f;

    private Rigidbody carRigidbody;
    private int currentGear = 0; // 預設N檔
    private bool clutchPressed = false;
    private float currentSpeed = 0.0f;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 判斷換檔
        if (Input.GetMouseButtonDown(0))
        {
            currentGear = Mathf.Clamp(currentGear + 1, 0, 3); // 增加檔位
            currentSpeed = Mathf.Max(currentSpeed, gearSpeeds[currentGear - 1]);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            currentGear = Mathf.Clamp(currentGear - 1, 0, 3); // 降低檔位
            currentSpeed = Mathf.Max(currentSpeed, gearSpeeds[currentGear - 1]);
        }

        // 判斷離合器
        clutchPressed = Input.GetKey(KeyCode.LeftControl);

        // 控制轉彎
        float turnAngle = 0.0f;
        if (currentSpeed > 0 || currentSpeed < 0)
        {
            turnAngle = Input.GetAxis("Horizontal") * maxTurnAngle;
            transform.rotation *= Quaternion.Euler(0, turnAngle * Time.deltaTime, 0);
        }

        // 控制剎車和油門
        float brakeForce = Input.GetKey(KeyCode.S) ? maxBrakeForce : 0;
        float throttle = 0.0f;
        if (currentGear > 0)
        {
            throttle = Input.GetKey(KeyCode.W) ? acceleration : 0;
        }
        else if (currentGear == 0)
        {
            throttle = Input.GetKey(KeyCode.W) ? 0 : 0; // N檔時不能前進
        }
        else if (currentGear == -1)
        {
            throttle = Input.GetKey(KeyCode.W) ? -acceleration : 0; // R檔時後退
        }

        // 計算目標速度
        float targetSpeed = 0.0f;
        if (currentGear > 0)
        {
            targetSpeed = gearSpeeds[currentGear - 1] * throttle;
        }
        else if (currentGear == 0)
        {
            targetSpeed = currentSpeed;
        }
        else if (currentGear == -1)
        {
            targetSpeed = Mathf.Min(currentSpeed, -maxReverseSpeed);
        }

        // 根據離合器控制速度
        if (!clutchPressed)
        {
            targetSpeed = Mathf.Min(targetSpeed, currentSpeed);
        }

        // 更新速度
        Vector3 velocity = transform.forward * targetSpeed;
        velocity.y = carRigidbody.velocity.y;
        carRigidbody.velocity = velocity;

        // 換檔時檢查是否要降檔
        if (currentGear > 0 && throttle > 0 && currentSpeed >= gearSpeeds[currentGear - 1])
        {
            currentGear--;
        }
    }

    // 取得目前檔位
    public int GetCurrentGear()
    {
        return currentGear;
    }
}
