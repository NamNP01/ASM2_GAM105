using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class manaBar : MonoBehaviour
{
    public Image MN;


    public void UpdateMana(int mana, int maxMana)
    {
        MN.fillAmount = (float)mana / (float)maxMana;
    }

    public void UpdateManaBar(int value, int maxValue)
    {
        MN.fillAmount = (float)value / (float)maxValue;
    }
}
