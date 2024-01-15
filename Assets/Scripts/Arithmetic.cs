using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public enum ArithmeticType
{
    add,
    sub,
    mult,
    div
}
public class Arithmetic : MonoBehaviour
{
    [HideInInspector] public Arithmetic Pair;
    [HideInInspector] public bool IsEatable = true;

    public ArithmeticType type;
    public int value;
    public TMP_Text value_T;
    public TMP_Text sigh_T;
}
