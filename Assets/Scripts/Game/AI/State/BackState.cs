using System.Collections;
using System.Collections.Generic;
using DrawXXL;
using UnityEngine;

public class BackState : BaseState
{
    private BackStateData _data;

    public override EAIState AIState { get => EAIState.Back; }

    public BackState(StateMachine stateMachine) : base(stateMachine) {
        var data = Resources.Load<BackStateData>("StateData/Back/BackStateData");
        _data = Object.Instantiate(data);
    }

    public override void OnStateEnter() {
        var aiObject = _stateMachine.AIObject;
        aiObject.Move(aiObject.BornPos);
    }

    public override void OnStateUpdate() {
        var aiObject = _stateMachine.AIObject;

        // Back ---> Patrol
        if (DistanceOfXZ(aiObject.Transform.position, aiObject.BornPos) < 0.2f) {
            _stateMachine.ChangeState(EAIState.Patrol); // 切换到追逐状态
        }

        // Back ---> Chase
        if (DistanceOfXZ(aiObject.Transform.position, aiObject.TargetTransform.position) < _data.backDistance) {
            _stateMachine.ChangeState(EAIState.Chase); // 切换到追逐状态
        }
    }

    public override void OnStateExit() { }

    public override void DrawGizmos() {
        var aiObject = _stateMachine.AIObject;

        // 绘制脱离范围
        DrawShapes.Circle(aiObject.Transform.position, _data.backDistance, Color.green,
                          Vector3.up, lineWidth: 0.05f, outlineStyle: DrawBasics.LineStyle.dotted);
        
        DrawBasics.Point(aiObject.BornPos, markingCrossLinesWidth: 0.1f);
        
        DrawBasics.MovingArrowsLine(aiObject.Transform.position, aiObject.BornPos, Color.blue,
                                    lineWidth: 0.2f, lengthOfArrows: 0.3f,
                                    customAmplitudeAndTextDir: Vector3.forward);
    }
}