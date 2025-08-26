using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager
{
    const string EASY_SELECTED = "EasySelected";

    const string GAME_LOADED = "GameLoaded";

    const string GAME_WON = "GameWon";

    const string HARD_SELECTED = "HardSelected";

    const string INSANE_SELECTED = "InsaneSelected";

    const string NORMAL_SELECTED = "NormalSelected";

    const string REZA_CALLED = "RezaCalled";

    const string TIME_UP = "TimeUp";

    const string TITLE_SCREEN_LOADED = "TitleScreenLoaded";
    const string GAME_LOST = "GameLost";
    const string GAME_RESTARTED = "GameRestarted";
    
    public static void _EasySelected()
    {
        Analytics.CustomEvent(EASY_SELECTED);
    }
    
    public static void _GameLoaded()
    {
        Analytics.CustomEvent(GAME_LOADED);
    }
    public static void _GameWon()
    {
        Analytics.CustomEvent(GAME_WON);
    }
    public static void _HardSelected()
    {
        Analytics.CustomEvent(HARD_SELECTED);
    }
    public static void _InsaneSelected()
    {
        Analytics.CustomEvent(INSANE_SELECTED);
    }
    public static void _NormalSelected()
    {
        Analytics.CustomEvent(NORMAL_SELECTED);
    }
    public static void _RezaCalled()
    {
        Analytics.CustomEvent(REZA_CALLED);
    }
    public static void _TimeUp()
    {
        Analytics.CustomEvent(TIME_UP);
    }
    public static void _TitleScreenLoaded()
    {
        Analytics.CustomEvent(TITLE_SCREEN_LOADED);
    }
    public static void _GameLost()
    {
        Analytics.CustomEvent(GAME_LOST);
    }
    public static void _GameRestarted()
    {

        Analytics.CustomEvent(GAME_RESTARTED);
    }
    static AnalyticsManager()
    {
        Debug.Log("AnalyticsManager initialized");
        // Static constructor to ensure the class is initialized before use
        UnityServices.InitializeAsync();

        AnalyticsService.Instance.StartDataCollection();

    }
}
