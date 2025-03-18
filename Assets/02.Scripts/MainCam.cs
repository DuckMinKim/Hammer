using UnityEngine;

public class MainCam : MonoBehaviour
{
    void Start()
    {
        SetAspectRatio(16f / 9f);  // 16:9 ���� ����
    }

    void SetAspectRatio(float targetAspect)
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera cam = Camera.main;
        if (scaleHeight < 1.0f)
        {
            // ȭ���� 16:9���� ���̰� �� ū ��� (��: 18:9, 20:9 ������ ��)
            cam.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else
        {
            // ȭ���� 16:9���� ���ΰ� �� ���� ��� (��: 4:3, 5:4 ������ �º�)
            float scaleWidth = 1.0f / scaleHeight;
            cam.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }
}
