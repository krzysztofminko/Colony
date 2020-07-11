using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions{

	[Category("Character")]
	[Description("Get Attack Range value from active weapon holded by character.")]
	public class GetAttackRange : ActionTask<Character>
	{
		public BBParameter<bool> secondary;
		[RequiredField]
		public BBParameter<float> attackRange;

		protected override void OnExecute()
		{
			if (secondary.value)
				attackRange = (agent.GetComponent<Inventory>().activeItem as WeaponItem).secondaryAttack.range;
			else
				attackRange = (agent.GetComponent<Inventory>().activeItem as WeaponItem).primaryAttack.range;
			EndAction(true);
		}
	}
}