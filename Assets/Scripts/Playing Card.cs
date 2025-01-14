using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.U2D;

public class PlayingCard : Card
{
    public PlayingCardSO playingCardSO;

    [SerializeField] private List<TextMeshProUGUI> m_cardTexts;
    [SerializeField] private List<Image> m_cardIcons;

    [SerializeField] private GameObject m_cardDesignParent;

    public void SetCardValues(PlayingCardSO _playingCardSO)
    {
        playingCardSO = _playingCardSO;

        foreach (TextMeshProUGUI _text in m_cardTexts)
        {
            _text.SetText(playingCardSO.cardText);
            _text.color = playingCardSO.cardColor;
        }

        foreach (Image _icon in m_cardIcons)
        {
            _icon.sprite = playingCardSO.cardIcon;
            _icon.color = playingCardSO.cardColor;
        }

         GameObject _cardDesignParent = Instantiate(playingCardSO.cardDesign, m_cardDesignParent.transform);

         
    }
}