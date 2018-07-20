﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameLogic2 : MonoBehaviour {
	
	// Game defines
	public GameObject tilePrefab;
	public int fieldHeight;
	public int fieldWidth;
	public int deathPairs;
	public int lives;
	public List<GameObject> tileMap;
	public List<Sprite> textureList;
	public Sprite deathSprite,coverSprite;

	// privates
	private float cameraSize;
	private int needDoubles;
	

	// Use this for initialization
	void Start () 
	{
		LoadTextures();	
		ShuffleTextures(textureList);	
		GeneratePlayField();		
		FillPlayField();
	}

	// this fill playfield tiles with with images and params
	void FillPlayField()
	{
		for(int i = 0; i < needDoubles; i++)
		{
			tileMap[i].GetComponent<Tile>().front = textureList[i];
			tileMap[i].GetComponent<Tile>().back = coverSprite;
		}


		//ShuffleGo(tileMap);
	}

	// this generates playfield with params

	void GeneratePlayField()
	{	
		// calculate how much images we need
		needDoubles = (fieldHeight * fieldWidth)/2 - deathPairs*2;

		// create tile for each element in width*height 
		for (int width = 0; width < fieldWidth; width++){
			for (int height = 0; height < fieldHeight; height++){	
				
				//Debug.Log(width+ " " + height);	
				GameObject go = Instantiate(tilePrefab);

				// Change position and make it show spite
				go.transform.position = new Vector3(width-fieldWidth/2,height-fieldHeight/2,0);
				go.GetComponent<Tile>().isOpen = true;

				// Add generated to list with all objects
				tileMap.Add(go);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// load Textures from folder Resources/Sprites and add them to List with textures
    void LoadTextures()
	{
		var tex =  Resources.LoadAll("Sprites", typeof(Texture2D));
		foreach(Texture2D t in tex)
		{
			Sprite newSprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);
			textureList.Add(newSprite);
		}
	}

	// Knuth shuffle algorithm :: courtesy of Wikipedia :)
	public void ShuffleTextures(List<Sprite> tex)
	{
        
        for (int t = 0; t < tex.Count; t++)
        {
            Sprite tmp = tex[t];
            int r = Random.Range(t, tex.Count);
            tex[t] = tex[r];
            tex[r] = tmp;
        }
	}

	public void SwitchAllTiles()
	{
		foreach (GameObject go in tileMap){
			go.GetComponent<Tile>().isOpen = !go.GetComponent<Tile>().isOpen;
		}
	}

		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
	public void ShuffleGo(List<GameObject> go)
	{
        
        for (int t = 0; t < go.Count; t++)
        {
            GameObject tmp = go[t];
            int r = Random.Range(t, go.Count);
            go[t] = go[r];
            go[r] = tmp;
        }
	}
}