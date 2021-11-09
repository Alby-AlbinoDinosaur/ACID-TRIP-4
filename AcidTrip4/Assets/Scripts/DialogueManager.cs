using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;
	private int index;
	private Dialogue[] conversation;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		index = 0;
	}

	public void BeginConversation(Dialogue[] dialogue)
	{
		conversation = dialogue;
		StartDialogue(conversation[0]);
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		if (index + 1 < conversation.Length)
		{
			index++;
			StartDialogue(conversation[index]);
		}
		else
		{
			animator.SetBool("IsOpen", false);
			index = 0;
		}
	}

}
