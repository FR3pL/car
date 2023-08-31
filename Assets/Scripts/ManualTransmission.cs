using UnityEngine;

public class ManualTransmission : MonoBehaviour
{
    public int gear = 8; // 目前檔位，8 代表 R 檔
    private float speed = 0.0f; // 目前車速
    private bool isClutchPressed = false; // 是否按下離合器

    private void Update()
    {
        // 根據玩家輸入切換檔位
        //if (Input.GetKeyDown(KeyCode.Alpha0)) ChangeToNeutral();
        //if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGear(1);
        //if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeGear(2);
        //if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeGear(3);
        //if (Input.GetKeyDown(KeyCode.Alpha8)) ChangeToReverse();
        print(gear);

        // 離合器控制邏輯
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isClutchPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isClutchPressed = false;
        }

        // 檔位切換邏輯
        if (isClutchPressed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (gear == 0 || gear == 2)
                {
                    ChangeGear(1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (gear == 1 || gear == 3)
                {
                    ChangeGear(2);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (gear == 2)
                {
                    ChangeGear(3);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (gear == 0)
                {
                    ChangeToReverse();
                }
            }
        }

        // 控制速度邏輯
        if (speed < GetMaxSpeedForGear(gear))
        {
            speed += Time.deltaTime * 10.0f; // 調整速度增加率
        }
    }

    private void ChangeToNeutral()
    {
        gear = 0; // 切換到 N 檔
    }

    private void ChangeToReverse()
    {
        if (gear == 0)
        {
            gear = 8; // 切換到 R 檔
        }
    }

    private void ChangeGear(int newGear)
    {
        if (gear == 0 || (gear == 8 && newGear == 1))
        {
            gear = newGear; // 允許切換檔位的情況下，切換檔位
        }
    }

    private float GetMaxSpeedForGear(int gear)
    {
        if (gear == 8) return GetMaxSpeedForGear(1) * 0.5f; // R 檔最大速度為一檔的一半
        return gear * 10.0f; // 根據檔位設定速度值，可以進行調整
    }
}
