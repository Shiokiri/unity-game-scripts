using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
	public float attackRange;
	public float skillRange;
	public float coolDown; // CD 冷却时间
	public int minDamage; // 最小攻击值
	public int maxDamage; // 最大攻击值
	public float criticalMultiplier; // 暴击伤害
	public float criticalChance; // 暴击率
}
