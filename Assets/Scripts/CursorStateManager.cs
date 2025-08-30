using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CursorStateManager : MonoBehaviour {
    [field: SerializeField] private Texture2D cursor1;
    [field: SerializeField] private Texture2D cursor2;
    private Vector2 pointerStart = new Vector2( 0, 0 );
    public void ChooseCursor1() {
        Cursor.SetCursor( cursor1, pointerStart, CursorMode.Auto );
    }
    public void ChooseCursor2() {
        Cursor.SetCursor( cursor2, pointerStart, CursorMode.Auto );
    }
}
