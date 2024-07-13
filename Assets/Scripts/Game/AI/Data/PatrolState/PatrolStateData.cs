using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 巡逻数据类
/// </summary>
public class PatrolStateData : ScriptableObject
{
    public EPatrolType patrolType;     // 巡逻类型
    public Vector3     targetPos;      // 目标位置
    public Vector3     lastTargetPos;  // 上一次目标位置
    public float       patrolDistance; // 与目标小于该距离时，将脱离巡逻状态，开始追逐
}

/// <summary>
/// 巡逻移动数据类
/// </summary>
public abstract class PatrolStateMoveData : PatrolStateData
{
    public bool  isWaiting; // 是否正在等待
    public float waitTime;  // 巡逻等待时间

    [NonSerialized]
    public float curTime; // 计时时间

    private void OnEnable() {
        curTime = waitTime; // 初始化计时时间
    }
}