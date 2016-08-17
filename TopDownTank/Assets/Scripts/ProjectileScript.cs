using UnityEngine;

namespace Assets.Scripts
{
	public class ProjectileScript : MonoBehaviour
	{
		public float Speed;
		public float LifeTime;
		public float Damage;
		
		private void Start()
		{
			Destroy(gameObject, LifeTime);
		}
	
		private void Update()
		{
			transform.Translate(Vector3.up * Time.deltaTime * Speed);
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.tag != "Monster") return;
			col.gameObject.GetComponent<UnitScript>().TakeDamage(Damage);
			Destroy(gameObject);
		}
	}
}
