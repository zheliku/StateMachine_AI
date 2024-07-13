using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(ChaseStateData), menuName = "ScriptableObject/" + nameof(ChaseStateData))]
public class ChaseStateData : ScriptableObject
{
    public float chaseDistance        = 7f; // 与目标超出该距离，则停止追逐，回到出生点
    public int   targetPosUpdateFrame = 10;  // 每隔 listenMaxCount 帧更新目标点，用于性能优化

    [NonSerialized]
    public int frameCount = 0; // 当前帧数记录
}