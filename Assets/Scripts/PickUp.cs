using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
	[SerializeField]
	List<SubtitlesFile> subtitles = default;
	[SerializeField]
	GameObject objectToActivate = default;
	[SerializeField] UnityEvent onPickUp = default;

	public float dissolveSpeed = 1.0f;
	public float startDissolveValue = 0.8f;
	public PostProcessVolume postprocess;
	public Camera mainCamera;
	public RawImage whiteScreen;
	public Material dissolveMaterial;

	Bloom bloom;

	public bool IsFastSubtitle;
	private bool triggerDissolve;
	private bool triggered = false;
	private float startBloom;

	private float initialBloom;
	private Color initialColor;
	private float initialFieldOfView;

	private float currentDisolve;
	
	Transform player;

	private void OnTriggerEnter(Collider other)
	{
		if (IsFastSubtitle)
		{
			onPickUp.Invoke();
		}
		else
		{
			player = other.transform;
			
			if (postprocess.profile.TryGetSettings(out bloom))
			{
				initialBloom = bloom.intensity.value;
			}
			initialColor = whiteScreen.color;
			initialFieldOfView = mainCamera.fieldOfView;
			triggerDissolve = true;
		}
		
		//onPickUp.Invoke();
	}
	private void Update()
	{
		IncreaseBloom();
		IncreaseDissolve();
	}
	
	public void ShowNextSubtitles()
	{
		SubtitlesManager.singleton.ShowSubtitles(subtitles);
	}

	public void ChangeActiveObject()
	{
		objectToActivate.SetActive(true);
		transform.parent.gameObject.SetActive(false);
	}

	private void IncreaseBloom()
	{
		if (triggered)
		{
			if (postprocess.profile.TryGetSettings(out bloom))
			{
				bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 300.0f, 0.025f);
			}

			whiteScreen.color = Color.Lerp(whiteScreen.color, Color.white, 0.03f);
			mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 160.0f, 0.02f);

			if (bloom.intensity.value >= 150.0f)
			{
				triggered = false;
				onPickUp.Invoke();
				
				TeleportPlayer();
			
				mainCamera.fieldOfView = initialFieldOfView;
				whiteScreen.color = initialColor;
				dissolveMaterial.SetFloat("_Dissolve", startDissolveValue);
				bloom.intensity.value = initialBloom;
			}
		}
	}
	
	private void IncreaseDissolve()
	{
		if (triggerDissolve)
		{
			currentDisolve += Time.deltaTime * dissolveSpeed;
			dissolveMaterial.SetFloat("_Dissolve",currentDisolve);

			if (dissolveMaterial.GetFloat("_Dissolve") > 1.7f)
			{
				triggered = true;
				triggerDissolve = false;
			
			}
		}
	}

	private void TeleportPlayer()
	{
		Debug.Log("",player.gameObject);
		CharacterController controller = player.GetComponent<CharacterController>();
		controller.enabled = false;
		player.position = new Vector3(0f, 1f, 0f);
		controller.enabled = true;
	}

}
