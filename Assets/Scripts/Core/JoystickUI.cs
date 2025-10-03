using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI")]
    public RectTransform background;
    public RectTransform handle;

    [Header("Tuning")]
    public float handleRange = 100f;
    public float deadzone = 0.1f;

    public Vector2 Value { get; private set; } // normalized [-1,1]

    int activePointerId = -1;

    void OnEnable() { ResetStick(); }
    void OnDisable() { ResetStick(); }

    public void OnPointerDown(PointerEventData e)
    {
        if (activePointerId == -1) activePointerId = e.pointerId;
        OnDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        if (e.pointerId != activePointerId || !background) return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, e.position, e.pressEventCamera, out var local))
        {
            Vector2 delta = local - background.rect.center;
            Vector2 clamped = Vector2.ClampMagnitude(delta, handleRange);

            // Normalize to [-1..1]
            Vector2 v = clamped / handleRange;
            float m = v.magnitude;
            if (m < deadzone) v = Vector2.zero;
            Value = v;

            if (handle) handle.anchoredPosition = clamped;
        }
    }

    public void OnPointerUp(PointerEventData e)
    {
        if (e.pointerId != activePointerId) return;
        activePointerId = -1;
        ResetStick();
    }

    void ResetStick()
    {
        Value = Vector2.zero;
        if (handle) handle.anchoredPosition = Vector2.zero;
    }
}
