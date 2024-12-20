﻿using UnityEngine;

namespace Archery.Online
{
	/// <summary>
	/// Represents a spawning point in the environment.
	/// </summary>
	public class SpawnPoint : MonoBehaviour
	{
		public float Radius = 1f;

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, Radius);
		}
	}
}
