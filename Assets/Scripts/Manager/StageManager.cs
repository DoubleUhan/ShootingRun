using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : SingletonMonoBase<StageManager>
{
    [HideInInspector] public bool isClear_Stage1;
    [HideInInspector] public bool isClear_Stage2;
    public GameObject stage1_col;
    public GameObject stage2_col;
}
