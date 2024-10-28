using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform background; // ҡ�˱���
    private RectTransform handle; // ҡ���ֱ�

    private Vector2 inputDirection; // ���뷽��

    private void Awake()
    {
        background = GetComponent<RectTransform>();
        handle = transform.GetChild(0).GetComponent<RectTransform>(); // ҡ���ֱ��Ǳ�������Ԫ��
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            // ��ȡ����λ�������ҡ�˱����İٷֱ�
            touchPosition.x = (touchPosition.x / background.sizeDelta.x) * 2;
            touchPosition.y = (touchPosition.y / background.sizeDelta.y) * 2;
            touchPosition = (touchPosition.magnitude > 1f) ? touchPosition.normalized : touchPosition;

            // ����ҡ���ֱ���λ��
            handle.anchoredPosition = new Vector2(touchPosition.x * (background.sizeDelta.x / 2), touchPosition.y * (background.sizeDelta.y / 2));
            // �������뷽��
            inputDirection = touchPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ����ҡ��λ�ú����뷽��
        handle.anchoredPosition = Vector2.zero;
        inputDirection = Vector2.zero;
    }

    // �������뷽��
    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }
}
