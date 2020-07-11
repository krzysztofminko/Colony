using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Player
{
	[HideMonoScript]
	public class Targeting : MonoBehaviour
	{
		[SerializeField]
		private LayerMask layerMask;
		[SerializeField]
		private float rayLength = 100;

		[ShowInInspector, ReadOnly]
		public Target Target {get; private set; }


		private void Update()
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, rayLength, layerMask))
			{
				Target newTarget = hit.collider.GetComponent<Target>();
				if (newTarget)
					SetTarget(newTarget);
				else
					ClearTarget();
			}
			else
			{
				ClearTarget();
			}
		}

		private void SetTarget(Target newTarget)
		{
			if (!newTarget)
			{
				Debug.LogError("Can't select null.", this);
			}
			else
			{
				if (Target && Target != newTarget)
					ClearTarget();
				Target = newTarget;
				if (Target)
					Target.Select();
			}
		}

		private void ClearTarget()
		{
			if (Target)
			{
				Target.Unselect();
				Target = null;
			}
		}
	}
}
 