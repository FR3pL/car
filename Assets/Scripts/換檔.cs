using UnityEngine;
using UnityEngine.UI;

public class GearIndicator : MonoBehaviour
{
    public ManualTransmissionCarController carController; // 汽車控制腳本的引用
    public Text gearText; // 顯示檔位的Text元素

    void Update()
    {
        int currentGear = carController.GetCurrentGear();
        gearText.text = "Gear: " + (currentGear == -1 ? "R" : currentGear == 0 ? "N" : currentGear.ToString());
    }
}
