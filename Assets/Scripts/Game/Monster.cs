using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IAIObject
{
    public Transform  targetTransform; // 目标位置
    public GameObject bullet;          // 子弹
    public float      attackRange = 3; // 攻击范围

    private Quaternion _startRotation;  // 记录开始攻击时的角度
    private Quaternion _targetRotation; // 记录目标角度
    private float      _rotateTime;     // 旋转计时

    private NavMeshAgent _navMeshAgent; // 导航代理

    private StateMachine _aiMachine; // AI 状态机
    
    private void Start() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        BornPos       = transform.position;

        _aiMachine = new StateMachine();
        _aiMachine.Init(this);

        // 为 AI 添加巡逻状态
        _aiMachine.AddState(EAIState.Patrol);
        _aiMachine.AddState(EAIState.Chase);
        _aiMachine.AddState(EAIState.Attack);
        _aiMachine.AddState(EAIState.Back);
        
        // 更改初始状态
        _aiMachine.ChangeState(EAIState.Patrol);
    }

    private void Update() {
        _aiMachine.UpdateState();
    }

    #region IAIObject 接口实现
    
    public Transform Transform       { get => transform; }
    public Transform TargetTransform { get => targetTransform; }
    public float     AttackRange     { get => attackRange; }

    public Vector3 BornPos {
        get;
        protected set;
    }

    public void Move(Vector3 targetPos) {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(targetPos);
    }

    public void StopMove() {
        _navMeshAgent.isStopped = true;
    }

    public bool LookAt(Vector3 targetPos) {
        // 实现匀速看向目标
        var newTargetRotation = Quaternion.LookRotation(targetTransform.position - transform.position, Vector3.up);
        if (_targetRotation != newTargetRotation) {
            _targetRotation = newTargetRotation;
            _rotateTime     = 0;
            _startRotation  = transform.rotation;
        }
        _rotateTime        += Time.deltaTime * _navMeshAgent.angularSpeed / 60;
        transform.rotation =  Quaternion.Slerp(_startRotation, _targetRotation, _rotateTime);
        return Quaternion.Angle(transform.rotation, _targetRotation) < 0.1f; // 角度 < 0.1 认为已经看向了目标
    }

    public void Attack() {
        Debug.Log("Attack");
        var bullet = Instantiate(this.bullet,
                                 transform.position + transform.forward * 0.5f,
                                 transform.rotation);
    }

    public void ChangeAction(EAction action) {
        Debug.Log("ChangeAction: " + action);
    }
    
    #endregion
}