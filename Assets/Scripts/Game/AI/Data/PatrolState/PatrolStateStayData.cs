using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 原地等待数据类
/// </summary>
[CreateAssetMenu(fileName = nameof(PatrolStateStayData), menuName = "ScriptableObject/" + nameof(PatrolStateStayData))]
public class PatrolStateStayData : PatrolStateData
{
    public EAction actionType; // 巡逻动作类型

    private void Awake() {
        patrolType = EPatrolType.Stay;
    }
}