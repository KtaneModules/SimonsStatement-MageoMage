using System;
using System.Collections;
using UnityEngine;
using KModkit; //gets edgework and stuff

public class simonsStatementScript : MonoBehaviour {

	public KMAudio audio; //audio script
	public KMBombInfo bomb; //bomb info script

    //for logging:
	static int moduleIdCounter = 1;
	int modId;
	private bool modSolved; //default is false

    public KMSelectable[] buttons;
	public MeshRenderer[] displayLEDs;
	public TextMesh symbol;
	public Color[] colors; //RGBY

    private bool colorblind;
	private int stage;
	private int step;
	private readonly string[] gates = {"AND", "OR", "XOR", "NAND", "NOR", "XNOR", "IMP", "IMPBY"};
	private readonly string[] symbols = {"∧", "∨", "⊻", "|", "↓", "↔", "→", "←"};
	private readonly string[] clrNames = {"red", "green", "blue", "yellow"};
	private int gate; //gate between indicators
	private int clr1; //left indicator
	private int clr2; //right indicator

    void Awake(){
		modId = moduleIdCounter++;

		foreach (KMSelectable button in buttons){
			KMSelectable temp = button;
			button.OnInteract += delegate(){Press(temp); return false;};
		}
		
		//colorblind = GetComponent<KMColorblindMode>().ColorblindModeActive;
	}

	void Press(KMSelectable button){

		Debug.LogFormat("[Simon's Statement #{0}] {1} has been pressed", modId, button.name);
		//turn on light
		StopAllCoroutines();
		foreach (KMSelectable btn in buttons) btn.GetComponent<Light>().enabled = false;
		StartCoroutine(GlowCoroutine(button));
		//play sound
		//check whether correct
		//make animation
	}

	private IEnumerator GlowCoroutine(KMSelectable button) {
			yield return new WaitForSeconds(0.05f);
			button.GetComponent<Light>().enabled = true;
			yield return new WaitForSeconds(0.5f);
			button.GetComponent<Light>().enabled = false;
		}

	void Start () {
		gate = UnityEngine.Random.Range(0,8);
		symbol.text = symbols[gate];

		clr1 = UnityEngine.Random.Range(0,4);
		clr2 = UnityEngine.Random.Range(0,3);
		clr2 += clr1 <= clr2 ? 1 : 0;

		displayLEDs[0].material.color = colors[clr1];
		displayLEDs[1].material.color = colors[clr2];

		Debug.LogFormat("[Simon's Statement #{0}] The expression is: {1} {2} {3}", modId, clrNames[clr1], gates[gate], clrNames[clr2]);

		stage = 1;
		step = 1;

	}

}