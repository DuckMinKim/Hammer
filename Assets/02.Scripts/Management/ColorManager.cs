using UnityEngine;
using UnityEngine.Events;

public class ColorManager : MonoBehaviour
{
    public Color color;
    public Color background;
    public Color deadZone;
    public Color glass;

    private void Awake()
    {
    }

    public void SetColor(Color newColor)
    {
        color = newColor;

    }
}
