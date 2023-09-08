using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level {
  [SerializeField]
  private int level;

  [SerializeField]
  private int experience;

  [SerializeField]
  private int experienceCap;

  [SerializeField]
  private List<LevelRange> levelRanges = new() {
    new LevelRange { start = 1, end = 10, experienceCapIncrease = 100 },
    new LevelRange { start = 11, end = 20, experienceCapIncrease = 200 },
    new LevelRange { start = 21, end = 30, experienceCapIncrease = 300 },
    new LevelRange { start = 31, end = 40, experienceCapIncrease = 400 }
  };

  public Level(int startLevel = 1) {
    level = startLevel;
    experienceCap = levelRanges[0].experienceCapIncrease;
  }

  public int GetLevel() {
    return level;
  }

  public int GetExperience() {
    return experience;
  }

  public void GainExperience(int amount) {
    experience += amount;
    CheckLevelUp();
  }

  private void CheckLevelUp() {
    if (experience < experienceCap) return;

    level++;

    var levelRange = levelRanges.Find(range => range.start <= level && range.end >= level);
    if (levelRange != null) experienceCap += levelRange.experienceCapIncrease;
  }

  [Serializable]
  private class LevelRange {
    public int end;
    public int experienceCapIncrease;
    public int start;
  }
}