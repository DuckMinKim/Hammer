using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CheckingJoypad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem.onAnyButtonPress
    .CallOnce(ctrl => Debug.Log($"Button {ctrl} pressed!"));

    }
}
