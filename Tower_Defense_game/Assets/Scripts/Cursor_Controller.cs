using UnityEngine;

public class Cursor_Controller : MonoBehaviour
{
    public Texture2D cursor;
    public Vector2 hotspot = Vector2.zero; 
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        if (cursor != null)
        {
            Cursor.SetCursor(cursor, hotspot, cursorMode);
        }
        else
        {
            Debug.LogError("Cursor texture not assigned in the Inspector!");
        }
    }
}
