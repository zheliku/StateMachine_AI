using UnityEngine;

/// <summary>
/// AI 对象接口，用于规范 AI 对象的行为
/// </summary>
public interface IAIObject
{
    public Transform Transform { get; } // AI 对象的 Transform

    public Transform TargetTransform { get; } // 目标监测对象的 Transform

    public float AttackRange { get; } // 攻击范围
    
    public Vector3 BornPos { get; } // 出生位置

    public void Move(Vector3 targetPos); // 移动

    public void StopMove(); // 停止移动
    
    public bool LookAt(Vector3 targetPos); // 看向目标位置

    public void Attack(); // 攻击

    public void ChangeAction(EAction action); // 切换指定动作
}