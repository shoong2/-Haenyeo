using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : ScriptableObject
{
    public RewardType rewardType;

    [SerializeField]
    Sprite icon;
    [SerializeField]
    string description;
    [SerializeField]
    int quantity;
    public Item[] item;


    public Sprite Icon => icon;
    public string Description => description;
    public int Quantitiy => quantity;

    public abstract void Give(Quest quest);

    public enum RewardType
    {
        XP,
        Item
    }
}
