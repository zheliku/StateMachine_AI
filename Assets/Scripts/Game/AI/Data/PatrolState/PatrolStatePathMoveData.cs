using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 路径巡逻数据类
/// </summary>
[CreateAssetMenu(fileName = nameof(PatrolStatePathMoveData), menuName = "ScriptableObject/" + nameof(PatrolStatePathMoveData))]
public class PatrolStatePathMoveData : PatrolStateMoveData
{
    public List<Vector3>       pointList  = new List<Vector3>(); // 路径点
    public int                 pointIndex = 0;                   // 当前点索引
    public EPatrolPathMoveType moveType;                         // 路径移动类型
    public bool                isReverse;                        // 是否正在反向移动

    private void Awake() {
        patrolType = EPatrolType.PathMove;
    }
}