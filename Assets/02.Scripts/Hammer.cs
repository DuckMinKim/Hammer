using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Hammer : MonoBehaviour
{
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


    void Update()
    {
        Vector3 mousePos = AdjustMousePositionForSimulator(Input.mousePosition);
        mousePos.z = Camera.main.nearClipPlane + 10f; // nearClipPlane
        point = Camera.main.ScreenToWorldPoint(mousePos);



        Collider2D col2d = Physics2D.OverlapCircle(point, radius, playerLayerMask);
        Collider2D testBox = Physics2D.OverlapCircle(point, radius, boxLayerMask);
        Collider2D nailCol2d = Physics2D.OverlapCircle(point, radius, nailLayerMask);
        Collider2D glassCol2d = Physics2D.OverlapCircle(point, radius, glassLayerMask);

        if (Input.GetMouseButtonDown(0))
            SoundManager.Instance.PlaySound(hammerClip);


        if (col2d != null && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(col2d.GetComponent<Player>().RestartScene(0.3f));
        }

        hammerIcon.position = AdjustMousePositionForSimulator(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && count > 0 )
        {
            GameObject _nail = Instantiate(nail, point, Quaternion.identity);

            if(testBox != null)
                AttachBox(_nail, testBox);

            count -= 1;
        }

        if(nailCol2d != null && Input.GetMouseButtonDown(1))
        {
            Destroy(nailCol2d.gameObject);
        }

        if(glassCol2d != null && Input.GetMouseButtonDown(0))
        {
            glassCol2d.GetComponent<Glass>().Breaking();
            SoundManager.Instance.PlaySound(breakClip);
        }

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

        //hinge.anchor = _nail.transform.localPosition;
        hinge.anchor = box.transform.InverseTransformPoint(_nail.transform.position);

        //hinge.useLimits = false; 
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject go = EventSystem.current.currentSelectedGameObject;
            return go != null && go.GetComponent<Button>() != null; 
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            GameObject go = EventSystem.current.currentSelectedGameObject;
            return go != null && go.GetComponent<Button>() != null;
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
