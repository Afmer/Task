using UnityEngine;
using UnityEngine.UI;
namespace Task.ControllerUI
{
    public class ShutdownButton : MonoBehaviour
    {
        [SerializeField]
        private Button _shutdownButton;
        private void Start()
        {
            _shutdownButton.onClick.AddListener(OnClickShutdownButton);
        }
        private void OnClickShutdownButton()
        {
#if UNITY_ANDROID
            // �������� ����� AndroidJavaObject.Call() � ���������� "finish"
            // ��� �������� ������� ���������� (����������)
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                .GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("finish");
#else
            // �� ������ ���������� ����� ��� �� �����������
            Application.Quit();
#endif
        }
    }
}
