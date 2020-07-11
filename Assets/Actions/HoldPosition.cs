using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{

	[Category("Character")]
	public class HoldPosition : ActionTask<Character>
	{
		public LayerMask layerMask;
		public float rayLength = 1000;

		protected override void OnExecute()
		{
			agent.Stop();
		}

		protected override void OnUpdate()
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, rayLength, layerMask))
			{
				agent.transform.rotation = Quaternion.RotateTowards(
					agent.transform.rotation,
					Quaternion.LookRotation(Vector3.ProjectOnPlane(hit.point, Vector3.up) - Vector3.ProjectOnPlane(agent.transform.position, Vector3.up)),
					1000 * Time.deltaTime);
			}
		}
	}
}