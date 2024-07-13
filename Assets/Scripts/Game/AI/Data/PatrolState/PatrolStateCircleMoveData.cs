using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 圆形范围巡逻数据类
/// </summary>
[CreateAssetMenu(fileName = nameof(PatrolStateCircleMoveData), menuName = "ScriptableObject/" + nameof(PatrolStateCircleMoveData))]
public class PatrolStateCircleMoveData : PatrolStateMoveData
{
    public Vector3 centerPos; // 中心点
    public float   radius;    // 半径

    private void Awake() {
        patrolType = EPatrolType.CircleMove;
    }
}