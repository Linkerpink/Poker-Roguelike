using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager m_gameManager;

    [SerializeField] private bool m_selectable = true;
    [SerializeField] private bool m_dragable = true;

    private bool m_mouseOver = false;
    private bool m_dragging = false;
    private Vector3 m_initialPosition;
    private Vector3 m_initialMousePos;

    private bool m_selected = false;

    private float m_smoothTime = 0.05f;

    private GameObject m_hand;

    private Deck m_deck;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();

        m_initialPosition = transform.position;

        m_deck = FindObjectOfType<Deck>();

        m_hand = GameObject.Find("Hand");

        if (m_hand != null)
        {
            foreach (Transform _child in m_hand.transform)
            {
                int m_handCount = m_deck.handList.Count;

                switch (m_handCount)
                {
                    case 0:
                    break;

                    case 1:
                    transform.SetParent(m_hand.transform);
                    break;
                    
                }
            }
        }
    }

    private void Update()
    {
        Vector3 _p = transform.position;

        if (m_selected)
        {
            _p.y = Mathf.Lerp(_p.y, m_initialPosition.y + 1, m_smoothTime);
        }
        else
        {
            _p.y = Mathf.Lerp(_p.y, m_initialPosition.y, m_smoothTime);
        }

        transform.position = new Vector3(transform.position.x, _p.y, 0);

        if (Input.GetMouseButtonDown(0) && m_mouseOver && !m_gameManager.isDragging)
        {
            m_initialMousePos = Input.mousePosition; // Mouse position
        }

        if (Input.GetMouseButton(0) && m_mouseOver && !m_gameManager.isDragging && m_dragable)
        {
            if (m_initialMousePos != Input.mousePosition)
            {
                m_dragging = true;
                m_gameManager.ChangeDragging(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_dragging = false;
            m_gameManager.ChangeDragging(false);
            
            if (m_selectable && m_mouseOver)
            {
                if (!m_selected && m_deck.selectedCards.Count < 5)
                {
                    m_selected = true;

                    PlayingCardSO _card;
                    _card = GetComponent<PlayingCard>().playingCardSO;

                    if (_card != null)
                    {
                        m_deck.SelectCard(_card);
                    }
                }
                else
                {
                    m_selected = false;

                    PlayingCardSO _card;
                    _card = GetComponent<PlayingCard>().playingCardSO;

                    if (_card != null)
                    {
                        m_deck.DeselectCard(_card);
                    }
                }
            }
        }

        if (m_dragging)
        {
            Vector3 _mousePos;
            Camera _cam = Camera.main;

            _mousePos.x = Input.mousePosition.x;
            _mousePos.y = Input.mousePosition.y;

            Vector3 _point;

            _point = _cam.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 0));

            transform.position = Vector3.Lerp(transform.position, _point, m_smoothTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, m_initialPosition, m_smoothTime);
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

    public virtual void SelectCard()
    {
        
    }
}