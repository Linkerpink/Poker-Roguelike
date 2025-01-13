using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform m_RectTransform;

    private bool m_mouseOver = false;
    private bool m_dragging = false;
    private Vector3 m_initialPosition;

    [SerializeField] private float smoothTime = 0.5f;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        m_initialPosition = m_RectTransform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && m_mouseOver)
        {
            m_dragging = true;
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            m_dragging = false;
        }
    }

    private void FixedUpdate()
    {
        if (m_dragging)
        {
            Vector3 _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_initialPosition.z));

            m_RectTransform.position = Vector3.Slerp(m_RectTransform.position, new Vector3(_mousePos.x, _mousePos.y, m_initialPosition.z), smoothTime);
        }
        else
        {
            m_RectTransform.position = Vector3.Slerp(m_RectTransform.position, m_initialPosition, smoothTime);
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