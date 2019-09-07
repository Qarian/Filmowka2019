using UnityEngine;

public class SkyManager : MonoBehaviour
{
	[SerializeField]
	float skyRotationSpeed = default;

	Material skybox;

	private void Start()
	{
		skybox = RenderSettings.skybox;
	}

	private void Update()
	{
		skybox.SetFloat("_Rotation", Time.time * skyRotationSpeed);
	}
}
