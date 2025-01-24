using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private GameObject m_playingCardPrefab;
    [SerializeField] private List<PlayingCardSO> m_playingCardSOList;

    public List<PlayingCardSO> handList = new();
    private List<PlayingCardSO> m_remainingDeckCards = new();
    public List<PlayingCardSO> selectedCards = new();

    [SerializeField] private TextMeshProUGUI m_deckText;
    [SerializeField] private float cardDrawTime = 0.25f;

    [SerializeField] private Transform[] m_cardPositions;
    private bool[] m_positionTaken;

    private GameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartRound();
    }

    private void StartRound()
    {
        InitializeCardPositions();
        SetCards();
        StartCoroutine(DrawCardsToHand(8));

        m_gameManager.round += 1;
        m_gameManager.roundScore = 0;
        m_gameManager.roundScoreQuota *= 1.25f;
    }

    private void InitializeCardPositions()
    {
        m_positionTaken = new bool[m_cardPositions.Length];
    }

    private PlayingCardSO ChooseRandomCard()
    {
        if (m_remainingDeckCards.Count > 0)
        {
            PlayingCardSO _card = m_remainingDeckCards[Random.Range(0, m_remainingDeckCards.Count)];
            handList.Add(_card);
            m_remainingDeckCards.Remove(_card);

            m_deckText.SetText(m_remainingDeckCards.Count.ToString() + " / " + m_playingCardSOList.Count.ToString());

            return _card;
        }
        return null;
    }

    private void AddCardToHand()
    {
        int positionIndex = FindAvailablePosition();
        if (positionIndex == -1)
        {
            Debug.LogError("No available positions for new card!");
            return;
        }

        PlayingCardSO _cardSO = ChooseRandomCard();
        if (_cardSO == null) return;

        GameObject _playingCard = Instantiate(m_playingCardPrefab, m_cardPositions[positionIndex].position, Quaternion.identity);
        _playingCard.GetComponent<PlayingCard>().SetCardValues(_cardSO, positionIndex);

        m_positionTaken[positionIndex] = true;
    }

    private int FindAvailablePosition()
    {
        for (int i = 0; i < m_positionTaken.Length; i++)
        {
            if (!m_positionTaken[i]) return i;
        }
        return -1;
    }

    private void SetCards()
    {
        m_remainingDeckCards = new List<PlayingCardSO>(m_playingCardSOList);
        m_deckText.SetText(m_remainingDeckCards.Count.ToString() + " / " + m_playingCardSOList.Count.ToString());
    }

    public void SelectCard(PlayingCardSO _card)
    {
        selectedCards.Add(_card);
    }

    public void DeselectCard(PlayingCardSO _card)
    {
        selectedCards.Remove(_card);
    }

    private IEnumerator DrawCardsToHand(int _cardAmount)
    {
        for (int i = 0; i < _cardAmount; i++)
        {
            AddCardToHand();
            yield return new WaitForSeconds(cardDrawTime);
        }
    }

    public void RemoveCardFromPosition(int positionIndex)
    {
        if (positionIndex >= 0 && positionIndex < m_positionTaken.Length)
        {
            m_positionTaken[positionIndex] = false;
        }
    }

    public void StartHandCheck()
    {
        if (m_gameManager.gameState == GameManager.GameStates.Game)
        {
            StartCoroutine(HandCheck());
        }
    }

    public IEnumerator HandCheck()
    {
        foreach (PlayingCardSO _card in selectedCards)
        {
            m_gameManager.roundScore += _card.cardValue;
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(1);
        m_gameManager.gameState = GameManager.GameStates.Shop;
    }
}