using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ja : MonoBehaviour
{
    private GameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();       
    }

    public void DoTheThingie(string _ja)
    {
        m_gameManager.ChangeGameState(_ja);
    }
}
