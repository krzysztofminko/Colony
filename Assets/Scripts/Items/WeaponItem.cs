using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class WeaponItem : Item
{
	public int damage;
	public AttackSettings primaryAttack;
	public AttackSettings secondaryAttack;
}
