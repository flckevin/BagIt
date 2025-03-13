using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quocanh.pattern;
using TMPro;
public class GameManager : QuocAnhSingleton<GameManager>
{
    [Header("GAME"), Space(10)]
    public TimeManager timeM;
    public Controller playerController;

    [Header("BLOCK_LINK TYPE"),Space(10)]
    public GameObject locker;

    [Header("LEVEL"), Space(10)]
    public LevelManager level;

    [Header("UI"), Space(10)]
    public GameMenu gameMenu;
    public GameObject canvas;
    public GameObject pauseMenu;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI itemInstruction;
    public TextMeshProUGUI levelNoDisplay;   
    public GameObject winMenu;
    public GameObject looseMenu;
}
