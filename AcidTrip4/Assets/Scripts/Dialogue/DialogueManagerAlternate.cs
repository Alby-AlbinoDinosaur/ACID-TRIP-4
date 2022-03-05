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

	//Whether or not to start the Dialogue on Awake function
	public bool useStartFunction = true;
	public Conversation startFuncConversation;

	public bool switchSceneOnEnd = true;
	public bool isInBattleDialogue = false;


	//What is currently being displayed:
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Animator animator;
	public Animator actor;
	public DemoEventManager manager;
	

	private Queue<string> sentences;
	private int index;
    public Dialogue[] conversation;
    private bool finished = false;

    public GameObject next;

	public GameObject dialogueCanvas; //The canvas in which the dialogue is held
	public FadeImage fadeImage;   //The image that will fade into background of the dialogue canvas (manual dialogue)
	
	//private Dialogue[] conversation;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		index = 0;

		if (useStartFunction)
        {
			BeginConversation(startFuncConversation);
		}
		else
        {
			finished = true;
        }
	}

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            
            if (!finished)
            {
                DisplayNextSentence();
				AudioManager.instance.PlayOneShot("menu_select");
            }
        }


    }

	
	//Calls StartDialogue
	//Used for buttons as well
	public void BeginConversation(Conversation converse)
	{
		dialogueCanvas.SetActive(true);
		actor.gameObject.SetActive(true);

		if (isInBattleDialogue)
		{
			fadeImage.FadeToBlack(true);
		}

		conversation = converse.conversation;
		StartDialogue(conversation[0]);
	}

//Begins displaying diaglog parameter
	private void StartDialogue (Dialogue dialogue)
	{
		finished = false;

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
	private void DisplayNextSentence ()
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
	private void EndDialogue()
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

			StartCoroutine(EndDialogueCoroutine());

		}
	}
	
	private void showSprite(string sprite)
	{
		switch (sprite)
		{
			case "Snail":
				
				nameText.color = new Color32(136,206,238,255);
				break;

			case "Leon Bradley":
				
				nameText.color = new Color32(45,119,94,255);
				break;

			case "Statistician":
				
				nameText.color = new Color32(227,58,46,255);
				break;

			case "Mans Undersnail":
				
				nameText.color = new Color32(0,161,231,255);
				break;

			case "Shredder":
				
				nameText.color = new Color32(253,253,112,255);
				break;

			case "Conche":
				nameText.color = new Color32(209,74,82,255);
				break;

			case "Coco the cowboy/cop":
				nameText.color = new Color32(128,106,78,255);
				break;

			default:
				
				nameText.color = new Color32(255,255,255,255);
				break;

		}

	}

	private IEnumerator EndDialogueCoroutine()
    {
		actor.gameObject.SetActive(false);

		if (isInBattleDialogue)
		{
			fadeImage.FadeToBlack(false);

			//Wait until it stops fading
			yield return new WaitUntil(() => !fadeImage.GetIsFading());
		}

		dialogueCanvas.SetActive(false);

		if (switchSceneOnEnd)
			manager.nextBattle();
	}

}
