using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private GameObject m_playingCardPrefab;

    [SerializeField] private List<PlayingCardSO> m_playingCardSOList;
    public List<PlayingCardSO> handList = new();
    private List<PlayingCardSO> m_remainingDeckCards = new();

    private List<PlayingCardSO> m_selectedCards = new();

    [SerializeField] private TextMeshProUGUI m_deckText;

    [SerializeField] private float cardDrawTime = 0.25f;

    private void Start()
    {
        SetCards();
        StartCoroutine(DrawCardsToHand(8));
    }

    private PlayingCardSO ChooseRandomCard()
    {
        if (m_remainingDeckCards.Count > 0)
        {
            PlayingCardSO _card;
        
            _card = m_remainingDeckCards[Random.Range(0, m_remainingDeckCards.Count)];

            handList.Add(_card);
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
        GameObject _playingCard = Instantiate(m_playingCardPrefab);
        _playingCard.GetComponent<PlayingCard>().SetCardValues(ChooseRandomCard());
    }

    private void SetCards()
    {
        m_remainingDeckCards = new List<PlayingCardSO>(m_playingCardSOList);
        m_deckText.SetText(m_remainingDeckCards.Count.ToString() + " / " + m_playingCardSOList.Count.ToString());
    }

    public void SelectCard(PlayingCardSO _card)
    {
        m_selectedCards.Add(_card);
    }

    public void DeselectCard(PlayingCardSO _card)
    {
        m_selectedCards.Remove(_card);
    }

    private IEnumerator DrawCardsToHand(int _cardAmount)
    {
        for (int i = 0; i < _cardAmount; i++)
        {
            AddCardToHand();
            yield return new WaitForSeconds(cardDrawTime);
        }
        
    }
}