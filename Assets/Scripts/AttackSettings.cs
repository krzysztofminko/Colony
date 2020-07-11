using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class AttackSettings : ScriptableObject
{
	[Min(0)]
	public float range;
	public Animation animation;
	public float damageRange;
	[Min(0)]
	public int additionalDamage;
	[Min(0)]
	public int totalDamageMultiplier = 1;
	public Projectile projectile;
}