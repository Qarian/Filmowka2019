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
	int strength;

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

	public void StartGeneratingMeteorites(int strength)
	{
		this.strength = strength;
		transform.position = new Vector3(transform.position.x, 8f - strength * 0.7f, transform.position.z);
		radius = 9f - 1f * strength;
		generatingFrequency = 2.2f - 0.5f * strength;
		meteoritesGO = new List<GameObject>();
		StartCoroutine(MakeMeteorite());
	}

	IEnumerator MakeMeteorite()
	{
		yield return new WaitForSeconds(2f);
		while (generating)
		{
			Vector3 pos = transform.position + new Vector3(Random.Range(-radius, radius), 0f, Random.Range(-radius, radius));
			GameObject go = Instantiate(meteoritePrefab, pos, Quaternion.identity);
			meteoritesGO.Add(go);
			go.GetComponent<StarScript>().speed = 3 + strength * 0.9f;
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
