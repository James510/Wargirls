﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] friends;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        friends = GameObject.FindGameObjectsWithTag("Friendly");
        if(enemies.Length != 0)
            for (int x = 0; x < friends.Length; x++)
                friends[x].SendMessage("EnemyList", enemies);
        if (friends.Length != 0)
            for (int x = 0; x < enemies.Length; x++)
                enemies[x].SendMessage("EnemyList", friends);
    }
}
