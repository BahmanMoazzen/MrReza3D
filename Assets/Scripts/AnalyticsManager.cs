#define DEBUG
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public static class AnalyticsManager
{
    const string EASY_SELECTED = "EasySelected";

    const string GAME_LOADED = "GameLoaded";

    const string GAME_WON = "GameWon";

    const string HARD_SELECTED = "HardSelected";

    const string INSANE_SELECTED = "InsaneSelected";

    const string NORMAL_SELECTED = "NormalSelected";

    const string REZA_CALLED = "RezaCalled";
    //const string TIME = "Time";
    const string TIME_UP = "TimeUp";
    //const string TIME_PASSED = "TimePassed";
    const string TITLE_SCREEN_LOADED = "TitleScreenLoaded";
    const string GAME_LOST = "GameLost";
    const string GAME_RESTARTED = "GameRestarted";

    public static void _EasySelected()
    {
        AnalyticsService.Instance.RecordEvent(EASY_SELECTED);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //}
        //);
    }

    public static void _GameLoaded()
    {
        AnalyticsService.Instance.RecordEvent(GAME_LOADED);
#if DEBUG
        AnalyticsService.Instance.Flush();
        Debug.Log("GameLoaded Event Recorded");
#endif        //Analytics.CustomEvent(GAME_LOADED,new Dictionary<string,object>
        //{

        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }
        //});
    }
    public static void _GameWon()
    {
        AnalyticsService.Instance.RecordEvent(GAME_WON);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(GAME_WON,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _HardSelected()
    {
        AnalyticsService.Instance.RecordEvent(HARD_SELECTED);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif

        //Analytics.CustomEvent(HARD_SELECTED,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _InsaneSelected()
    {
        AnalyticsService.Instance.RecordEvent(INSANE_SELECTED);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif

        //Analytics.CustomEvent(INSANE_SELECTED,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _NormalSelected()
    {
        AnalyticsService.Instance.RecordEvent(NORMAL_SELECTED);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(NORMAL_SELECTED,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _RezaCalled()
    {
        AnalyticsService.Instance.RecordEvent(REZA_CALLED);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(REZA_CALLED,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _TimeUp()
    {
        AnalyticsService.Instance.RecordEvent(TIME_UP);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(TIME_UP,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _TitleScreenLoaded()
    {
        AnalyticsService.Instance.RecordEvent(TITLE_SCREEN_LOADED);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(TITLE_SCREEN_LOADED,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _GameLost()
    {
        AnalyticsService.Instance.RecordEvent(GAME_LOST);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(GAME_LOST,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    public static void _GameRestarted()
    {
        AnalyticsService.Instance.RecordEvent(GAME_RESTARTED);
#if DEBUG
        AnalyticsService.Instance.Flush();
#endif
        //Analytics.CustomEvent(GAME_RESTARTED,
        //    new Dictionary<string, object>
        //{
        //    { TIME_PASSED,Time.timeSinceLevelLoad },
        //    {TIME,DateTime.Now.ToString("hh:mm:ss") }

        //});
    }
    static AnalyticsManager()
    {
        Debug.Log("AnalyticsManager initialized");
        // Static constructor to ensure the class is initialized before use
        UnityServices.InitializeAsync();
        
        AnalyticsService.Instance.StartDataCollection();


    }
}
