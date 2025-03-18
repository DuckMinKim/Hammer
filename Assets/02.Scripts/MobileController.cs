using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Player player; // 플레이어 스크립트

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
