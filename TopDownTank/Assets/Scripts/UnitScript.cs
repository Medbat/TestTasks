using UnityEngine;

namespace Assets.Scripts
{
	public class UnitScript : MonoBehaviour
	{
		public float Hp;
		public float Defence;
		public float Speed;
		public float RotationSpeed;

		public void TakeDamage(float damage)
		{
			Hp -= damage*(1 - Defence);
		}

		// Use this for initialization
		private void Start ()
		{
	
		}
	
		// Update is called once per frame
		private void Update()
		{
			if (Hp <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
