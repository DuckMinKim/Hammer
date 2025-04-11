using UnityEngine;
using UnityEngine.Events;

public class PedalButton : MonoBehaviour
{
    [SerializeField] LayerMask canPressObjects;
    [SerializeField] UnityEvent doAnything;
    [SerializeField] UnityEvent doEndAnything;
    Animator anim;
    [SerializeField] AudioClip pressSound;
    [SerializeField] BoxCollider2D pedalColider2d;

    [SerializeField] bool isHold;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }


    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col != null && col.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        anim.SetBool("isPressed?", true);
    //        doAnything.Invoke();
    //        SoundManager.Instance.PlaySound(pressSound);
    //        pedalColider2d.enabled = false;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null && (canPressObjects & (1 << col.gameObject.layer)) != 0 && !isHold)
        {
            anim.SetBool("isPressed?", true);
            doAnything.Invoke();
            SoundManager.Instance.PlaySound(pressSound);
            pedalColider2d.enabled = false;
        }

        if (col != null && (canPressObjects & (1 << col.gameObject.layer)) != 0 && isHold)
        {
            anim.SetBool("isPressed?", true);
            doAnything.Invoke();
            SoundManager.Instance.PlaySound(pressSound);
            //pedalColider2d.enabled = false;
        }
    }

    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col != null && col.gameObject.layer == LayerMask.NameToLayer("Box"))
    //    {
    //        anim.SetBool("isPressed?", true);
    //        doAnything.Invoke();
    //        SoundManager.Instance.PlaySound(pressSound);
    //        pedalColider2d.enabled = false;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col != null && (canPressObjects & (1 << col.gameObject.layer)) != 0 && isHold)
        {
            anim.SetBool("isPressed?", false);
            SoundManager.Instance.PlaySound(pressSound);

            doEndAnything?.Invoke();
        }
        
    }
}
