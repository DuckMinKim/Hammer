using UnityEngine;
using UnityEngine.Rendering;

public class OpenDoor : MonoBehaviour
{
    private enum Mode
    {
        Horizontal,
        Vertical
    }
    private int step;
    [SerializeField] private Mode useOpenSystem;

    [SerializeField] private float doorOpeningTime;
    [SerializeField] private float targetOpenAmount;
    private float doorOpeningCurrentTime;

    [SerializeField] private GameObject leftDoor, rightDoor;
    private Vector2 leftDoorPos, rightDoorPos;

    void Start()
    {
        step = 0;
        doorOpeningCurrentTime = 0;


        leftDoorPos = leftDoor.transform.position;
        rightDoorPos = rightDoor.transform.position;   
    }


    void Update()
    {
        switch (step)
        {
            case 1: {
                    if (leftDoor.transform.position.x > leftDoorPos.x - targetOpenAmount)
                    {
                        leftDoor.transform.Translate(-1, 0, 0);
                        rightDoor.transform.Translate(1, 0, 0);
                    }
                    else if (leftDoor.transform.position.x < leftDoorPos.x - targetOpenAmount)
                    {
                        leftDoor.transform.position = new(leftDoorPos.x - targetOpenAmount, leftDoor.transform.position.y);
                        doorOpeningCurrentTime = doorOpeningTime;
                    }
                }; break;
            case 2: {
                    if (leftDoor.transform.position.x > leftDoorPos.x - targetOpenAmount)
                    {
                        leftDoor.transform.Translate(-1, 0, 0);
                        rightDoor.transform.Translate(1, 0, 0);
                    }
                    else if (leftDoor.transform.position.x < leftDoorPos.x - targetOpenAmount)
                    {
                        leftDoor.transform.position = new(leftDoorPos.x - targetOpenAmount, leftDoor.transform.position.y);
                        rightDoor.transform.position = new(rightDoor.x - targetOpenAmount, leftDoor.transform.position.y);
                        doorOpeningCurrentTime = doorOpeningTime;
                    }
                }; break;
            default: { }; break;
        }

        


        if (doorOpeningCurrentTime >= 0)
            doorOpeningCurrentTime -= Time.deltaTime;

        if(doorOpeningCurrentTime < 0)
        {
            DoorClose();
            doorOpeningCurrentTime = 0;
        }

    }

    public void DoorOpen()
    {
        step = 1;
    }

    public void DoorClose()
    {
        step = 2;
    }
}
