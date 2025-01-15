using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//FIX GAME NOT CANVAS!!!
public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager m_gameManager;

    private RectTransform m_rectTransform;

    private bool m_mouseOver = false;
    private bool m_dragging = false;
    private Vector3 m_initialPosition;

    private float smoothTime = 0.5f;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_rectTransform = GetComponent<RectTransform>();

        m_initialPosition = m_rectTransform.localPosition;
        Debug.Log(m_initialPosition);
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
            Vector3 _mousePos = Input.mousePosition;
            _mousePos.z = 100;
            
            Debug.Log(new Vector3(_mousePos.x, _mousePos.y, m_rectTransform.position.z));
            m_rectTransform.position = Vector3.Lerp(m_rectTransform.position, new Vector3(_mousePos.x, _mousePos.y, m_rectTransform.position.z), smoothTime * Time.deltaTime);
        }
        else
        {
            m_rectTransform.localPosition = Vector3.Lerp(m_rectTransform.localPosition, m_initialPosition, smoothTime * Time.deltaTime);
        }

        // Debug.Log(m_initialPosition);

        // Debug.Log(m_initialPosition);
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