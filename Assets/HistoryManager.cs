using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HistoryManager : MonoBehaviour 
{
	public static HistoryManager Instance;

	public Text textComponent;

	public void Start()
	{
		Instance = this;
		textComponent = GetComponent<Text>();
	}

	public void AddEntry(string entry)
	{
		if (textComponent.text.Length > 1000)
			textComponent.text = entry;
		else
			textComponent.text += entry;
	}
}