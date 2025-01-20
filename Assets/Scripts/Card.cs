using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager m_gameManager;

    private bool m_mouseOver = false;
    private bool m_dragging = false;
    private Vector3 m_initialPosition;

    private float smoothTime = 0.05f;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();

        m_initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && m_mouseOver && !m_gameManager.isDragging)
        {
            m_dragging = true;
            m_gameManager.ChangeDragging(true);
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            m_dragging = false;
            m_gameManager.ChangeDragging(false);
        }

        if (m_dragging)
        {
            Vector3 _mousePos;
            Camera _cam = Camera.main;

            _mousePos.x = Input.mousePosition.x;
            _mousePos.y = Input.mousePosition.y;

            Vector3 _point;

            _point = _cam.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, _cam.nearClipPlane));

            transform.position = Vector3.Lerp(transform.position, _point, smoothTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, m_initialPosition, smoothTime);
        }
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        m_mouseOver = true;
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        m_mouseOver = false;
    }
}