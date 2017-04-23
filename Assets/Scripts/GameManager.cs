using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{	
	public static GameManager Instance = null;
	public int NbMicrobBirth = -2;
	public string[] names = new string[]{"Benjamin","Chloé","Yanis","Adrien","Alexis","Guillaume","Louna","Louise","Erwan","Bastien","Katell","Bastien","Maelys","Zacharis","Maxence","Alexis","Davy","Noémie","Kimberley","Dylan","Cédric","Adam","Ambre","Timothée","Dimitri","Amélie","Kimberley","Lina","Simon","Rémi","Esteban","Maxime","Kimberley","Killian","Luna","Chaïma","Clotilde","Ambre","Valentine","Syrine","Samuel","Louna","Nolan","Éléna","Lily","Laura","Timothée","Hugo","Victor","Cédric","Célia","Marion","Justine","Maxime","Léonie","Timothée","Élouan","Erwan","Lucas","Inès","Lutécia","Louis","Davy","Alexandra","Guillaume","Jeanne","Yüna","Maïlé","Davy","Lutécia","Anthony","Amine","Jasmine","Tristan","Clotilde","Samuel","Jérémy","Élouan","Élise","Éloïse","Nina","Julien","Gabin","Rose","Jérémy","Kimberley","Maxime","Anthony","Solene","Lucie","Julien","Jordan","Ambre","Clément","Nicolas","Pauline","Loevan","Marion","Cédric","Noë","Célia","Valentin","Simon","Florian","Élisa","Anthony","Jeanne","Margaux","Nathan","Yasmine","Amine","Mathéo","Capucine","Gabriel","Thibault","Lauriane","Célia","Constant","Maryam","Samuel","Romain","Gaspard","Tristan","Mohamed","Alexis","Émile","Malik","Maryam","Salomé","Pierre","Dylan","Maxime","Luna","Guillaume","Romane","Dorian","Alexis","Juliette","Dorian","Gabin","Quentin","Lisa","Loevan","Jade","Dylan","Florian","Adam","Nathan","Élisa","Timothée","Marwane","Clémence","Sara","Juliette","Julien","Charlotte","Anthony","Dorian","Syrine","Maelys","Eva","Dylan","Loane","Marie","Adrian","Dorian","Clotilde","Colin","Gilbert","Lilou","Jade","Maxime","Mélissa","Océane","Lamia","Alexandre","Éloïse","Lorenzo","Alexandre","Ambre","Alexandre","Victor","Dorian","Marine","Elsa","Elsa","Mehdi","Louise","Yanis","Esteban","Émilie","Noë","Rosalie","Loevan","Chaïma","Nolan","Evan","Kevin","Zoé"};
	public BaseTool currentTool = null;
	public List<MicrobIA> microbsList = new List<MicrobIA>();

	public bool killEveryone = false;

	void Awake()
	{
		Instance = this;
	}

	public void AddMicrob(MicrobIA m)
	{
		microbsList.Add(m);
		NbMicrobBirth++;
	}

	public void RemoveMicrob(MicrobIA m)
	{
		microbsList.Remove(m);
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

	void Update()
	{
		if (killEveryone)
		{
			if (microbsList.Count == 0)
			{
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

				

			/* 
			List<GameObject> microbsToKill = new List<GameObject>();
			if (microbsList.Count == 0)
			{
				killEveryone = false;
				return;
			}


			for (int i = 0; i < 500; i++)
			{
				Destroy(microbsList[i].gameObject);	
			}
			microbsList.RemoveRange(0,500);


			/*
			else if (microbsList.Count > 500)
			{
				for (int i = 0; i < 500; i++)
				{
					microbsToKill.Add(microbsList[i].gameObject);					
				}		
			}
			else
				microbsToKill.Add(microbsList[0].gameObject);

			foreach (GameObject go in microbsToKill)
			{
				Destroy(go);
			}
			microbsToKill.Clear();
			*/
		}
	}


	
}
