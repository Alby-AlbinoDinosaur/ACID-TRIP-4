using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	/*
		Stores Dialogue information and calls DialogueManager to start dialogue
		Is used on buttons to initiate dialogue

		dialogue is a public list of Dialogue classes: a struct of {name,text}
			see Dialogue.cs

	*/

	//********************************* DEPRECATED ******************************

	public Dialogue[] dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().BeginConversation(dialogue);
	}

}
