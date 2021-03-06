using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
/*
Dialogue Manager displays text on screen after being given a list of dialog from BeginConversation(Dialogue dialogue)
Requires 2 text objests to display text and an animator for the text box

*/
public class DialogueManagerAlternate : MonoBehaviour {

	//Whether or not to start the Dialogue on Awake function
	public bool useStartFunction = true;
	public Conversation startFuncConversation;
	public bool isInBattleDialogue = false;


	//What is currently being displayed:
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Animator animator;
	public Animator actor;
	public DemoEventManager manager;
	

	private Queue<string> sentences;
	private int index;
	private Dialogue[] conversation;
    private bool finished = false;

    public GameObject next;

	public GameObject dialogueCanvas; //The canvas in which the dialogue is held
	public FadeImage fadeImage;   //The image that will fade into background of the dialogue canvas (manual dialogue)

	//Event for when conversation begins -------------------------------------------
	[System.Serializable]
	public class ConversationBeginEvent : UnityEvent { }

	[FormerlySerializedAs("onConversationBegin")]
	[SerializeField]
	private ConversationBeginEvent onConversationBegin = new ConversationBeginEvent();
	//--------------------------------------------------------------------------------

	//Event for when conversation finishes -------------------------------------------
	[System.Serializable]
	public class ConversationEndEvent : UnityEvent { }

	[FormerlySerializedAs("onConversationEnd")]
	[SerializeField]
	private ConversationEndEvent onConversationEnd = new ConversationEndEvent();
	//--------------------------------------------------------------------------------

	



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
		//Invoke onConversationBegin events
		onConversationBegin.Invoke();
		StartCoroutine(BeginConvCoroutine(converse));
	}

	//Because bruh moment because gameobject setactives dont actually activate gameobjects this frame
	private IEnumerator BeginConvCoroutine(Conversation converse)
    {
		dialogueCanvas.SetActive(true);
		actor.gameObject.SetActive(true);

		//wait a frame for objects to activate
		yield return new WaitForEndOfFrame();

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

		string[] soundCheck = dialogue.name.Split(' ');
		
		if(soundCheck[0] == "playsound"){
			AudioManager.instance.PlayOneShot(soundCheck[1]);
			DisplayNextSentence();
			
		}
		else{
			string specialName = setName(dialogue.name);
			showSprite(specialName);
			if(dialogue.name.Length > 0){
				
				actor.SetTrigger(dialogue.name);
			}

			


			sentences.Clear();

			
			

			foreach (string sentence in dialogue.sentences)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
		}
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
			case "Mysterious Shadowy Figure":
				
				nameText.color = new Color32(69,61,81,255);
				fadeImage.FadeToBlack(true);
				break;

			case "Neck R.O. Manser":
				nameText.color = new Color32(69,61,81,255);
				fadeImage.FadeToBlack(true);
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
//Finds correct names for specific sprites
	private string setName(string name){
		switch (name)
		{
			case "Snail Sad":
				nameText.text = "Snail";
				return "Snail";
				break;
			case "Snail Down":
				nameText.text = "Snail";
				return "Snail";
				break;
			case "Snail Think":
				nameText.text = "Snail";
				return "Snail";
				break;
			case "Snail Buff":
				nameText.text = "Snail";
				return "Snail";
				break;

			case "Mans1":
				nameText.text = "Mans Undersnail";
				return "Mans Undersnail";
				break;
			case "Mans2":
				nameText.text = "Mans Undersnail";
				return "Mans Undersnail";
				break;
			case "Mans3":
				nameText.text = "Mans Undersnail";
				return "Mans Undersnail";
				break;
			case "Mans Reveal":
				nameText.text = "Mans Undersnail";
				return "Mans Undersnail";
				break;

			case "Coco Punch":
				nameText.text = "";
				return "";
				break;
			
			default:
				nameText.text = name;
				return name;
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

		//Invoke onConversationEnd events
		onConversationEnd.Invoke();
	}

}
