using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	public int life = 10000;
	public GameObject FX_GameOver = null;
	public GameObject collisionPlane = null;
	public GameObject gameOverPanel = null;
	public Renderer dishBrokenRenderer = null;

	[HideInInspector]
	public static GameManager Instance = null;
	public int NbMicrobBirth = -2;
	public string[] names = new string[0];
	public BaseTool currentTool = null;
	public List<MicrobIA> microbsList = new List<MicrobIA>();
	public bool killEveryone = false;
	public int currentLife = 0;
	public AudioClip SFX_GameOver = null;
	public AudioClip SFX_Step = null;
	public int nbSterilized = 0;
	
	private List<int> brokenSteps = new List<int>();

	private bool manualKill = true;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		names = new string[] { "Benjamin", "Chloé", "Yanis", "Adrien", "Alexis", "Guillaume", "Louna", "Louise", "Erwan", "Bastien", "Katell", "Bastien", "Maelys", "Zacharis", "Maxence", "Alexis", "Davy", "Noémie", "Kimberley", "Dylan", "Cédric", "Adam", "Ambre", "Timothée", "Dimitri", "Amélie", "Kimberley", "Lina", "Simon", "Rémi", "Esteban", "Maxime", "Kimberley", "Killian", "Luna", "Chaïma", "Clotilde", "Ambre", "Valentine", "Syrine", "Samuel", "Louna", "Nolan", "Alison", "Lily", "Laura", "Timothée", "Hugo", "Victor", "Cédric", "Célia", "Marion", "Justine", "Maxime", "Léonie", "Timothée", "Nathalie", "Erwan", "Lucas", "Inès", "Lutécia", "Louis", "Davy", "Alexandra", "Guillaume", "Jeanne", "Yüna", "Maïlé", "Davy", "Lutécia", "Anthony", "Amine", "Jasmine", "Tristan", "Clotilde", "Samuel", "Jérémy", "Yann", "Johnson", "Trump", "Nina", "Julien", "Gabin", "Rose", "Jérémy", "Kimberley", "Maxime", "Anthony", "Solene", "Lucie", "Julien", "Jordan", "Ambre", "Clément", "Nicolas", "Pauline", "Loevan", "Marion", "Cédric", "Noë", "Célia", "Valentin", "Simon", "Florian", "Philipe", "Anthony", "Jeanne", "Margaux", "Nathan", "Yasmine", "Amine", "Mathéo", "Capucine", "Gabriel", "Thibault", "Lauriane", "Célia", "Constant", "Maryam", "Samuel", "Romain", "Gaspard", "Tristan", "Mohamed", "Alexis", "George Abitbol", "Malik", "Maryam", "Salomé", "Pierre", "Dylan", "Maxime", "Luna", "Guillaume", "Romane", "Dorian", "Alexis", "Juliette", "Dorian", "Gabin", "Quentin", "Lisa", "Loevan", "Jade", "Dylan", "Florian", "Adam", "Nathan", "Astérix", "Timothée", "Marwane", "Clémence", "Sara", "Juliette", "Julien", "Charlotte", "Anthony", "Dorian", "Syrine", "Maelys", "Eva", "Dylan", "Loane", "Marie", "Adrian", "Dorian", "Clotilde", "Colin", "Gilbert", "Lilou", "Jade", "Maxime", "Mélissa", "Océane", "Lamia", "Alexandre", "Numérobis", "Lorenzo", "Alexandre", "Ambre", "Alexandre", "Victor", "Dorian", "Marine", "Elsa", "Elsa", "Mehdi", "Louise", "Yanis", "Esteban", "Batman", "Noë", "Rosalie", "Loevan", "Chaïma", "Nolan", "Evan", "Kevin", "Zoé" };

		int unCinquieme = life/5;
		brokenSteps.Add(unCinquieme);
		brokenSteps.Add(unCinquieme*2);
		brokenSteps.Add(unCinquieme*3);
		brokenSteps.Add(unCinquieme*4);
		brokenSteps.Add(life);

		killEveryone = false;
		gameOverPanel.SetActive(false);
		currentLife = life;
	}

	public void AddMicrob(MicrobIA m)
	{
		microbsList.Add(m);
		NbMicrobBirth++;
	}

	public void RemoveMicrob(MicrobIA m)
	{
		if (m.isAffectedByGeneticAlteration)
			nbSterilized--;

		microbsList.Remove(m);
		if (microbsList.Count == 0)
		{
			killEveryone = true;
		}
		if(microbsList.Count == nbSterilized)
		{
			gameOverPanel.SetActive(true);
			gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "Your population is infertil.\n Is this a victory or a defeat ?";
			gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Your specie survives " + TimerScoring.Instance.formatedTime;
			gameOverPanel.transform.GetChild(2).GetComponent<Text>().text = "With a " + (NbMicrobBirth + 2) + " subjects peak";

		}
	}

	public string GetRandomName()
	{
		return names[Random.Range(0, names.Length-1)];
	}

	public void GameOver()
	{
		foreach (MicrobIA m in microbsList)
		{
			m.isDying = true;
		}
		killEveryone = true;
		manualKill = false;

		GetComponent<AudioSource>().PlayOneShot(SFX_GameOver);
		CameraManager.Instance.duration = 1f;
		CameraManager.Instance.magnitude = 1f;
		CameraManager.Instance.StartCoroutine("Shake");

		dishBrokenRenderer.gameObject.SetActive(false);
		GetComponent<Animator>().SetTrigger("Destroy");

		GameObject fx = GameObject.Instantiate(FX_GameOver, transform.position,Quaternion.identity) as GameObject;
		fx.GetComponent<ParticleSystem>().collision.SetPlane(0,collisionPlane.transform);
		Destroy(fx,10f);
	}

	public void Hit()
	{
		currentLife--;
		//Debug.Log("Current life of petri = " + currentLife);
		if (currentLife ==0)
		{
			GameOver();
		}
		else if (currentLife == brokenSteps[4])
		{
			GetComponent<AudioSource>().PlayOneShot(SFX_Step);
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0f);
		}
		else if (currentLife == brokenSteps[3])
		{
			GetComponent<AudioSource>().PlayOneShot(SFX_Step);
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0.4f);
		}
		else if (currentLife == brokenSteps[2])
		{
			GetComponent<AudioSource>().PlayOneShot(SFX_Step);
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0.6f);
		}
		else if (currentLife == brokenSteps[1])
		{
			GetComponent<AudioSource>().PlayOneShot(SFX_Step);
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0.8f);
		}
		else if (currentLife == brokenSteps[0])
		{
			GetComponent<AudioSource>().PlayOneShot(SFX_Step);
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 1f);
		}
	}

	void Update()
	{
		if (killEveryone)
		{
			if (microbsList.Count == 0)
			{
				//Invoke( "Restart", 5f);
				gameOverPanel.SetActive(true);

				if (manualKill)
					gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "You killed your subjects.\n Is this a victory or a defeat ?";
				else
					gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "Your specie destroyed its own small world...";

				gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Your specie survives "+ TimerScoring.Instance.formatedTime;
				gameOverPanel.transform.GetChild(2).GetComponent<Text>().text = "With a " + (NbMicrobBirth + 2) + " subjects peak";
				killEveryone = false;
				return;
			}
			else if (microbsList.Count > 100)
			{
				for (int i = 0; i < 100; i++)
				{
					Destroy(microbsList[i].gameObject);
				}
				microbsList.RemoveRange(0,100);
			}
			else
			{
				Destroy(microbsList[0].gameObject);
				microbsList.RemoveRange(0,1);
			}
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
