using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPopTextController : MonoBehaviour {

    private static FloatingPopText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
            popupText = Resources.Load<FloatingPopText>("Prefabs/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Vector3 point)
    {
        FloatingPopText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector3(point.x + Random.Range(-.2f, .2f), point.y + Random.Range(-.2f, .2f), point.z));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
