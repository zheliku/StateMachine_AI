using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(BackStateData), menuName = "ScriptableObject/" + nameof(BackStateData))]
public class BackStateData : ScriptableObject
{
    public float backDistance = 5f; // 与目标小于该距离，则停止返回，继续追逐
}