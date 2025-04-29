using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Hammer : MonoBehaviour
{
    InputActionAsset inputActions;
    InputAction fire1Action;
    InputAction fire2Action;
    InputAction moveCursorAction;
    float cursorSpeed = 1000f;
    Vector2 currentMousePos;


    [SerializeField] Transform hammerIcon;
    [SerializeField] GameObject nail;
    [SerializeField] int count;
    [SerializeField] Text countText;
    Vector3 point;

    [SerializeField] GameObject hammer;
    [SerializeField] float radius;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] LayerMask boxLayerMask;
    [SerializeField] LayerMask nailLayerMask;
    [SerializeField] LayerMask glassLayerMask;

    [SerializeField] AudioClip hammerClip;
    [SerializeField] AudioClip breakClip;

    void Start()
    {
        Cursor.visible = false;
    }

    void Awake()
    {
        StopAllCoroutines();
        if (GameObject.Find("ControllerManager") != null)
            inputActions = GameObject.Find("ControllerManager").GetComponent<ControllerManager>().inputActions;

        moveCursorAction = inputActions.FindActionMap("Player").FindAction("MoveCursor");
        fire1Action = inputActions.FindActionMap("Player").FindAction("Fire1");
        fire2Action = inputActions.FindActionMap("Player").FindAction("Fire2");
    }

    private void OnEnable() {
        moveCursorAction.Enable();
        fire1Action.Enable();
        fire2Action.Enable();

        fire1Action.performed += OnL_Click;
        fire2Action.performed += OnR_Click;


        Vector2 centerPos = new(Screen.width / 2f, Screen.height / 2f);
        Mouse.current.WarpCursorPosition(centerPos);
        currentMousePos = centerPos;
        //currentMousePos = Mouse.current.position.ReadValue();
    }

    private void OnDisable()
    {
        fire1Action.Disable();
        fire2Action.Disable();

        fire1Action.performed -= OnL_Click;
        fire2Action.performed -= OnR_Click;
    }
    private void OnL_Click(InputAction.CallbackContext context)
    {
        Collider2D col2d = Physics2D.OverlapCircle(point, radius, playerLayerMask);
        Collider2D testBox = Physics2D.OverlapCircle(point, radius, boxLayerMask);
        Collider2D glassCol2d = Physics2D.OverlapCircle(point, radius, glassLayerMask);

        if (fire1Action.ReadValue<float>() != 0)
            SoundManager.Instance.PlaySound(hammerClip);


        if (col2d != null && fire1Action.ReadValue<float>() != 0 )
        {
            if (col2d.TryGetComponent<Player>(out var player))
            {
                player.Restart(0.3f);
            }
        }


        if (fire1Action.ReadValue<float>() != 0 && count > 0)
        {
            GameObject _nail = Instantiate(nail, point, Quaternion.identity);

            if (testBox != null)
                AttachBox(_nail, testBox);

            count -= 1;
        }

        

        if (glassCol2d != null && fire1Action.ReadValue<float>() != 0)
        {
            glassCol2d.GetComponent<Glass>().Breaking();
            SoundManager.Instance.PlaySound(breakClip);
        }
    }
    private void OnR_Click(InputAction.CallbackContext context) {
        Collider2D nailCol2d = Physics2D.OverlapCircle(point, radius, nailLayerMask);

        if (nailCol2d != null && fire2Action.ReadValue<float>() != 0)
        {
            Destroy(nailCol2d.gameObject);
        }
    }
    void Update()
    {

        Vector2 mouseRaw = Mouse.current.position.ReadValue();
        Vector2 moveInput = moveCursorAction.ReadValue<Vector2>();

        if (moveInput.sqrMagnitude < 0.01f)
        {
            currentMousePos = mouseRaw;
        }
        else
        {
            currentMousePos += moveInput * cursorSpeed * Time.deltaTime;
            Mouse.current.WarpCursorPosition(currentMousePos);
            InputState.Change(Mouse.current.position, currentMousePos);
        }



        Vector3 mousePos = AdjustMousePositionForSimulator(Input.mousePosition);
        mousePos.z = Camera.main.nearClipPlane + 10f; // nearClipPlane
        point = Camera.main.ScreenToWorldPoint(mousePos);

        hammerIcon.position = AdjustMousePositionForSimulator(Input.mousePosition);


        transform.position = point;

        countText.text = "Count: " + count;
    }

    Vector3 AdjustMousePositionForSimulator(Vector3 mousePos)
    {
        Rect safeArea = Screen.safeArea;
        mousePos.x = Mathf.Clamp(mousePos.x, safeArea.xMin, safeArea.xMax);
        mousePos.y = Mathf.Clamp(mousePos.y, safeArea.yMin, safeArea.yMax);
        return mousePos;
    }
    void AttachBox(GameObject _nail, Collider2D box)
    {
        _nail.transform.SetParent(box.transform.GetChild(0));

        HingeJoint2D hinge = box.AddComponent<HingeJoint2D>();
        hinge.connectedBody = _nail.GetComponent<Rigidbody2D>();

        hinge.anchor = box.transform.InverseTransformPoint(_nail.transform.position);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
