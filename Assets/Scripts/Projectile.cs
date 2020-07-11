using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Experimental.TerrainAPI;

public class Projectile : MonoBehaviour
{
	[Min(0)]
	public int damage = 10;
	[Min(1)]
	public float speed = 10;
	public LayerMask layerMask;

	[Min(0)]
	public float explosionRange;
	[Min(0)]
	public int explosionDamage;

	private float lifeRange = 100;


	private void Update()
	{
		if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, speed * Time.deltaTime, layerMask, QueryTriggerInteraction.Ignore))
		{
			Character character = hit.collider.GetComponent<Character>();
			if (character)
				character.Health -= damage;

			if (explosionRange > 0)
			{
				Collider[] colliders = Physics.OverlapSphere(hit.point, explosionRange, layerMask);
				for (int i = 0; i < colliders.Length; i++)
				{
					character = colliders[i].GetComponent<Character>();
					if (character)
						character.Health -= explosionDamage;
				}
			}
			Destroy(gameObject);
		}
		else
		{
			lifeRange -= speed * Time.deltaTime;

			if (lifeRange < 0)
				Destroy(gameObject);
			else
				transform.position += transform.forward * speed * Time.deltaTime;
		}

	}
}
