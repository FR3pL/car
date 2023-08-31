using UnityEngine;

public class ManualTransmission : MonoBehaviour
{
    public int gear = 8; // 初始檔位為 R 檔 (8)
    private float speed = 0.0f;
    private bool isClutchPressed = false;

    private void Update()
    {
        // 離合器控制
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isClutchPressed = true; // 當按下空白鍵時，表示離合器被按下
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isClutchPressed = false; // 當放開空白鍵時，表示離合器被釋放
        }

        // 檔位切換邏輯
        if (isClutchPressed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (gear == 0 || gear == 2)
                {
                    ChangeGear(1); // 變換到一檔
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (gear == 1 || gear == 3)
                {
                    ChangeGear(2); // 變換到二檔
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (gear == 2)
                {
                    ChangeGear(3); // 變換到三檔
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (gear == 0)
                {
                    ChangeToReverse(); // 變換到 R 檔
                }
            }
        }

        // 速度控制邏輯
        if (speed < GetMaxSpeedForGear(gear))
        {
            speed += Time.deltaTime * 10.0f; // 根據檔位遞增速度
        }

        if (!isClutchPressed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                gear = 0; // 變換到 N 檔
            }                
        }
    }    

    private void ChangeToReverse()
    {
        if (gear == 0)
        {
            gear = 8; // 變換到 R 檔
        }
    }

    private void ChangeGear(int newGear)
    {
        if (gear == 0 || (gear == 8 && newGear == 1))
        {
            gear = newGear; // 變換檔位
        }
    }

    private float GetMaxSpeedForGear(int gear)
    {
        if (gear == 8) return GetMaxSpeedForGear(1) * 0.5f;
        return gear * 10.0f; // 根據檔位計算最大速度
    }
}
