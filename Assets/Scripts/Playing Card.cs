using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayingCard : Card
{
    public PlayingCardSO playingCardSO;

    private List<TextMeshProUGUI> m_cardTexts = new List<TextMeshProUGUI>();

    private void Awake()
    {


        foreach (RectTransform _child in )//fix dit jij skitzo
        foreach (TextMeshProUGUI _text in transform)
        {
            m_cardTexts.Add(_text);
        }
    }

    public void SetCardValues(PlayingCardSO _playingCardSO)
    {
        playingCardSO = _playingCardSO;

        foreach(TextMeshProUGUI _text in m_cardTexts)
        {
            _text.SetText(playingCardSO.cardText);
        }
    }
}