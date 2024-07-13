using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有限状态机类，用于管理各个状态之间的切换
/// </summary>
public class StateMachine
{
    public IAIObject AIObject; // 管理的 AI 对象

    /// <summary>
    /// 管理所有状态的字典容器
    /// </summary>
    private Dictionary<EAIState, BaseState> _stateDic = new Dictionary<EAIState, BaseState>();

    private BaseState _nowState; // 当前状态
    
    /// <summary>
    /// 初始化方法
    /// </summary>
    /// <param name="aiObject">待管理的 AI 对象</param>
    public void Init(IAIObject aiObject) {
        AIObject = aiObject;
    }

    /// <summary>
    /// 添加 AI 状态
    /// </summary>
    public void AddState(EAIState state) {
        switch (state) {
            case EAIState.Patrol:
                _stateDic.Add(state, new PatrolState(this));
                break;
            case EAIState.Back:
                _stateDic.Add(state, new BackState(this));
                break;
            case EAIState.Chase:
                _stateDic.Add(state, new ChaseState(this));
                break;
            case EAIState.Attack:
                _stateDic.Add(state, new AttackState(this));
                break;
            default: throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(EAIState state) {
        _nowState?.OnStateExit(); // 退出状态

        if (_stateDic.TryGetValue(state, out BaseState nowState)) { // 进入状态
            _nowState = nowState;
            _nowState.OnStateEnter();
        }
    }

    /// <summary>
    /// 更新当前状态
    /// </summary>
    public void UpdateState() {
        _nowState?.OnStateUpdate();

        _nowState?.DrawGizmos();
    }
}