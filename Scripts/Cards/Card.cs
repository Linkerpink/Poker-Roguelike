using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
    }

    private void OnMouseEnter()
    {
        m_animator.SetTrigger("Card Shake");
        Debug.Log("ja");
    }
}
