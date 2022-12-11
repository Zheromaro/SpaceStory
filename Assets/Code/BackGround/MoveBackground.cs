using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackGround
{
	public class MoveBackground : MonoBehaviour
	{

		public float speed;
		private float x;
		public float PontoDeDestino;
		public float PontoOriginal;

		// Use this for initialization
		void Start()
		{
			//PontoOriginal = transform.position.x;
		}


		void Update()
		{

			x = transform.position.x;
			x += speed * Time.deltaTime;
			transform.position = new Vector3(x, transform.position.y, transform.position.z);

			if (x <= PontoDeDestino)
			{

				Debug.Log("hhhh");
				x = PontoOriginal;
				transform.position = new Vector3(x, transform.position.y, transform.position.z);
			}
		}

	}
}
