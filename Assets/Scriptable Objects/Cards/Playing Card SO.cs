using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu]
public class PlayingCardSO : ScriptableObject
{
    public string cardText;
    public int cardValue;
    public Sprite cardSprite;
    public Color cardColor;
}