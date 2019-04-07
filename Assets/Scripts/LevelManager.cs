﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool isGameOver;
    public Player WinningPlayer;
    public GameObject PlayerPrefab;
    public int NumberOfPlayers;


    private void Awake()
    {
        isGameOver = false;
        LoadPlayers(NumberOfPlayers);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        CheckForWinner();
    }

    private void LoadPlayers(int numberOfPlayers)
    {
        GameObject[] players = new GameObject[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = Instantiate(PlayerPrefab, new Vector3(i * 2, 1, i), Quaternion.identity);

            var player = players[i].GetComponent<Player>();
            player.PlayerNumber = i + 1;

            var controller = players[i].GetComponent<PlayerController>();
            controller.ControllerNumber = i + 1;

            var cam = players[i].GetComponentInChildren<Camera>();
            cam.rect = new Rect(GetCordFromPlayerNumber(player.PlayerNumber), GetSizeFromNumberOfPlayers(numberOfPlayers));
        }
    }

    private Vector2 GetSizeFromNumberOfPlayers(int playerNumber)
    {
        return playerNumber > 2 ? new Vector2(0.5f, 0.5f) : new Vector2(1f, 0.5f);
    }

    private Vector2 GetCordFromPlayerNumber(int playerNumber)
    {
        if (NumberOfPlayers == 2)
        {
            return playerNumber == 1 ? new Vector2(0f, 0.5f) : new Vector2(0f, 0f);
        }

        byte b = (byte)(playerNumber + 1);
        float x = (b & (1 << 0)) != 0 ? 1 : 0;
        float y = (b & (1 << 1)) != 0 ? 1 : 0;
        return new Vector2(x / 2, y / 2);
    }

    private void CheckForWinner()
    {
        var players = FindObjectsOfType<Player>();
        var livingPlayers = new List<Player>();
        foreach (var player in players)
        {
            if (player.IsAlive)
            {
                livingPlayers.Add(player);
            }
        }

        if (livingPlayers.Count <= 1)
        {
            GameOver(livingPlayers[0]);
        }
    }

    private void GameOver(Player lastPlayer)
    {
        WinningPlayer = lastPlayer;
        isGameOver = true;
    }
}
