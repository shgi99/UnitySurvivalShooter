using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    public string moveXAxisName = "Horizontal"; 
    public string moveYAxisName = "Vertical"; 
    public string fireButtonName = "Fire1"; 

    public Vector2 Move { get; private set; } 
    public bool Fire { get; private set; } 

    // �������� ����� �Է��� ����
    private void Update()
    {
        float moveX = Input.GetAxis(moveXAxisName);
        float moveY = Input.GetAxis(moveYAxisName); 

        Move = new Vector2(moveX, moveY); 
        Fire = Input.GetButton(fireButtonName);
    }
}