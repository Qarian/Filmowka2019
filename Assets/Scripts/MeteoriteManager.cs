using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
	[SerializeField]
	GameObject meteoritePrefab = default;
	[SerializeField]
	float radius = 2;
	public float generatingFrequency = default;

	List<GameObject> meteoritesGO;

	bool generating = true;

	private void OnDrawGizmos()
	{
		Transform T = GetComponent<Transform>();
		Gizmos.color = Color.white;
		float theta = 0f;
		float x = radius * Mathf.Cos(theta);
		float y = radius * Mathf.Sin(theta);
		Vector3 pos = T.position + new Vector3(x, 0, y);
		Vector3 newPos = pos;
		Vector3 lastPos = pos;
		for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
		{
			x = radius * Mathf.Cos(theta);
			y = radius * Mathf.Sin(theta);
			newPos = T.position + new Vector3(x, 0, y);
			Gizmos.DrawLine(pos, newPos);
			pos = newPos;
		}
		Gizmos.DrawLine(pos, lastPos);
	}

	public void StartGeneratingMeteorites()
	{
		meteoritesGO = new List<GameObject>();
		StartCoroutine(MakeMeteorite());
	}

	IEnumerator MakeMeteorite()
	{
		yield return new WaitForSeconds(generatingFrequency);
		while (generating)
		{
			Vector3 pos = transform.position + new Vector3(Random.Range(-radius, radius), 0f, Random.Range(-radius, radius));
			meteoritesGO.Add(Instantiate(meteoritePrefab, pos, Quaternion.identity));
			yield return new WaitForSeconds(generatingFrequency);
		}
	}

	public void StopGeneratingMeteorites()
	{
		generating = false;
		DestroyAllMeteorites();
	}

	public void DestroyAllMeteorites()
	{
		for (int i = 0; i < meteoritesGO.Count; i++)
		{
			if (meteoritesGO[i] != null)
				Destroy(meteoritesGO[i]);
		}
	}
}
