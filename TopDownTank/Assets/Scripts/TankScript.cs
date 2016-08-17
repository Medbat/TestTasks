using UnityEngine;

namespace Assets.Scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(UnitScript))]
	public class TankScript : MonoBehaviour
	{
		public Component SmallWeapon, BigWeapon;

		private Rigidbody2D _rigidbody;
		private BoxCollider2D _boxCollider;
		private UnitScript _unitScript;

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_boxCollider = GetComponent<BoxCollider2D>();
			_unitScript = GetComponent<UnitScript>();
		}
	
		private void Update()
		{
			UpdateTransform();
			SwitchWeapons();
		}

		private void SwitchWeapons()
		{
			bool isQPressed;
			if ((isQPressed = Input.GetKeyDown(KeyCode.Q)) || Input.GetKeyDown(KeyCode.W))
			{
				SmallWeapon.gameObject.SetActive(isQPressed);
				BigWeapon.gameObject.SetActive(!isQPressed);
			}
		}

		private void UpdateTransform()
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				Move(Vector3.up);
			}
			else
			{
				if (Input.GetKey(KeyCode.DownArrow))
					Move(Vector3.down);
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				Rotate(Vector3.forward);
			}
			else
			{
				if (Input.GetKey(KeyCode.LeftArrow))
					Rotate(-Vector3.forward);
			}
		}

		private void Move(Vector3 where)
		{
			transform.Translate(where * _unitScript.Speed * Time.deltaTime);
		}

		private void Rotate(Vector3 where)
		{
			transform.Rotate(where * -90 * Time.deltaTime * _unitScript.RotationSpeed);
		}
	}
}
