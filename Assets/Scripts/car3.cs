using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car3 : MonoBehaviour
{
    public Transform LeftFont;
    public Transform RightFont;
    public Transform LeftBack;
    public Transform RightBack;

    private float y;
    //车辆控制速度参数
    private float speedOne = 0f; //车辆实时速度
    private float speedMax = 120f; //车辆最大速度
    private float speedMin = -20f; //车辆最小速度(倒车最大速度）
    private float speedUpA = 2f; //车辆加速加速度（A键控制）
    private float speedDownS = 4f; //车辆减速加速度（S键控制）
    private float speedTend = 0.5f; //无操作实时速度趋于0时加速度
    private float speedBack = 1f; //车辆倒车加速度
    private bool amp;
    float f = 10;
    float _speed;
    Vector3 curpos, lastpos;

    float Speed()
    {
        curpos = LeftFont.transform.position;//当前点 float
        _speed = (Vector3.Magnitude(curpos - lastpos) / Time.deltaTime);//与上一个点做计算除去当前帧花的时间。
        lastpos = curpos;//把当前点保存下一次用
        return _speed;
    }

        // Update is called once per frame
        void Update()

    {




        f += Speed();
        RightBack.transform.localEulerAngles = new Vector3(f, 0,0.0f);
        LeftBack.transform.localEulerAngles = new Vector3(f, 0, 0.0f);
        RightFont.transform.localEulerAngles = new Vector3(f, Input.GetAxis("Horizontal")*45, 0.0f);
        LeftFont.transform.localEulerAngles = new Vector3(f, Input.GetAxis("Horizontal") * 45, 0.0f);

        transform.Rotate(0, y, 0);
        //鼠标隐藏
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
        //按下W键并且速度没达到最大，则速度增加
        if (Input.GetKey(KeyCode.W) & speedOne < speedMax)
        {
            speedOne = speedOne + Time.deltaTime * speedUpA;
        }
        //按下S键并且速度没达到零，则速度减小
        if (Input.GetKey(KeyCode.S) & speedOne > 0f)
        {
            speedOne = speedOne - Time.deltaTime * speedDownS;
        }
        //没有执行速度操作并且速度大于最小速度，则缓慢操作
        if (!Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.S) && speedOne > 0f)
        {
            speedOne = speedOne - Time.deltaTime * speedTend;
        }
        if (!Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.S) && speedOne < 0f)
        {
            speedOne = speedOne + Time.deltaTime * speedTend;
        }

        //按下S键并且速度没有达到倒车速度最大时，且车辆处于可以倒车状态时车辆倒车
        if (Input.GetKey(KeyCode.S) && speedOne > speedMin && speedOne <= 0)
        {
            speedOne = speedOne - Time.deltaTime * speedBack;
        }

        //按下空格，则汽车停止
        if (Input.GetKey(KeyCode.Space) && speedOne != 0)
        {
            speedOne = Mathf.Lerp(speedOne, 0, 0.4f);
            if (speedOne < 5) speedOne = 0;
        }




        transform.Translate(Vector3.forward * speedOne * Time.deltaTime);
        //使用A和D来控制物体左右旋转
        if (speedOne > 1f || speedOne < -1f)
        {
            y = Input.GetAxis("Horizontal") * 60f * Time.deltaTime;
            transform.Rotate(0, y, 0);
        }

        ///* if (transform.eulerAngles.z != 0)
        // {
        //     transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        // }*/
    }
}


