/// <summary>
/// 巡逻类型
/// </summary>
public enum EPatrolType
{
    Stay,       // 原地播放某个动作（睡觉、放哨等）
    CircleMove, // 圆形范围内随机移动
    PathMove    // 按照路径移动
}

public enum EPatrolPathMoveType
{
    Loop,   // 循环
    GoBack, // 往返
}