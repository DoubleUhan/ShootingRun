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
    public ArithmeticType type;
    public int value;
    public TMP_Text value_T;

    void Start()
    {
        // value = Ra
    }

}
