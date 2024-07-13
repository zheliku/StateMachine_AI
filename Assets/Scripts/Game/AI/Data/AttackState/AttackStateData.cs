using UnityEngine;

[CreateAssetMenu(fileName = nameof(AttackStateData), menuName = "ScriptableObject/" + nameof(AttackStateData))]
public class AttackStateData : ScriptableObject
{
    public float nextAttackTime;          // 下次攻击的时间
    public float attackIntervalTime = 1f; // 攻击间隔时间
}