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
    }

    public void DoorOpen()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveDoors(leftDoorOpenPos, rightDoorOpenPos));


        Invoke(nameof(DoorClose), openDurationBeforeClose);
    }

    public void DoorJustOpen()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveDoors(leftDoorOpenPos, rightDoorOpenPos));
    }

    public void DoorClose()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveDoors(leftDoorClosedPos, rightDoorClosedPos));
    }

    private IEnumerator MoveDoors(Vector3 leftTarget, Vector3 rightTarget)
    {

        if (leftDoor == null || rightDoor == null)
            yield break;

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
}
