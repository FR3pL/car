using UnityEngine;
using UnityEngine.UI;

public class GearIndicator : MonoBehaviour
{
    public ManualTransmissionCarController carController;
    public Text gearText;

    private void Update()
    {
        // 取得目前檔位
        int currentGear = carController.GetCurrentGear();
        string gearName = currentGear == 0 ? "N" : (currentGear > 0 ? currentGear.ToString() : "R");

        // 顯示目前檔位
        gearText.text = "Gear: " + gearName;
    }
}
