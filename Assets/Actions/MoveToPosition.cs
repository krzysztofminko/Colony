using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

	[Category("Character")]
	public class MoveToPosition : ActionTask<Character>
	{
		[RequiredField]
		public BBParameter<Vector3> position;
		[Min(0)]
		public BBParameter<float> distance = 0.1f;


		protected override void OnUpdate()
		{
			if (agent.GoTo(position.value, distance.value))
				EndAction(true);
		}

		protected override void OnStop()
		{
			agent.Stop();
		}
	}
}