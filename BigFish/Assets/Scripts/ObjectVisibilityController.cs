using UnityEngine;
public class ObjectVisibilityController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
 //   private Collider2D objectCollider;
    private const float visibilityPadding = 0.8f; // ����� � 10%
    private Canvas objectCanvas;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main; // �������� ������� ������
 //       objectCollider = GetComponent<Collider2D>(); // ��������� �������� ���������, ���� �� ����
        objectCanvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        CheckVisibility();
    }

    private void CheckVisibility()
    {
        bool isVisible = IsObjectVisible();

        // �������� ��� ��������� SpriteRenderer
        spriteRenderer.enabled = isVisible;

        // �������� ��� ��������� Canvas, ���� �� ����
        if (objectCanvas != null)
        {
            objectCanvas.enabled = isVisible;
        }
    }

    private bool IsObjectVisible()
    {
        Bounds objectBounds;

        // ���������� ���������, ���� �� ����, ����� ���������� ������� �������
        /*
        if (objectCollider != null)
        {
            objectBounds = objectCollider.bounds;
        }
        else
        {*/
            objectBounds = spriteRenderer.bounds;
      //  }
        
        // �������� ��� ������ ���� ������� � ������ ������
        Vector3 minBounds = objectBounds.min - new Vector3(objectBounds.size.x * visibilityPadding, objectBounds.size.y * visibilityPadding, 0);
        Vector3 maxBounds = objectBounds.max + new Vector3(objectBounds.size.x * visibilityPadding, objectBounds.size.y * visibilityPadding, 0);

        // ��������� ������� ���������� ������ ������� � �������� ���������� ������
        Vector3 screenPointMin = mainCamera.WorldToViewportPoint(minBounds);
        Vector3 screenPointMax = mainCamera.WorldToViewportPoint(maxBounds);

        // ���������, �������� �� ���� �� ����� ������� � ������
        bool isVisible = (screenPointMin.x < 1 && screenPointMax.x > 0) &&
                         (screenPointMin.y < 1 && screenPointMax.y > 0);

        // ��� ��� �������
        Debug.Log("Object is visible: " + isVisible + " | Bounds min: " + screenPointMin + " | Bounds max: " + screenPointMax);

        return isVisible;
    }
}