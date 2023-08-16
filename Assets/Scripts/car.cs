using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    public Text speedText;        // 显示速度的文本组件
    public Text gearText;         // 显示档位的文本组件

    private CarUI carController; // 对应的汽车控制器脚本
    private object CurrentSpeed;
    private string CurrentGear;

    private void Start()
    {
        carController = GetComponent<CarUI>();
    }

    private void Update()
    {
        // 更新UI上的速度和档位显示
        //speedText.text = "Speed: " + carController.CurrentSpeed.("F1") + " km/h";
        //gearText.text = "Gear: " + carController.CurrentGear;
    }
}
