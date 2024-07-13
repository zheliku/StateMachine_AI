using System.Collections;
using System.Collections.Generic;
using DrawXXL;
using UnityEngine;

public class AttackState : BaseState
{
    private AttackStateData _data;

    public override EAIState AIState { get => EAIState.Attack; }

    public AttackState(StateMachine stateMachine) : base(stateMachine) {
        var data = Resources.Load<AttackStateData>("StateData/Attack/AttackStateData");
        _data = Object.Instantiate(data);
    }

    public override void OnStateEnter() {
        // 刚进入状态时，认为此时需要攻击
        _data.nextAttackTime = Time.time;
    }

    public override void OnStateUpdate() {
        var aiObject = _stateMachine.AIObject;

        // 看向目标并且达到计时，才进行攻击
        if (aiObject.LookAt(aiObject.TargetTransform.position) &&
            Time.time >= _data.nextAttackTime) {
            aiObject.Attack();                                           // 执行攻击
            _data.nextAttackTime = Time.time + _data.attackIntervalTime; // 更新下次攻击的时间
        }

        // Attack ---> Chase
        if (DistanceOfXZ(aiObject.Transform.position, aiObject.TargetTransform.position) > aiObject.AttackRange) {
            _stateMachine.ChangeState(EAIState.Chase);
        }
    }

    public override void OnStateExit() { }
    
    public override void DrawGizmos() {
        var aiObject = _stateMachine.AIObject;
        
        // 绘制攻击范围
        DrawShapes.Circle(aiObject.Transform.position, aiObject.AttackRange, Color.red,
                          Vector3.up, lineWidth: 0.05f);
    }
}