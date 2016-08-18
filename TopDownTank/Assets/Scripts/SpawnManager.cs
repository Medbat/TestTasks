using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
	public class SpawnManager : MonoBehaviour
	{
		public GameObject Borders;
		public int MaxMonsterCount;

		private Object[] _monsters;

		private void Start ()
		{
			_monsters = new Object[2];
			_monsters[0] = Resources.Load("Monster1");
			_monsters[1] = Resources.Load("Monster2");
			Random.InitState(DateTime.Now.GetHashCode());
		}

		private void Update ()
		{
			if (GameObject.FindGameObjectsWithTag("Monster").Length < MaxMonsterCount)
			{
				var rand = Random.value;
				GameObject spawnedMonster;
				if (rand < 0.6)
				{
					spawnedMonster = SpawnMonster(0);
				}
				else
				{
					spawnedMonster = SpawnMonster(1);
				}
				foreach (var col in Borders.GetComponentsInChildren<Collider2D>())
				{
					Physics2D.IgnoreCollision(col, spawnedMonster.GetComponent<Collider2D>());
				}
			}

		}

		private GameObject SpawnMonster(int number)
		{
			var places = GetComponentsInChildren<Transform>();
			return (GameObject)Instantiate(
				_monsters[number], 
				places[Random.Range(0, places.Length)].position,
				Quaternion.identity);
		}
	}
}
