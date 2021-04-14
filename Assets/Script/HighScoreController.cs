﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
  private Transform entryContainer;
  private Transform entryTemplate;
  private List<Transform> highscoreEntryTransformList;

  private void Awake()
  {
    entryContainer = transform.Find("HighScoreContainer");
    entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

    entryTemplate.gameObject.SetActive(false);

    //PlayerPrefs.DeleteKey("highscoreTable");
    string jsonString = PlayerPrefs.GetString("highscoreTable");
    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

    if (highscores == null)
    {
      // There's no stored table, initialize
      Debug.Log("Initializing table with default values...");
      AddHighscoreEntry(1000, "CMK");
      AddHighscoreEntry(21, "JOE");
      AddHighscoreEntry(31, "DAV");
      AddHighscoreEntry(23, "CAT");
      AddHighscoreEntry(54, "MAX");
      AddHighscoreEntry(5, "AAA");
      // Reload
      jsonString = PlayerPrefs.GetString("highscoreTable");
      highscores = JsonUtility.FromJson<Highscores>(jsonString);
    }

    Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    //AddHighscoreEntry(142, "JEX");
    jsonString = PlayerPrefs.GetString("highscoreTable");
    highscores = JsonUtility.FromJson<Highscores>(jsonString);

    // Sort entry list by Score
    for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
    {
      for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
      {
        if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
        {
          // Swap
          HighscoreEntry tmp = highscores.highscoreEntryList[i];
          highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
          highscores.highscoreEntryList[j] = tmp;
        }
      }
    }

    if(highscores.highscoreEntryList.Count < 10)
		{
      int lastHighScore = highscores.highscoreEntryList[highscores.highscoreEntryList.Count - 1].score;
      PlayerPrefs.SetInt("HighScore", lastHighScore);
      Debug.Log("Lowest High Score is: " + PlayerPrefs.GetInt("HighScore"));
    }
    else {
      int tenthHighScore = highscores.highscoreEntryList[9].score;
      PlayerPrefs.SetInt("HighScore", tenthHighScore);
      Debug.Log("Tenth High Score is: " + PlayerPrefs.GetInt("HighScore"));
    }

    highscoreEntryTransformList = new List<Transform>();
    if (highscores.highscoreEntryList.Count < 10)
    {
      foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
      {
        CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
      }
    }
    else
		{
      for (int i = 0; i < 10; i++)
      {
        HighscoreEntry highscoreEntry = highscores.highscoreEntryList[i];
        CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
      }
    }
  }

  private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
  {
    float templateHeight = 31f;
    Transform entryTransform = Instantiate(entryTemplate, container);
    RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
    entryRectTransform.anchoredPosition = new Vector2(-10, -templateHeight * transformList.Count - 30);
    entryTransform.gameObject.SetActive(true);

    int rank = transformList.Count + 1;
    string rankString;
    switch (rank)
    {
      default:
        rankString = rank + ""; break;
    }

    entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

    int score = highscoreEntry.score;

    entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

    string name = highscoreEntry.name;
    entryTransform.Find("NameText").GetComponent<Text>().text = name;

    // Set background visible odds and evens, easier to read
    //entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

    // Highlight First
    if (rank == 1)
    {
      entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;
      entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
      entryTransform.Find("NameText").GetComponent<Text>().color = Color.green;
    }

    transformList.Add(entryTransform);
  }

  public static void AddHighscoreEntry(int score, string name)
  {
    // Create HighscoreEntry
    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

    // Load saved Highscores
    string jsonString = PlayerPrefs.GetString("highscoreTable");
    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

    if (highscores == null)
    {
      // There's no stored table, initialize
      highscores = new Highscores()
      {
        highscoreEntryList = new List<HighscoreEntry>()
      };
    }

    // Add new entry to Highscores
    highscores.highscoreEntryList.Add(highscoreEntry);

    // Save updated Highscores
    string json = JsonUtility.ToJson(highscores);
    PlayerPrefs.SetString("highscoreTable", json);
    PlayerPrefs.Save();
  }


  private class Highscores
  {
    public List<HighscoreEntry> highscoreEntryList;
  }

  /*
   * Represents a single High score entry
   * */
  [System.Serializable]
  private class HighscoreEntry
  {
    public int score;
    public string name;
  }
}


