using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Player player; // �÷��̾� ��ũ��Ʈ

    void Start()
    {

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //player.MovePlayer(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //player.MovePlayer(0);

    }
}
