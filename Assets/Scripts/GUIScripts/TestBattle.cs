﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using GSP.Core;
/*
 * DO NOT EDIT THIS FILE FOR GUIDELINES. IT WILL BE REPLACED AND DELETED WHEN REAL SYSTEM IS PUT IN.
 * or do, I just value your time.
 */

public class TestBattle : MonoBehaviour 
{
	bool player1Turn;
	Text player1Name;
	Text player2Name;
	Text player1Health;
	Text player2Health;
	Text fightBoxText;
	RectTransform player1Bar;
	RectTransform player2Bar;
	int linesOfText;
	Vector3 originalPos;
	List<string> verbs;

	// Use this for initialization
	void Start () 
	{
		player1Turn = true;
		player1Name = GameObject.Find ("Battler1Name").GetComponent<Text> ();
		player2Name = GameObject.Find ("Battler2Name").GetComponent<Text> ();
		player1Health = GameObject.Find ("Battler1Health").GetComponent<Text> ();
		player2Health = GameObject.Find ("Battler2Health").GetComponent<Text> ();
		player1Bar = GameObject.Find ("Battler1HealthLeft").GetComponent<RectTransform> ();
		player2Bar = GameObject.Find ("Battler2HealthLeft").GetComponent<RectTransform> ();
		fightBoxText = GameObject.Find ("FightText").GetComponent<Text> ();
		fightBoxText.text = "The battlers square off!";
		linesOfText = 1;
		originalPos = fightBoxText.transform.position;
		verbs = new List<string>();
		verbs.Add ("attacked");
		verbs.Add ("retaliated against");
		verbs.Add ("hammered");
		verbs.Add ("struck");
		verbs.Add ("feinted at");
		verbs.Add ("lashed at");
		InvokeRepeating ("Fight", 2, 1.2f);
	}
	
	void Fight()
	{
		int randomVerb = Random.Range (0, verbs.Count);
		if(player1Turn)
		{
			int randomAtk = Random.Range (10, 20);
			int randomDef = Random.Range (13, 25);
			int randomDamage = randomAtk - randomDef;
			if(randomDamage < 0)
			{
				randomDamage = 1;
			}
			fightBoxText.text += "\n" + player1Name.text + " " + verbs [randomVerb] + " " +
				player2Name.text + " for " + randomDamage;
			int tempHealth = int.Parse(player2Health.text) - randomDamage;
			float test = (float)tempHealth/20.0f;
			player2Health.text = tempHealth.ToString();
			player2Bar.localScale = new Vector3(test, 1f, 1f);
			player1Turn = false;
		}
		else
		{
			int randomAtk = Random.Range (9, 18);
			int randomDef = Random.Range (8, 16);
			int randomDamage = randomAtk - randomDef;
			if(randomDamage < 0)
			{
				randomDamage = 1;
			}
			fightBoxText.text += "\n" + player2Name.text + " " + verbs [randomVerb] + " " +
				player1Name.text + " for " + randomDamage;
			int tempHealth = int.Parse(player1Health.text) - randomDamage;
			float test = (float)tempHealth/20.0f;
			player1Health.text = tempHealth.ToString();
			player1Bar.localScale = new Vector3(test, 1.0f, 1.0f);
			player1Turn = true;
		}
		linesOfText++;
		if(linesOfText > 8)
		{
			fightBoxText.transform.position = new Vector3(
				fightBoxText.transform.position.x,
				fightBoxText.transform.position.y + 16);
		}
		if(int.Parse(player1Health.text) <= 0)
		{
			fightBoxText.transform.position = originalPos;
			fightBoxText.text = player2Name.text + " wins!";
			Invoke("EndBattle", 1.5f);
		}
		else if(int.Parse(player2Health.text) <= 0)
		{
			fightBoxText.transform.position = originalPos;
			fightBoxText.text = player1Name.text + " wins!";
			Invoke("EndBattle", 1.5f);
		}
	}

	private void EndBattle()
	{
		GameMaster.Instance.LoadLevel (GameMaster.Instance.BattleMap.ToString ());
	} //end EndBattle
}
