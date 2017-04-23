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
	public string[] names = new string[]{"Benjamin","Chloé","Yanis","Adrien","Alexis","Guillaume","Louna","Louise","Erwan","Bastien","Katell","Bastien","Maelys","Zacharis","Maxence","Alexis","Davy","Noémie","Kimberley","Dylan","Cédric","Adam","Ambre","Timothée","Dimitri","Amélie","Kimberley","Lina","Simon","Rémi","Esteban","Maxime","Kimberley","Killian","Luna","Chaïma","Clotilde","Ambre","Valentine","Syrine","Samuel","Louna","Nolan","Éléna","Lily","Laura","Timothée","Hugo","Victor","Cédric","Célia","Marion","Justine","Maxime","Léonie","Timothée","Élouan","Erwan","Lucas","Inès","Lutécia","Louis","Davy","Alexandra","Guillaume","Jeanne","Yüna","Maïlé","Davy","Lutécia","Anthony","Amine","Jasmine","Tristan","Clotilde","Samuel","Jérémy","Élouan","Élise","Éloïse","Nina","Julien","Gabin","Rose","Jérémy","Kimberley","Maxime","Anthony","Solene","Lucie","Julien","Jordan","Ambre","Clément","Nicolas","Pauline","Loevan","Marion","Cédric","Noë","Célia","Valentin","Simon","Florian","Élisa","Anthony","Jeanne","Margaux","Nathan","Yasmine","Amine","Mathéo","Capucine","Gabriel","Thibault","Lauriane","Célia","Constant","Maryam","Samuel","Romain","Gaspard","Tristan","Mohamed","Alexis","Émile","Malik","Maryam","Salomé","Pierre","Dylan","Maxime","Luna","Guillaume","Romane","Dorian","Alexis","Juliette","Dorian","Gabin","Quentin","Lisa","Loevan","Jade","Dylan","Florian","Adam","Nathan","Élisa","Timothée","Marwane","Clémence","Sara","Juliette","Julien","Charlotte","Anthony","Dorian","Syrine","Maelys","Eva","Dylan","Loane","Marie","Adrian","Dorian","Clotilde","Colin","Gilbert","Lilou","Jade","Maxime","Mélissa","Océane","Lamia","Alexandre","Éloïse","Lorenzo","Alexandre","Ambre","Alexandre","Victor","Dorian","Marine","Elsa","Elsa","Mehdi","Louise","Yanis","Esteban","Émilie","Noë","Rosalie","Loevan","Chaïma","Nolan","Evan","Kevin","Zoé"};
	public BaseTool currentTool = null;
	public List<MicrobIA> microbsList = new List<MicrobIA>();
	public bool killEveryone = false;
	public int currentLife = 0;
	public AudioClip SFX_GameOver = null;

	private List<int> brokenSteps = new List<int>();

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
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
		microbsList.Remove(m);
		if (microbsList.Count == 0)
			killEveryone = true;
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

		GetComponent<AudioSource>().PlayOneShot(SFX_GameOver);
		CameraManager.Instance.duration = 1f;
		CameraManager.Instance.magnitude = 1f;
		CameraManager.Instance.StartCoroutine("Shake");

		dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 1f);
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
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0f);
		}
		else if (currentLife == brokenSteps[3])
		{
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0.2f);
		}
		else if (currentLife == brokenSteps[2])
		{
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0.4f);
		}
		else if (currentLife == brokenSteps[1])
		{
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0.6f);
		}
		else if (currentLife == brokenSteps[0])
		{
			CameraManager.Instance.StartCoroutine("Shake");
			dishBrokenRenderer.material.color = new Color(dishBrokenRenderer.material.color.r, dishBrokenRenderer.material.color.g, dishBrokenRenderer.material.color.b, 0.8f);
		}
	}

	void Update()
	{
		if (killEveryone)
		{
			if (microbsList.Count == 0)
			{
				gameOverPanel.SetActive(true);
				gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Your specie survives "+ TimerScoring.Instance.formatedTime;
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
}
