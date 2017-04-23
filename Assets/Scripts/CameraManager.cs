using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	public float duration = 1f;
	public float magnitude = 1f;
	public float maxValue = 1f;
	public float minValue = -1f;

	public static CameraManager Instance = null;

	// Use this for initialization
	void Start () 
	{
		Instance = this;	
	}


	IEnumerator Shake() 
	{

		float elapsed = 0.0f;

		Vector3 originalCamPos = transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.Range(minValue, maxValue);
			float y = Random.Range(minValue, maxValue);
			x *= magnitude * damper;
			y *= magnitude * damper;

			transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}

		transform.position = originalCamPos;
	}
}
