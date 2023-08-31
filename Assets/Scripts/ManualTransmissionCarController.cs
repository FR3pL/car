﻿using UnityEngine;

public class ManualTransmissionCarController : MonoBehaviour
{
    public float maxBrakeForce = 1000.0f;
    public float[] maxSpeedInGears = { 0.0f, 30.0f, 60.0f, 90.0f }; // 0檔, 1檔, 2檔, 3檔
    public float maxTurnSpeed = 5.0f;
    public float maxReverseSpeed = -30.0f;

    private int currentGear = 1;
    private float currentSpeed = 0.0f;
    private float maxSpeedInCurrentGear = 0.0f;

    private bool clutchPressed = false;
    private bool changingGear = false;

    private void Update()
    {
        // 檢查換檔輸入
        if (!changingGear)
        {
            // 檢查數字鍵盤輸入
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ChangeGear(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeGear(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeGear(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeGear(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                // 從0檔進入到一檔或者R檔
                if (currentGear == 0)
                {
                    ChangeGearWithClutch(1);
                }
                else if (currentGear != 8)
                {
                    ChangeGearWithClutch(8);
                }
            }
        }

        // 檢查離合器
        clutchPressed = Input.GetKey(KeyCode.Space);

        // 控制剎車和油門
        float throttle = 0.0f;
        float brakeForce = Input.GetKey(KeyCode.S) ? maxBrakeForce : 0;

        // 計算最大速度
        maxSpeedInCurrentGear = maxSpeedInGears[currentGear];

        // 控制轉向
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && (currentSpeed > 1.0f || currentSpeed < -1.0f))
        {
            float steeringAmount = Input.GetAxis("Horizontal");
            transform.Rotate(0, steeringAmount * maxTurnSpeed * Time.deltaTime, 0);
        }

        // 檢查是否換檔
        if (!changingGear && clutchPressed && !Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                StartCoroutine(ChangeGearWithClutch(0));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeGearWithClutch(1));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(ChangeGearWithClutch(2));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(ChangeGearWithClutch(3));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                StartCoroutine(ChangeGearWithClutch(8));
            }
        }

        // 控制速度
        if (!changingGear)
        {
            // 控制油門
            if (currentGear != 0)
            {
                throttle = Input.GetKey(KeyCode.W) ? 1.0f : 0.0f;
            }
            else if (currentSpeed > maxReverseSpeed)
            {
                throttle = Input.GetKey(KeyCode.W) ? -1.0f : 0.0f;
            }
            else
            {
                throttle = 0.0f;
            }

            // 設定汽車速度
            currentSpeed += (throttle * Time.deltaTime * 5.0f) - (brakeForce * Time.deltaTime * 0.1f);
            transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        }
    }

    // 變換檔位
    private void ChangeGear(int newGear)
    {
        currentGear = newGear;
        changingGear = false;
    }

    // 使用離合器換檔
    private System.Collections.IEnumerator ChangeGearWithClutch(int newGear)
    {
        changingGear = true;

        // 變換檔位並等待一會兒
        ChangeGear(newGear);
        yield return new WaitForSeconds(0.5f);

        changingGear = false;
    }

    // 取得目前檔位
    public int GetCurrentGear()
    {
        return currentGear;
    }
}
