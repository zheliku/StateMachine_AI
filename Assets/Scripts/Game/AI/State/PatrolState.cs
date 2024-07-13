using System;
using DrawXXL;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

/// <summary>
/// 巡逻状态类，处理巡逻逻辑
/// </summary>
public class PatrolState : BaseState
{
    private PatrolStateData _data; // 巡逻数据

    public override EAIState AIState { get => EAIState.Patrol; }

    public PatrolState(StateMachine stateMachine) : base(stateMachine) {
        var data = Resources.Load<PatrolStatePathMoveData>("StateData/Patrol/PatrolStatePathMoveData");
        _data = Object.Instantiate(data);
    }

    public override void OnStateEnter() { }

    public override void OnStateUpdate() {
        var aiObject = _stateMachine.AIObject;
        switch (_data.patrolType) {
            case EPatrolType.Stay:
                OnStayUpdate(aiObject);
                break;
            case EPatrolType.CircleMove:
                OnMoveUpdate(aiObject, EPatrolType.CircleMove);
                break;
            case EPatrolType.PathMove:
                OnMoveUpdate(aiObject, EPatrolType.PathMove);
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        // Patrol ---> Chase
        if (DistanceOfXZ(aiObject.Transform.position, aiObject.TargetTransform.position) < _data.patrolDistance) {
            _stateMachine.ChangeState(EAIState.Chase); // 切换到追逐状态
        }
    }

    public override void OnStateExit() {
        var aiObject = _stateMachine.AIObject;

        switch (_data.patrolType) {
            case EPatrolType.Stay:
                // ...
                break;
            case EPatrolType.CircleMove:
            case EPatrolType.PathMove:
                _stateMachine.AIObject.StopMove(); // 停止移动
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        _data.lastTargetPos = aiObject.BornPos;
    }

    /// <summary>
    /// 原地等待
    /// </summary>
    private void OnStayUpdate(IAIObject aiObject) {
        var data = (PatrolStateStayData)_data;  // 转化数据
        aiObject.ChangeAction(data.actionType); // 更新动作
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void OnMoveUpdate(IAIObject aiObject, EPatrolType moveType) {
        var data = (PatrolStateMoveData)_data; // 转化数据
        if (data.curTime <= 0) {
            if (data.isWaiting) {
                // 依据不同移动类型，更新目标位置
                _data.lastTargetPos = _data.targetPos;
                _data.targetPos = moveType switch {
                    EPatrolType.CircleMove => CalCircleTargetPos((PatrolStateCircleMoveData)data),
                    EPatrolType.PathMove   => CalPathTargetPos((PatrolStatePathMoveData)data),
                    _                      => throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null)
                };
                data.isWaiting = false;
            }
            aiObject.Move(_data.targetPos); // 移动中...

            // 判断是否到达目的地
            if (DistanceOfXZ(_data.targetPos, aiObject.Transform.position) < 0.2f) {
                data.isWaiting = true;          // 继续下一个位置
                data.curTime   = data.waitTime; // 重新计时
                aiObject.StopMove();            // 停止移动
            }
        }
        else {
            data.curTime -= Time.deltaTime; // 计时
        }
    }

    /// <summary>
    /// 更新圆形范围目标位置
    /// </summary>
    private Vector3 CalCircleTargetPos(PatrolStateCircleMoveData data) {
        var radius = Random.Range(data.radius / 2, data.radius);
        return data.centerPos + Quaternion.Euler(0, Random.Range(0f, 359.9f), 0) * Vector3.forward * radius;
    }

    /// <summary>
    /// 更新路径范围目标位置
    /// </summary>
    private Vector3 CalPathTargetPos(PatrolStatePathMoveData data) {
        var targetPos = data.pointList[data.pointIndex];
        // 更新下一个位置
        switch (data.moveType) {
            case EPatrolPathMoveType.Loop: // 顺序取出下一个点
                data.pointIndex = (data.pointIndex + 1) % data.pointList.Count;
                break;
            case EPatrolPathMoveType.GoBack: // 往返取出下一个点
                if (data.isReverse)
                    --data.pointIndex;
                else
                    ++data.pointIndex;
                if (data.pointIndex == data.pointList.Count)
                    data.isReverse = true;
                else if (data.pointIndex == -1)
                    data.isReverse = false;
                break;
            default: throw new ArgumentOutOfRangeException();
        }
        return targetPos;
    }

    public override void DrawGizmos() {
        var aiObject = _stateMachine.AIObject;

        switch (_data.patrolType) {
            case EPatrolType.Stay:
                break;
            case EPatrolType.CircleMove:
                var circleData = (PatrolStateCircleMoveData)_data;
                // 绘制巡逻路径
                DrawBasics.MovingArrowsLine(circleData.lastTargetPos, circleData.targetPos, Color.blue,
                                            lineWidth: 0.2f, lengthOfArrows: 0.3f,
                                            customAmplitudeAndTextDir: Vector3.forward);
                // 绘制巡逻外圈范围
                DrawShapes.Circle(circleData.centerPos, circleData.radius, Color.magenta,
                                  Vector3.up, lineWidth: 0.05f);
                // 绘制巡逻内圈范围
                DrawShapes.Circle(circleData.centerPos, circleData.radius / 2, Color.magenta,
                                  Vector3.up, lineWidth: 0.02f);
                break;
            case EPatrolType.PathMove:
                var pathData = (PatrolStatePathMoveData)_data;
                // 绘制巡逻路径
                DrawBasics.PointList(pathData.pointList, markingCrossLinesWidth: 0.1f);
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        // 绘制脱离范围
        DrawShapes.Circle(aiObject.Transform.position, _data.patrolDistance, Color.green,
                          Vector3.up, lineWidth: 0.05f, outlineStyle: DrawBasics.LineStyle.dotted);
    }
}