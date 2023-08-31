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
    private bool brakePressed = false;
    private float currentSpeed = 0.0f;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 判斷換檔
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentGear = 0; // N檔
            currentSpeed = 0.0f;
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
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentGear = -1; // R檔
        }

        // 判斷離合器和剎車
        clutchPressed = Input.GetKey(KeyCode.Space);
        brakePressed = Input.GetKey(KeyCode.S);

        // 控制轉彎
        float turnAngle = 0.0f;
        if (currentSpeed != 0)
        {
            turnAngle = Input.GetAxis("Horizontal") * maxTurnAngle;
            transform.rotation *= Quaternion.Euler(0, turnAngle * Time.deltaTime, 0);
        }

        // 控制剎車和油門
        float throttle = 0.0f;
        float brakeForce = brakePressed ? maxBrakeForce : 0;

        // 根據檔位控制油門
        if (!clutchPressed) // 如果沒有踩離合器
        {
            if (currentGear > 0)
            {
                throttle = Input.GetKey(KeyCode.W) ? acceleration : 0;
            }
            else if (currentGear == -1)
            {
                throttle = Input.GetKey(KeyCode.W) ? -acceleration : 0; // R檔時後退
            }
        }

        // 計算目標速度並根據離合器控制速度
        float targetSpeed = 0.0f;
        if (currentGear > 0)
        {
            targetSpeed = gearSpeeds[currentGear - 1];
        }
        else if (currentGear == -1)
        {
            targetSpeed = -maxReverseSpeed;
        }

        if (!clutchPressed)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
        }

        // 如果在一檔至三檔，速度小於一檔最大速度的一半，且剎車被按下，則進入N檔
        if (currentGear > 0 && currentGear <= 3 && currentSpeed < gearSpeeds[currentGear - 1] / 2 && brakePressed)
        {
            currentGear = 0; // 進入N檔
            currentSpeed = 0.0f;
        }

        // 更新速度
        Vector3 velocity = transform.forward * currentSpeed;
        velocity.y = carRigidbody.velocity.y;
        carRigidbody.velocity = velocity;
    }

    // 取得目前檔位
    public int GetCurrentGear()
    {
        return currentGear;
    }
}
