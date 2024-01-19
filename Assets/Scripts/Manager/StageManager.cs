using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : SingletonMonoBase<StageManager>
{
    public bool[] isClear_Stage = new bool[3];
    public Vector3[] stagePos =
    {
        new Vector3(-19.75f, 0, 88.6f),
        new Vector3(-46.1f, 1.21f, 17.9f),
        new Vector3(2.02f, 0, -6.6f),
       // new Vector3(-29, 0, 41),
    };

    public int clearStageMax = 0;
    public Transform stageCol;

    public Vector3 GetStartPos()
    {
        Debug.Log(stagePos[clearStageMax]);
        return stagePos[clearStageMax];
    }

    public void SetNextStagePos()
    {
        if (clearStageMax + 1 < stagePos.Length)
        {
            stageCol.position = stagePos[clearStageMax + 1];
            Debug.Log(stageCol.position);
        }
        else
        {
            Debug.LogWarning("Max Stage");
        }
    }
}
