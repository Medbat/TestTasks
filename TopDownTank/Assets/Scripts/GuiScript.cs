using UnityEngine;

namespace Assets.Scripts
{
	public class GuiScript : MonoBehaviour
	{
		private UnitScript _player;

		private void Start ()
		{
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitScript>();
		}

		private void OnGUI()
		{
			if (_player == null)
			{
				Time.timeScale = 0;
				GUI.Box(new Rect(10, 10, 100, 25), "GAME OVER!");
			}
			else
			{
				GUI.Box(new Rect(10, 10, 100, 25), "HP: " + _player.Hp);
			}
		}
	}
}
