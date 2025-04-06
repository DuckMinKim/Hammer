using UnityEngine;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
    }

    public void FadeIn()
    {
        anim.SetBool("isFade", false);
    }

    public void FadeOut()
    {
        anim.SetBool("isFade", true);

    }
}
