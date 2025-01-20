using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        GameObject _cardDesign = Instantiate(playingCardSO.cardDesign, m_cardDesignParent.transform);

        foreach (Transform _child in _cardDesign.transform)
        {
            if (_child.gameObject.name == "Card Design Canvas")
            {
                Transform _cardDesignCanvas = _child.GetComponent<Transform>();
                
                foreach (Transform _c in _cardDesignCanvas.transform)
                {
                    Image _icon = _c.gameObject.GetComponent<Image>();

                    _icon.sprite = playingCardSO.cardIcon;
                    _icon.color = playingCardSO.cardColor;
                }
            }
        }
    }
}