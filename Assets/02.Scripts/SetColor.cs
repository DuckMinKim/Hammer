using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SetColor;
using static UnityEngine.Rendering.DebugUI;

public class SetColor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Image image;
    Text text;
    ParticleSystem particle;

    public enum ColodMod
    {
        Normal,
        Background,
        DeadZone,
        Glass
    }

    public ColodMod colodMod;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        text = GetComponent<Text>();
        particle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        switch (colodMod)
        {
            case ColodMod.Normal: ApplyColor(GameObject.Find("ColorManager").GetComponent<ColorManager>().color); break;
            case ColodMod.Glass: ApplyColor(GameObject.Find("ColorManager").GetComponent<ColorManager>().glass); break;
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

        if(particle != null)
        {
            var main = particle.main;
            main.startColor = newColor;
        }
    }

}
