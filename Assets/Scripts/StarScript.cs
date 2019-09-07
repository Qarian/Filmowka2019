using UnityEngine;

public class StarScript : MonoBehaviour
{
	[SerializeField]
	float speed = 5f;

	[SerializeField]
	GameObject meshGO;
	[SerializeField]
	ParticleSystem trailParticle;
	[SerializeField]
	GameObject boomParticleGO;
	[SerializeField]
	Transform target = default;

	Vector3 dir;
	bool hit = false;
	float particleLifetime;
	float particleTime = 0;
	Material particleMaterial;
	ParticleSystem particle;


	void Start()
	{
		FlyToTarget();
	}

	void FlyToTarget()
	{
		if (target == null)
			target = GameObject.FindGameObjectWithTag("Player").transform;
		dir = (target.position - transform.position).normalized;
		transform.LookAt(target);
		//transform.Rotate(Vector3.up, 90, Space.Self);

		SetupMaterial();

		Color newColor = particleMaterial.GetColor("_Color");
		newColor.a = 1;
		particleMaterial.SetColor("_Color", newColor);
	}

	void Update()
	{
		if (hit)
		{
			particleTime += Time.deltaTime;
			Color newColor = particleMaterial.GetColor("_Color");
			newColor.a = 1 - (particleTime / particleLifetime);
			particleMaterial.SetColor("_Color", newColor);
			return;
		}

		transform.Translate(dir * speed * Time.deltaTime, Space.World);

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
		int i = 0;
		while (i < hitColliders.Length)
		{
			Debug.Log(hitColliders[i].name, hitColliders[i].gameObject);
			if (hitColliders[i].tag == "Player")
				KillPlayer();
			else if (hitColliders[i].isTrigger == false)
				Boom();

			i++;
		}
	}

	void Boom()
	{
		Debug.Log("Boom");
		if (hit)
			return;

		hit = true;

		SetupMaterial();

		meshGO.SetActive(false);
		trailParticle.Stop();
		//GetComponent<MeshRenderer>().enabled = false;
		particle.Play();
		Destroy(gameObject, particleLifetime);
	}

	void KillPlayer()
	{

	}

	void SetupMaterial()
	{
		if (particleMaterial != null)
			return;

		particle = boomParticleGO.GetComponent<ParticleSystem>();
		particleLifetime = particle.main.startLifetime.constant;
		particleMaterial = boomParticleGO.GetComponent<ParticleSystemRenderer>().material;
	}
}
