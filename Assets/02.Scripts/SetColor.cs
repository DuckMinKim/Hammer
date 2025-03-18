using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SetColor;
using static UnityEngine.Rendering.DebugUI;

public class SetColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image image;
    private Text text;

    public enum ColodMod
    {
        Normal,
        Background,
        DeadZone
    }

    public ColodMod colodMod;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        text = GetComponent<Text>();

        switch (colodMod)
        {
            case ColodMod.Normal: ApplyColor(GameObject.Find("ColorManager").GetComponent<ColorManager>().color); break;
            case ColodMod.Background: ApplyColor(GameObject.Find("ColorManager").GetComponent<ColorManager>().background); break;
            case ColodMod.DeadZone: ApplyColor(GameObject.Find("ColorManager").GetComponent<ColorManager>().deadZone); break;
            default: break;
        }

    }

    private void ApplyColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }

        if (image != null)
        {
            image.color = newColor;
        }

        if(text != null)
        {
            text.color = newColor;
        }
    }

}
