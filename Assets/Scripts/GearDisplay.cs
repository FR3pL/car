using UnityEngine;
using UnityEngine.UI;

public class GearDisplay : MonoBehaviour
{
    public ManualTransmission transmissionScript; // 參考到 ManualTransmission 腳本
    public Text gearText; // 顯示檔位的文字 UI 元件

    private void Update()
    {
        int currentGear = transmissionScript.gear; // 取得目前檔位的數值
        string gearString = ""; // 儲存檔位文字的字串

        // 使用 switch 陳述式根據檔位數值設定對應的文字
        switch (currentGear)
        {
            case 0:
                gearString = "N"; // N 檔
                break;
            case 1:
                gearString = "1"; // 一檔
                break;
            case 2:
                gearString = "2"; // 二檔
                break;
            case 3:
                gearString = "3"; // 三檔
                break;
            case 8:
                gearString = "R"; // R 檔
                break;
        }

        // 將設定好的檔位文字更新到 UI 元件上
        gearText.text = "Gear: " + gearString;
    }
}

