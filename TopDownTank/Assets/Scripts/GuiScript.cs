using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class GuiScript : MonoBehaviour
	{
		private UnitScript _player;
		private int _score;

		public void IncreaseScore()
		{
			_score++;
		}

		private void Start ()
		{
			_score = 0;
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitScript>();
		}

		private void OnGUI()
		{
			if (_player == null)
			{
				Time.timeScale = 0;
				GUI.Box(new Rect(10, 10, 100, 50), "GAME OVER!" + Environment.NewLine + "Kills: " + _score);
			}
			else
			{
				GUI.Box(new Rect(10, 10, 100, 50), "HP: " + _player.Hp + Environment.NewLine + "Kills: " + _score);
			}
		}

		
	}
}
