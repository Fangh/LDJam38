using UnityEngine;
using System.Collections;

public class MicrobCount : MonoBehaviour
{	
	public static MicrobCount Instance = null;
	public int NBMicrob = 0;
	public int NbMicrobBirth = -2;
	public string[] names = new string[]{"Benjamin","Chloé","Yanis","Adrien","Alexis","Guillaume","Louna","Louise","Erwan","Bastien","Katell","Bastien","Maelys","Zacharis","Maxence","Alexis","Davy","Noémie","Kimberley","Dylan","Cédric","Adam","Ambre","Timothée","Dimitri","Amélie","Kimberley","Lina","Simon","Rémi","Esteban","Maxime","Kimberley","Killian","Luna","Chaïma","Clotilde","Ambre","Valentine","Syrine","Samuel","Louna","Nolan","Éléna","Lily","Laura","Timothée","Hugo","Victor","Cédric","Célia","Marion","Justine","Maxime","Léonie","Timothée","Élouan","Erwan","Lucas","Inès","Lutécia","Louis","Davy","Alexandra","Guillaume","Jeanne","Yüna","Maïlé","Davy","Lutécia","Anthony","Amine","Jasmine","Tristan","Clotilde","Samuel","Jérémy","Élouan","Élise","Éloïse","Nina","Julien","Gabin","Rose","Jérémy","Kimberley","Maxime","Anthony","Solene","Lucie","Julien","Jordan","Ambre","Clément","Nicolas","Pauline","Loevan","Marion","Cédric","Noë","Célia","Valentin","Simon","Florian","Élisa","Anthony","Jeanne","Margaux","Nathan","Yasmine","Amine","Mathéo","Capucine","Gabriel","Thibault","Lauriane","Célia","Constant","Maryam","Samuel","Romain","Gaspard","Tristan","Mohamed","Alexis","Émile","Malik","Maryam","Salomé","Pierre","Dylan","Maxime","Luna","Guillaume","Romane","Dorian","Alexis","Juliette","Dorian","Gabin","Quentin","Lisa","Loevan","Jade","Dylan","Florian","Adam","Nathan","Élisa","Timothée","Marwane","Clémence","Sara","Juliette","Julien","Charlotte","Anthony","Dorian","Syrine","Maelys","Eva","Dylan","Loane","Marie","Adrian","Dorian","Clotilde","Colin","Gilbert","Lilou","Jade","Maxime","Mélissa","Océane","Lamia","Alexandre","Éloïse","Lorenzo","Alexandre","Ambre","Alexandre","Victor","Dorian","Marine","Elsa","Elsa","Mehdi","Louise","Yanis","Esteban","Émilie","Noë","Rosalie","Loevan","Chaïma","Nolan","Evan","Kevin","Zoé"};

	void Awake()
	{
		Instance = this;
	}

	public void AddMicrob()
	{
		NBMicrob++;
		NbMicrobBirth++;
	}

	public void RemoveMicrob()
	{
		NBMicrob--;		
	}

	public string GetRandomName()
	{
		return names[Random.Range(0, names.Length-1)];
	}

	
}
