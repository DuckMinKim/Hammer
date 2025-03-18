using UnityEngine;

public class MainCam : MonoBehaviour
{
    void Start()
    {
        SetAspectRatio(16f / 9f);  // 16:9 비율 유지
    }

    void SetAspectRatio(float targetAspect)
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera cam = Camera.main;
        if (scaleHeight < 1.0f)
        {
            // 화면이 16:9보다 높이가 더 큰 경우 (예: 18:9, 20:9 비율의 폰)
            cam.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else
        {
            // 화면이 16:9보다 가로가 더 넓은 경우 (예: 4:3, 5:4 비율의 태블릿)
            float scaleWidth = 1.0f / scaleHeight;
            cam.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }
}
