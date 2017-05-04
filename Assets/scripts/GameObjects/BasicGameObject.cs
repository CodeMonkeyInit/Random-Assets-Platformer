using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
	public abstract class BasicGameObject : MonoBehaviour
	{

		protected static int instanceCount;
		protected int id;

		public int ID { get { return id; } }

		protected virtual void  Awake()
		{
			id = instanceCount;
			instanceCount++;
		}

		protected virtual void  OnDestroy()
		{
			instanceCount--;
		}
	}
}