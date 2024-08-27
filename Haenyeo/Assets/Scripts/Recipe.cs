using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public Cooking[] cooking;

    public GameObject slotsParent;
    Slot[] slots;

    private void Awake()
    {
        slots = slotsParent.GetComponentsInChildren<Slot>();
    }


}
