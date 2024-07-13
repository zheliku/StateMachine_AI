using System;
using System.Collections;
using System.Collections.Generic;
using DrawXXL;
using UnityEngine;
using Object = UnityEngine.Object;

public class ChaseState : BaseState
{
    private ChaseStateData _data;

    public override EAIState AIState { get => EAIState.Chase; }

    public ChaseState(StateMachine stateMachine) : base(stateMachine) {
        var data = Resources.Load<ChaseStateData>("StateData/Chase/ChaseStateData");
        _data = Object.Instantiate(data);
    }

    public override void OnStateEnter() { }

    public override void OnStateUpdate() {
        var aiObject = _stateMachine.AIObject;

        // 每隔 _listenMaxCount 帧刷新目标对象位置，进行追逐
        if (_data.frameCount++ % _data.targetPosUpdateFrame == 0) {
            aiObject.Move(aiObject.TargetTransform.position);
            _data.frameCount %= _data.targetPosUpdateFrame;
        }

        // Chase ---> Attack
        if (DistanceOfXZ(aiObject.Transform.position, aiObject.TargetTransform.position) < aiObject.AttackRange) {
            aiObject.StopMove();
            _stateMachine.ChangeState(EAIState.Attack);
        }
        
        // Chase ---> Back
        if (DistanceOfXZ(aiObject.Transform.position, aiObject.TargetTransform.position) > _data.chaseDistance) {
            _stateMachine.ChangeState(EAIState.Back);
        }
    }

    public override void OnStateExit() { }
    
    public override void DrawGizmos() {
        var aiObject = _stateMachine.AIObject;
        
        // 绘制攻击范围
        DrawShapes.Circle(aiObject.Transform.position, aiObject.AttackRange, Color.red,
                          Vector3.up, lineWidth: 0.05f);

        // 绘制脱离范围
        DrawShapes.Circle(aiObject.Transform.position, _data.chaseDistance, new Color(1, 0.5f, 0),
                          Vector3.up, lineWidth: 0.05f, outlineStyle: DrawBasics.LineStyle.dotted);
    }
}