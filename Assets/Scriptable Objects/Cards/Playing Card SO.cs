using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayingCardSO : ScriptableObject
{
    public string cardText;
    public int cardValue;

    [HideInInspector] public enum Suits
    {
        Heart,
        Club,
        Spade,
        Diamond,
    }
    public Suits suit;

    public Sprite cardIcon;
    public GameObject cardDesign;
    public Color cardColor;
}