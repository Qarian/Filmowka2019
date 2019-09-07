using UnityEngine;

public class StarScript : MonoBehaviour
{
	[SerializeField]
	float speed = 5f;

	[SerializeField]
	Transform target = default;

	Vector3 dir;

    void Start()
    {
		FlyToTarget();

	}

	void FlyToTarget()
	{
		dir = (target.position - transform.position).normalized;
		transform.LookAt(target);
		//transform.Rotate(Vector3.up, 90, Space.Self);
	}

    void Update()
    {
		transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}
