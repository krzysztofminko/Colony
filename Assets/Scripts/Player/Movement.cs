using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
	[HideMonoScript, RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
	public class Movement : MonoBehaviour
	{
		public float walkSpeed = 4;
		public float runSpeed = 8;

		[SerializeField]
		private LayerMask layerMask;
		[SerializeField]
		private float rayLength = 100;

		[ShowInInspector, ReadOnly]
		public bool IsRunning { get; private set; } = true;

		private Animator animator;
		private NavMeshAgent nmAgent;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			nmAgent = GetComponent<NavMeshAgent>();
		}

		private void Update()
		{
			//Toggle run
			if (Input.GetButtonDown("Run"))
				IsRunning = !IsRunning;
			nmAgent.speed = IsRunning ? runSpeed : walkSpeed;

			if (Input.GetKey(KeyCode.Space))
			{
				//Stand ground..
				nmAgent.SetDestination(transform.position);

				//..and look around
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, rayLength, layerMask))
				{
					transform.rotation = Quaternion.RotateTowards(
						transform.rotation,
						Quaternion.LookRotation(Vector3.ProjectOnPlane(hit.point, Vector3.up) - Vector3.ProjectOnPlane(transform.position, Vector3.up)),
						1000 * Time.deltaTime);
				}
			}
			else if (Input.GetMouseButton(1) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100.0f)) 
			{ 
				//Move to clicked point
				nmAgent.SetDestination(hit.point);
			}

			//animator.SetFloat("movementSpeed", nmAgent.velocity.sqrMagnitude);
		}

		private void OnDisable()
		{
			nmAgent.SetDestination(transform.position);
			//animator.SetFloat("movementSpeed", 0);
		}

	}
}