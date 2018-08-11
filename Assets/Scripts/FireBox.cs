using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBox : MonoBehaviour
{
	public int x, y;
	public GameObject space;

	void Awake()
	{
		for (int px = 0; px < x; px++)
			for (int py = 0; py < y; py++)
				SpawnSpace(new Vector3(px, py));

	}

	private void SpawnSpace(Vector3 pos)
	{
		GameObject go = Instantiate(space);
		go.transform.parent = transform;
		go.transform.localPosition = pos;
	}
}