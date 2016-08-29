using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerScript : MonoBehaviour
	{
		public Transform Placer;

		private Rigidbody2D _rigidbody;

		void Start()
		{
			var mesh = new Mesh();
			mesh.vertices = new[]
			{
				new Vector3(3, 0, 0), // 0
				new Vector3(2, -1, 0), // 1
				new Vector3(3, -2, 0), // 2
				new Vector3(4, -1, 0), // 3
				new Vector3(0, -2, 0), // 4
				new Vector3(1, -2, 0), // 5
				new Vector3(0, -5, 0), // 6
				new Vector3(1, -5, 0), // 7
				new Vector3(5, -2, 0), // 8
				new Vector3(3, -6, 0), // 9
				new Vector3(6, -2, 0), // 10
				new Vector3(5, -5, 0), // 11
				new Vector3(6, -5, 0), // 12
				new Vector3(2, -6, 0), // 13
				new Vector3(4, -6, 0), // 14
				new Vector3(2, -7, 0), // 15
				new Vector3(4, -7, 0) // 16
			};
			var uvs = new Vector2[mesh.vertices.Length];
			for (var i = 0; i < uvs.Length; i++)
			{
				uvs[i] = new Vector2(0, 0);
			}
			mesh.uv = uvs;
			mesh.triangles = new[]
			{
				// head
				0, 3, 1,
				2, 1, 3,
				// right arm
				4, 5, 6,
				7, 6, 5,
				// body
				5, 8, 9,
				// left arm
				8, 10, 11,
				12, 11, 10,
				// platform
				13, 14, 15,
				15, 14, 16
			};
			mesh.RecalculateNormals();
			GetComponent<MeshFilter>().mesh = mesh;
			transform.position = Placer.position;

			_rigidbody = GetComponent<Rigidbody2D>();
		}

		// Update is called once per frame
		void Update()
		{
			_rigidbody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")*10, 0.8f), _rigidbody.velocity.y);
		}
	}
}
