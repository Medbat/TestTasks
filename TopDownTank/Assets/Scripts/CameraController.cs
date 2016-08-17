using UnityEngine;

namespace Assets.Scripts
{
	public class CameraController : MonoBehaviour
	{
		private Transform _player;

		private void Awake()
		{
			_player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		private void Update()
		{
			if (_player != null)
			{
				_player = GameObject.FindGameObjectWithTag("Player").transform;
				transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
			}
		}
	}
}
