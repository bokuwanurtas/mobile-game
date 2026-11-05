using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaPr : MonoBehaviour
{
    RectTransform panel;
    Rect lastSafe;
    Vector2Int lastScreen;

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        Apply();
    }

    void Start() { Apply(); }

    void Update()
    {
        if (Screen.safeArea != lastSafe ||
            Screen.width != lastScreen.x ||
            Screen.height != lastScreen.y)
        {
            Apply();
        }
    }

    void Apply()
    {
        if (Screen.width <= 0 || Screen.height <= 0) return;

        Rect safe = Screen.safeArea;
        if (safe.width <= 0 || safe.height <= 0)
            safe = new Rect(0, 0, Screen.width, Screen.height);

        lastSafe = Screen.safeArea;
        lastScreen = new Vector2Int(Screen.width, Screen.height);

        Vector2 min = safe.position;
        Vector2 max = safe.position + safe.size;
        min.x /= Screen.width;  min.y /= Screen.height;
        max.x /= Screen.width;  max.y /= Screen.height;

        panel.anchorMin = min;
        panel.anchorMax = max;
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;
    }
}