using UnityEngine;
using UnityEngine.Events;

public class PedalButton : MonoBehaviour
{
    [SerializeField] UnityEvent doAnything;
    Animator anim;
    [SerializeField] AudioClip pressSound;

    [SerializeField] BoxCollider2D pedalColider2d;
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
        if (col != null && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            anim.SetBool("isPressed?", true);
            doAnything.Invoke();
            SoundManager.Instance.PlaySound(pressSound);
            pedalColider2d.enabled = false;
        }
    }
}
