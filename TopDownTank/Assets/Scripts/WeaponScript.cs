using UnityEngine;

namespace Assets.Scripts
{
	public class WeaponScript : MonoBehaviour
	{
		public float Damage;
		public float AttackInterval;
		public string ProjectileName;
		public Transform Muzzle;

		private float _weaponAttackTimer;
		private Object _muzzleFlash;
		private Object _projectile;

		private void Start ()
		{
			_muzzleFlash = Resources.Load("MuzzleFlash");
			_projectile = Resources.Load(ProjectileName);
		}

		private void Update ()
		{
			if (Input.GetKey(KeyCode.X))
				TryToShoot();
			if (_weaponAttackTimer > 0)
				_weaponAttackTimer -= Time.deltaTime;
		}

		private void TryToShoot()
		{
			if (_weaponAttackTimer > 0)
				return;
			var flash = (GameObject)Instantiate(_muzzleFlash, Muzzle.position, transform.rotation);
			flash.transform.parent = transform;
			var projectile = (GameObject)Instantiate(_projectile, Muzzle.transform.position, transform.rotation);
			projectile.GetComponent<ProjectileScript>().Damage = Damage;
			_weaponAttackTimer = AttackInterval;
		}
	}
}
