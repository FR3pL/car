using UnityEngine;
using UnityEngine.UI;

public class GearIndicator : MonoBehaviour
{
    public ManualTransmissionCarController carController;
    public Text gearText;

    private void Update()
    {
        int currentGear = carController.GetCurrentGear();
        gearText.text = "Gear: " + currentGear;
    }
}
