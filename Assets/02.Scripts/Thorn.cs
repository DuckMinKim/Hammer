using UnityEngine;

public class Thorn : MonoBehaviour
{
    Rigidbody2D rb2;

    [SerializeField] bool isActivated;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] float raycastDistance;

    Vector2 pos;
    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            rb2.constraints = RigidbodyConstraints2D.None;
            rb2.WakeUp();
        }
    }

    private void FixedUpdate()
    {
        bool isRaycastPlayer = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, playerLayerMask);

        if (isRaycastPlayer)
        {
            isActivated = true;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - raycastDistance));
    }
}
