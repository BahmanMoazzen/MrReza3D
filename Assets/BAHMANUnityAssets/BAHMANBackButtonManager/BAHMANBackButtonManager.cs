/*
 BAHMAN Back Button Handler V.1.1
this module handles back button on mobile devices and Escape button on desktops
It should be loaded in the very first scene because it won't be destroyed on load.
use the prefab provided in this folder to modify the look of the message box.
*/

using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class BAHMANBackButtonManager : MonoBehaviour
{
    //events to fire 
    public static event UnityAction OnBackButtonMenuShowed;
    public static event UnityAction OnBackButtonMenuHide;

    [Tooltip("check this if you need just trigger the events")]
    [SerializeField] bool _SilentMode = false;

    [Tooltip("the scene name of the home button")]
    [SerializeField] string _HomeSceneName;

    [Tooltip("the default panel to show when back button pressed")]
    [SerializeField] GameObject _BackPanel;

    //the menu item to insert back button prefab
    const string _prefabName = "BAHMANBackButtonManager";
    //[MenuItem("BAHMAN Unity Assets/Create Back Button Manager", false, 2)]
    //static void CreateCustomGameObject(MenuCommand menuCommand)
    //{

    //    GameObject newGo = Instantiate(Resources.Load<GameObject>(_prefabName), Vector3.zero, Quaternion.identity);

    //    //GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
    //    //Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
    //    newGo.name = _prefabName;
    //    Selection.activeObject = newGo;

    //}


    void Awake()
    {
        if (_BackPanel == null)
        {
            Debug.LogError("Back Button Manager: Back Panel Not Specified!");
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void OnEnable()
    {

        _BackPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_BackPanel.activeInHierarchy)
            {
                if (!_SilentMode) _BackPanel.SetActive(false);
                OnBackButtonMenuHide?.Invoke();
            }
            else
            {
                if (!_SilentMode) _BackPanel.SetActive(true);
                OnBackButtonMenuShowed?.Invoke();
            }
        }
    }
    public void _ShowMenu()
    {
        _BackPanel.SetActive(true);
        OnBackButtonMenuShowed?.Invoke();
    }
    public void _Exit()
    {
        Application.Quit();
    }
    public void _Home()
    {
        if (_HomeSceneName != string.Empty)
        {
            _BackPanel.SetActive(false);
            BAHMANLoadingManager._INSTANCE._LoadScene(_HomeSceneName);
        }
        else
        {
            Debug.LogError("Back Button Manager: Home Scene Not Specified!");
        }
        //LoadingManager._INSTANCE._LoadScene(_HomeSceneName);
    }

}

