using UnityEngine;

namespace car
{
    /// <summary>
    /// 場景管理器:切換場景與退出遊戲
    /// </summary>
    public class NewBehaviourScript : MonoBehaviour
    {
        //按鈕與程式溝通方式
        public void ChangeScene()
        {
            print("切換場景");
            UnityEngine.SceneManagement.SceneManager.LoadScene("遊戲畫面");
        }

        public void Quit()
        {
            print("退出遊戲");
            Application.Quit();
        }
    }
}

