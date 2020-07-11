using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
	[Required]
	public Transform follow;

	private Vector3 offset;

	private void Awake()
	{
		if (follow)
			offset = transform.position - follow.position;
	}

	private void Update()
	{
		if (follow)
			transform.position = follow.position + offset;
	}
}
