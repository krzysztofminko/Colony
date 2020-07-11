using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions{

	[Category("Character")]
	public class Attack : ActionTask<Character>
	{
		public BBParameter<bool> secondary;
		public BBParameter<bool> holdPosition = false;
		public BBParameter<Target> target;
		[RequiredField]
		public BBParameter<bool> isBusy;

		private Inventory inventory;
		private WeaponItem weaponItem;
		private AttackSettings attackSettings;
		private Target savedTarget;

		protected override string OnInit()
		{
			inventory = agent.GetComponent<Inventory>();
			return null;
		}

		protected override void OnExecute()
		{
			savedTarget = target.value;
		}

		protected override void OnUpdate()
		{
			weaponItem = inventory.activeItem as WeaponItem;
			attackSettings = secondary.value ? weaponItem.secondaryAttack : weaponItem.primaryAttack;

			if (!holdPosition.value && !savedTarget)
			{
				EndAction(false);
			}
			else
			{
				if (holdPosition.value || agent.GoTo(savedTarget.transform.position, Mathf.Max(savedTarget.stopDistance, attackSettings.range)))
				{
					isBusy = true;
					//play animation

					if (attackSettings.projectile)
					{
						Object.Instantiate(attackSettings.projectile, agent.transform.position, savedTarget ? Quaternion.LookRotation(savedTarget.transform.position - agent.transform.position) : agent.transform.rotation);
					}

					Collider[] colliders = Physics.OverlapSphere(agent.transform.position, attackSettings.damageRange);
					for (int i = 0; i < colliders.Length; i++)
					{
						Character targetCharacter = colliders[i].gameObject.GetComponent<Character>();
						if (targetCharacter && targetCharacter != agent)
						{
							targetCharacter.Health -= (weaponItem.damage + attackSettings.additionalDamage) * attackSettings.totalDamageMultiplier;
						}
					}

					EndAction(true);
				}
			}
		}

		protected override void OnStop()
		{
			agent.Stop();
			isBusy = false;
		}
	}
}