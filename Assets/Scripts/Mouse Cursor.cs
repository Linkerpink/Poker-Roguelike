using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private void Update()
    {
        Cursor.visible = true;
        Vector3 _point;

        Camera _cam = Camera.main;

        Vector3 _mousePos = Input.mousePosition;

        _point = _cam.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, _cam.nearClipPlane));

        transform.position = _point;
    }
}