using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Data Structure for holding dialogue information
//Name = Name on header
//Sentences = Text to display on screen
//Sentences is for storing data for a single dialogue screen
public class Dialogue {

	public string name;

	[TextArea(3, 10)]
	//? Idk what this is
	public string[] sentences;

}
