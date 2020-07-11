using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

	[Category("Character")]
	public class MoveToTarget : ActionTask<Character>
	{
		[RequiredField]
		public BBParameter<Target> target;
		public bool defaultDistance = true;
		[Min(0)]
		public BBParameter<float> alternativeDistance;

		private Target savedTarget;


		protected override void OnExecute()
		{
			savedTarget = target.value;
		}

		protected override void OnUpdate()
		{
			if (savedTarget)
			{
				if (defaultDistance)
				{
					if (agent.GoTo(savedTarget.transform.position))
						EndAction(true);
				}
				else
				{
					if (agent.GoTo(savedTarget.transform.position, alternativeDistance.value))
						EndAction(true);
				}
			}
			else
			{
				EndAction(false);
			}
		}

		protected override void OnStop()
		{
			agent.Stop();
		}
	}
}