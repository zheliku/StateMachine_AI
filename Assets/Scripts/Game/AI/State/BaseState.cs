using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public abstract class BaseState
{
    public virtual EAIState AIState { get; } // 状态类型

    protected StateMachine _stateMachine; // 附属的状态机

    public BaseState(StateMachine stateMachine) {
        _stateMachine = stateMachine;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public abstract void OnStateEnter();

    /// <summary>
    /// 保持状态
    /// </summary>
    public abstract void OnStateUpdate();

    /// <summary>
    /// 离开状态
    /// </summary>
    public abstract void OnStateExit();

    /// <summary>
    /// 辅助绘制范围，不强制重写
    /// </summary>
    public virtual void DrawGizmos() { }

    /// <summary>
    /// XZ 平面上的距离
    /// </summary>
    protected float DistanceOfXZ(Vector3 pos1, Vector3 pos2) {
        pos1.y = pos2.y = 0;
        return Vector3.Distance(pos1, pos2);
    }
}