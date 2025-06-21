using UnityEngine;

public class GearRotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    Rigidbody2D rb2;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
        //transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb2.MoveRotation(rb2.rotation + rotateSpeed * Time.fixedDeltaTime);
    }

    public void SetRotateSpeed(float speed)
    {
        rotateSpeed = speed;
    }

    public void FlipRotation()
    {
        rotateSpeed *= -1;
    }
}
