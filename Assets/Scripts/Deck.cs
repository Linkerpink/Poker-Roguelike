using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private GameObject m_playingCardPrefab;

    [SerializeField] private List<PlayingCardSO> m_playingCardSOList;
    private List<PlayingCardSO> m_handList = new List<PlayingCardSO>();
    private List<PlayingCardSO> m_remainingDeckCards = new List<PlayingCardSO>();

    private void Start()
    {
        SetCards();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddCardToHand();
        }
    }

    private PlayingCardSO ChooseRandomCard()
    {
        PlayingCardSO _card;
        
        _card = m_remainingDeckCards[Random.Range(0, m_remainingDeckCards.Count)];

        m_handList.Add(_card);
        m_remainingDeckCards.Remove(_card);

        return _card;
    }

    private void AddCardToHand()
    {
        RectTransform _handRectTransform =  GameObject.Find("Hand").GetComponent<RectTransform>();
        GameObject _playingCard = Instantiate(m_playingCardPrefab, _handRectTransform);
        _playingCard.GetComponent<PlayingCard>().SetCardValues(ChooseRandomCard());
    }

    private void SetCards()
    {
        m_remainingDeckCards = m_playingCardSOList;
    }
}