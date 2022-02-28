using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
	/*
		Dialogue Manager displays text on screen after being given a list of dialog from BeginConversation(Dialogue dialogue)
		Requires 2 text objests to display text and an animator for the text box

	*/
public class DialogueManagerAlternate : MonoBehaviour {



//What is currently being displayed:
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Animator animator;
	public Animator actor;
	public DemoEventManager manager;
	public AudioManager soundManager;

	private Queue<string> sentences;
	private int index;
    public Dialogue[] conversation;
    private bool finished = false;

    public GameObject next;
	
	//private Dialogue[] conversation;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		index = 0;
        StartDialogue(conversation[0]);
	}

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            if (!finished)
            {
                DisplayNextSentence();
				soundManager.Play("menu_hover");
            }
        }


    }

//Called by a DialogueTrigger and stores dialog information into conversation
//Calls StartDialogue
	public void BeginConversation(Dialogue[] dialogue)
	{
		conversation = dialogue;
		StartDialogue(conversation[0]);
	}

//Begins displaying diaglog parameter
	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;
		actor.SetTrigger(dialogue.name);

		showSprite(dialogue.name);

		


		sentences.Clear();

		
		

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

//Called by continue button to move onto next text
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

//Displays dialog text letter by letter (1 letter per cyce)
	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

//Is called everytime StartDialogue() is called
//Hides Dialog Box if conversation is done
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
            finished = true;
			manager.nextBattle();

		}
	}
	
	void showSprite(string sprite)
	{
		switch (sprite)
		{
			case "Snail":
				
				nameText.color = new Color32(0,0,255,255);
				break;

			case "Leon Bradley":
				
				nameText.color = new Color32(0,255,0,255);
				break;

			case "Statistician":
				
				nameText.color = new Color32(255,0,0,255);
				break;

			case "Mans Undersnail":
				
				nameText.color = new Color32(255,0,255,255);
				break;

			case "Shredder":
				
				nameText.color = new Color32(255,200,0,255);
				break;

			default:
				
				nameText.color = new Color32(255,255,255,255);
				break;

		}

	}



}
