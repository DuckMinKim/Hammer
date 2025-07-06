using UnityEngine;

public class ChainFreezing : MonoBehaviour
{
    //[SerializeField]
    //GameObject anchor;
    [SerializeField]    
    Rigidbody2D rb2;

    [SerializeField]
    float fixedY;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 pos = rb2.position;
        pos.y = transform.position.y+ fixedY;
        //pos.x = Mathf.Clamp(pos.x, transform.position.x - transform.localScale.x/2, transform.position.x + transform.localScale.x/2);
        rb2.position = pos;
        //anchor.transform.localPosition = pos;

        //rb2.position = pos;
    }
}
