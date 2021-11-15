using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    private Vector2 _screenSize;

    private void Awake()
    {
        var rectTransform = GetComponent<RectTransform>();
        var safeArea = Screen.safeArea;

        _screenSize = new Vector2(Screen.width, Screen.height);
        var anchorMin = GetUIPointFromScreen(safeArea.position);
        var anchorMax = GetUIPointFromScreen(safeArea.position + safeArea.size);

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

    private Vector2 GetUIPointFromScreen(Vector2 screenPoint)
    {
        return screenPoint /= _screenSize;
    }
}