using System.Collections;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.IK;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2;
    Animator anim;
    SpriteRenderer sr;

    [SerializeField] LayerMask groundLayerMask;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 pushOffset;
    [SerializeField] float groundCheckRadius;
    [SerializeField] Vector2 groundCheckSize;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float gravity;
    [SerializeField] LoadImage ldImg;

    float h, v, j;
    bool isGrounded;
    bool isPushing;

    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip deadClip;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        if(h < 0 && moveSpeed >0) sr.flipX = true;
        else if(h > 0 && moveSpeed > 0) sr.flipX = false;

    }



    void FixedUpdate()
    {

        //isGrounded = Physics2D.OverlapCircle(transform.position + offset, groundCheckRadius, groundLayerMask);
        isGrounded = Physics2D.OverlapCapsule(transform.position + offset, groundCheckSize, CapsuleDirection2D.Horizontal,0, groundLayerMask);
        isPushing = Physics2D.OverlapCircle(transform.position + pushOffset * h, groundCheckRadius, groundLayerMask);
        
        anim.SetBool("isPushing?", isPushing);

        if (isGrounded && Input.GetButton("Jump"))
        {
            anim.SetBool("isGrounded?", true);
            anim.SetBool("isJump?", true);

            SoundManager.Instance.PlaySound(jumpClip);

            j = jumpPower;
        }
        else if (isGrounded)
        {
            j = -1;
            anim.SetBool("isGrounded?", true);
        }
        else
        {
            j -= gravity * Time.deltaTime;
            anim.SetBool("isGrounded?", false);
            anim.SetBool("isJump?", false);
        }

        rb2.linearVelocity = new Vector2(h * moveSpeed, j * jumpPower);
    }



    public IEnumerator RestartScene(float waitTime)
    {
        SoundManager.Instance.PlaySound(deadClip);
        ldImg.FadeOut();
        rb2.bodyType = RigidbodyType2D.Kinematic;
        moveSpeed = 0;
        jumpPower = 0;
        anim.SetBool("isDead?", true);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + pushOffset, groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, groundCheckSize);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null && col.gameObject.layer == LayerMask.NameToLayer("DeadZone")) {
            StartCoroutine(RestartScene(0.3f));
        }
    }

    public void MovePlayer(int h)
    {
        this.h = h;
    }
    
    
}
