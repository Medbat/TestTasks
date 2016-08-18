using UnityEngine;

namespace Assets.Scripts
{
	[RequireComponent(typeof (Rigidbody2D))]
	[RequireComponent(typeof (BoxCollider2D))]
	[RequireComponent(typeof (UnitScript))]
	public class MonsterScript : MonoBehaviour
	{
		public float Damage;
		public float DamageInterval;

		private Rigidbody2D _rigidbody;
		private BoxCollider2D _boxCollider;
		private UnitScript _unitScript;
		private Transform _player;
		private float _damageTimer;

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_boxCollider = GetComponent<BoxCollider2D>();
			_unitScript = GetComponent<UnitScript>();
			_player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		private void Update()
		{
			if (_player == null)
				return;
			_damageTimer -= Time.deltaTime;
			var direction = _player.position - transform.position;
			var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
			transform.Translate(Vector3.up*_unitScript.Speed*Time.deltaTime);
		}

		private void OnCollisionStay2D(Collision2D col)
		{
			if (col.gameObject.tag == "Player" && _damageTimer <= 0)
			{
				col.gameObject.GetComponent<UnitScript>().TakeDamage(Damage);
				_damageTimer = DamageInterval;
			}
		}

		private void OnDestroy()
		{
			var cam = GameObject.FindGameObjectWithTag("MainCamera");
			if (cam != null) cam.GetComponent<GuiScript>().IncreaseScore();
		}
	}
}
