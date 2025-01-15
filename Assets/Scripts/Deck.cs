using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private GameObject m_playingCardPrefab;

    [SerializeField] private List<PlayingCardSO> m_playingCardSOList;
    private List<PlayingCardSO> m_handList = new List<PlayingCardSO>();
    private List<PlayingCardSO> m_remainingDeckCards = new List<PlayingCardSO>();

    [SerializeField] private TextMeshProUGUI m_deckText;


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
        if (m_remainingDeckCards.Count > 0)
        {
            PlayingCardSO _card;
        
            _card = m_remainingDeckCards[Random.Range(0, m_remainingDeckCards.Count)];

            m_handList.Add(_card);
            m_remainingDeckCards.Remove(_card);

            m_deckText.SetText(m_remainingDeckCards.Count.ToString() + " / " + m_playingCardSOList.Count.ToString());

            return _card;
        }
        else
        {
            return null;
        }
        
    }

    private void AddCardToHand()
    {
        RectTransform _handRectTransform =  GameObject.Find("Hand").GetComponent<RectTransform>();
        GameObject _playingCard = Instantiate(m_playingCardPrefab, _handRectTransform);
        _playingCard.GetComponent<PlayingCard>().SetCardValues(ChooseRandomCard());
    }

   private void SetCards()
    {
        m_remainingDeckCards = new List<PlayingCardSO>(m_playingCardSOList);
        m_deckText.SetText(m_remainingDeckCards.Count.ToString() + " / " + m_playingCardSOList.Count.ToString());
    }
}