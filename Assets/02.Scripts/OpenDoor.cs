using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    private enum Mode
    {
        Horizontal,
        Vertical,
        Both
    }

    [SerializeField] private Mode useOpenSystem = Mode.Horizontal;

    [SerializeField] private float doorOpeningTime = 2f;
    [SerializeField] private float targetOpenAmount = 2f;
    [SerializeField] private float targetOpenAmount_y;
    [SerializeField] private float openDurationBeforeClose = 3f;

    [SerializeField] private GameObject leftDoor, rightDoor;
    [SerializeField] private float openSpeed;

    private Vector3 leftDoorClosedPos, rightDoorClosedPos;
    private Vector3 leftDoorOpenPos, rightDoorOpenPos;

    private Coroutine moveCoroutine;
    [SerializeField] GameObject gearTarget;
    public float gearZ, gearCurrentZ;




    private void Awake()
    {
        if(leftDoor == null)
        {
            leftDoor = new GameObject();
        }

        if (rightDoor == null)
        {
            rightDoor = new GameObject();
        }
    }

    void Start()
    {
        leftDoorClosedPos = leftDoor.transform.position;
        rightDoorClosedPos = rightDoor.transform.position;

        if (useOpenSystem == Mode.Horizontal)
        {
            leftDoorOpenPos = leftDoorClosedPos + Vector3.left * targetOpenAmount;
            rightDoorOpenPos = rightDoorClosedPos + Vector3.right * targetOpenAmount;
        }
        else if(useOpenSystem == Mode.Vertical)// Vertical
        {
            leftDoorOpenPos = leftDoorClosedPos + Vector3.down * targetOpenAmount;
            rightDoorOpenPos = rightDoorClosedPos + Vector3.up * targetOpenAmount;
        }
        else
        {
            leftDoorOpenPos = leftDoorClosedPos + Vector3.down * targetOpenAmount_y + Vector3.left * targetOpenAmount;
            rightDoorOpenPos = rightDoorClosedPos + Vector3.up * targetOpenAmount_y + Vector3.right * targetOpenAmount;
        }


        if(gearTarget != null)
        {
            gearZ = gearTarget.transform.eulerAngles.z;
        }
    }

    private void Update()
    {
        if (gearTarget != null)
        {
            MenuallyMoveDoors();
        }
    }

    public void DoorOpen(float waitTime)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveDoors(leftDoorOpenPos, rightDoorOpenPos, waitTime));


        Invoke(nameof(DoorClose), openDurationBeforeClose);
    }

    public void DoorJustOpen(float waitTime)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveDoors(leftDoorOpenPos, rightDoorOpenPos, waitTime));
    }

    public void DoorClose()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveDoors(leftDoorClosedPos, rightDoorClosedPos, 0));
    }

    private IEnumerator MoveDoors(Vector3 leftTarget, Vector3 rightTarget, float waitTime)
    {

        if (leftDoor == null || rightDoor == null)
            yield break;

        yield return new WaitForSeconds(waitTime);

        float elapsed = 0f;
        Vector3 leftStart = leftDoor.transform.position;
        Vector3 rightStart = rightDoor.transform.position;

        while (elapsed < doorOpeningTime)
        {
            if (leftDoor == null || rightDoor == null)
                yield break;

            float t = elapsed / doorOpeningTime;
            leftDoor.transform.position = Vector3.Lerp(leftStart, leftTarget, t);
            rightDoor.transform.position = Vector3.Lerp(rightStart, rightTarget, t);

            elapsed += Time.deltaTime * openSpeed;
            yield return null;
        }

        if (leftDoor != null)
        leftDoor.transform.position = leftTarget;
    if (rightDoor != null)
        rightDoor.transform.position = rightTarget;
    }

    private void MenuallyMoveDoors()
    {
        Rigidbody2D rb2Gear = gearTarget.GetComponent<Rigidbody2D>();

        float angularVelocity = rb2Gear.angularVelocity; 

        float rpm = Mathf.Abs(angularVelocity) / 360f * 60f;

        leftDoor.transform.Translate(0, rpm / 100f * Time.deltaTime, 0);
        rightDoor.transform.Translate(0, rpm / 100f * Time.deltaTime, 0);

        //Debug.Log("회전 속도 (RPM): " + rpm);


        //float t = 1;

        //gearCurrentZ = (gearZ - gearTarget.transform.eulerAngles.z) / 100f;

        //Vector3 leftTargetOffset = new(leftDoor.transform.position.x, gearCurrentZ, 0);
        //Vector3 rightTargetOffset = new(rightDoor.transform.position.x, -gearCurrentZ, 0);

        //Vector3 leftStart = leftDoor.transform.position;
        //Vector3 rightStart = rightDoor.transform.position;

        //leftDoor.transform.position = Vector3.Lerp(leftStart, leftTargetOffset, t);
        ////Debug.Log("z 값: " + gearCurrentZ);
        //rightDoor.transform.position = Vector3.Lerp(rightStart, -rightTargetOffset, t);

    }
}
