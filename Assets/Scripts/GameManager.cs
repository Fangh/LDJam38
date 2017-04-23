using UnityEngine;
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

	[HideInInspector]
	public static GameManager Instance = null;
	public int NbMicrobBirth = -2;
	public string[] names = new string[]{"Benjamin","Chloé","Yanis","Adrien","Alexis","Guillaume","Louna","Louise","Erwan","Bastien","Katell","Bastien","Maelys","Zacharis","Maxence","Alexis","Davy","Noémie","Kimberley","Dylan","Cédric","Adam","Ambre","Timothée","Dimitri","Amélie","Kimberley","Lina","Simon","Rémi","Esteban","Maxime","Kimberley","Killian","Luna","Chaïma","Clotilde","Ambre","Valentine","Syrine","Samuel","Louna","Nolan","Éléna","Lily","Laura","Timothée","Hugo","Victor","Cédric","Célia","Marion","Justine","Maxime","Léonie","Timothée","Élouan","Erwan","Lucas","Inès","Lutécia","Louis","Davy","Alexandra","Guillaume","Jeanne","Yüna","Maïlé","Davy","Lutécia","Anthony","Amine","Jasmine","Tristan","Clotilde","Samuel","Jérémy","Élouan","Élise","Éloïse","Nina","Julien","Gabin","Rose","Jérémy","Kimberley","Maxime","Anthony","Solene","Lucie","Julien","Jordan","Ambre","Clément","Nicolas","Pauline","Loevan","Marion","Cédric","Noë","Célia","Valentin","Simon","Florian","Élisa","Anthony","Jeanne","Margaux","Nathan","Yasmine","Amine","Mathéo","Capucine","Gabriel","Thibault","Lauriane","Célia","Constant","Maryam","Samuel","Romain","Gaspard","Tristan","Mohamed","Alexis","Émile","Malik","Maryam","Salomé","Pierre","Dylan","Maxime","Luna","Guillaume","Romane","Dorian","Alexis","Juliette","Dorian","Gabin","Quentin","Lisa","Loevan","Jade","Dylan","Florian","Adam","Nathan","Élisa","Timothée","Marwane","Clémence","Sara","Juliette","Julien","Charlotte","Anthony","Dorian","Syrine","Maelys","Eva","Dylan","Loane","Marie","Adrian","Dorian","Clotilde","Colin","Gilbert","Lilou","Jade","Maxime","Mélissa","Océane","Lamia","Alexandre","Éloïse","Lorenzo","Alexandre","Ambre","Alexandre","Victor","Dorian","Marine","Elsa","Elsa","Mehdi","Louise","Yanis","Esteban","Émilie","Noë","Rosalie","Loevan","Chaïma","Nolan","Evan","Kevin","Zoé"};
	public BaseTool currentTool = null;
	public List<MicrobIA> microbsList = new List<MicrobIA>();
	public bool killEveryone = false;

	private int currentLife = 0;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
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
	}

	public void Hit()
	{
		currentLife--;
		if (currentLife ==0)
		{
			GetComponent<Animator>().SetTrigger("Destroy");
			GameManager.Instance.GameOver();
			GameObject fx = GameObject.Instantiate(FX_GameOver, transform.position,Quaternion.identity) as GameObject;
			fx.GetComponent<ParticleSystem>().collision.SetPlane(0,collisionPlane.transform);
			Destroy(fx,10f);
		}
		else
		{
			Debug.Log("Current life of petri = " + currentLife);
		}
	}

	void Update()
	{
		if (killEveryone)
		{
			if (microbsList.Count == 0)
			{
				gameOverPanel.SetActive(true);
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
