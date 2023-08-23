using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cm : MonoBehaviour
{
    public Rigidbody carRigidbody; // 車輛的Rigidbody
    public TMP_Text  speedText; // 顯示時速的Text元素

    private void Update()
    {
        // 計算車輛的時速
        float speed = carRigidbody.velocity.magnitude * 3.6f; // 將米/秒轉換為公里/小時

        // 更新時速表的顯示
        speedText.text = "Speed: " + Mathf.Round(speed) + " km/h";
    }
}


