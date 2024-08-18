using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform background;
    public RectTransform handle;
    private Vector2 joystickPosition;
    private bool joystickVisible = false;
    public float vertical;
    public float horizontal;

    public void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        joystickVisible = true;
        background.position = eventData.position;
        handle.position = eventData.position;
        joystickPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (joystickVisible)
        {
            Vector2 direction = eventData.position - joystickPosition;
            handle.anchoredPosition = Vector2.ClampMagnitude(direction, background.sizeDelta.x / 2);
            direction = direction.normalized;
            vertical = direction.y;
            horizontal = direction.x;
            InputManager.instance.verticalInput = vertical;
            InputManager.instance.horizontalInput = horizontal;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        vertical = 0;
        horizontal = 0;
        background.gameObject.SetActive(false);
        joystickVisible = false;
        handle.anchoredPosition = Vector2.zero;
    }
}