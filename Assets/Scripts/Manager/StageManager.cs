using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : SingletonMonoBase<StageManager>
{
    public bool[] isClear_Stage = new bool[3];
    public Vector3[] stagePos =
    {
        new Vector3(-19.75f, 1.16709f, 88.6f),
        new Vector3(-31, 0, 72),
        new Vector3(-29, 0, 41),
    };
   
    public int clearStageMax = 0;
    public Transform stageCol;

    public Vector3 GetStartPos()
    {
        return stagePos[clearStageMax];
    }

    public void SetNextStagePos()
    {
        if (clearStageMax + 1 < stagePos.Length)
        {
            stageCol.position = stagePos[clearStageMax + 1];
        }
        else
        {
            Debug.LogWarning("Max Stage");
        }
    }
}
