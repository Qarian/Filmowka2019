using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
	[SerializeField] UnityEvent onPickUp = default;

	public PostProcessVolume postprocess;
	public Camera mainCamera;
	public RawImage whiteScreen;
	Bloom bloom;
	private bool triggered = false;
	
	

	private float startBloom;

	private float initialBloom;
	private Color initialColor;
	private float initialFieldOfView;

	private void OnTriggerEnter(Collider other)
	{
		if (postprocess.profile.TryGetSettings(out bloom))
		{
			initialBloom = bloom.intensity.value;
		}
		initialColor = whiteScreen.color;
		initialFieldOfView = mainCamera.fieldOfView;
		triggered = true;
		//onPickUp.Invoke();
	}
	private void Update()
	{
		if (triggered)
		{
			if (postprocess.profile.TryGetSettings(out bloom))
			{
				bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 300.0f, 0.025f);
			}

			whiteScreen.color = Color.Lerp(whiteScreen.color, Color.white, 0.03f);
			mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 160.0f, 0.02f);

			if (bloom.intensity.value >= 200.0f)
			{
				triggered = false;
				onPickUp.Invoke();
				mainCamera.fieldOfView = initialFieldOfView;
				whiteScreen.color = initialColor;
				bloom.intensity.value = initialBloom;

			}
			
		}

	}


		public void ShowNextSubtitles()
		{
			SubtitlesManager.singleton.ShowNextSubtitles();
		}
	
}
