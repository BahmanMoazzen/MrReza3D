/*
 * Message box Version 1.0
 * This module shows timed messages on the screen
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


// Message structure to show on screen
class MessageStruct
{
    public Color _color;
    public string _message;
    public float _interval;

}


public class BAHMANMessageBoxManager : MonoBehaviour
{

    // instance to call message box manager
    public static BAHMANMessageBoxManager _INSTANCE;

    //public procedures to call


    // insert message by single text
    public void _ShowMessage(string iMessage)
    {
        MessageStruct messageStructure = new MessageStruct();
        messageStructure._color = _DefaultColor;
        messageStructure._interval = _DefaultHideIntervalTime;
        messageStructure._message = iMessage;
        _messageQueue.Enqueue(messageStructure);


    }
    // insert message by single text and color
    public void _ShowMessage(string iMessage, Color iColor)
    {
        MessageStruct messageStructure = new MessageStruct();
        messageStructure._color = iColor;
        messageStructure._message = iMessage;
        messageStructure._interval = _DefaultHideIntervalTime;
        _messageQueue.Enqueue(messageStructure);


    }
    // insert message by single text and color and time
    public void _ShowMessage(string iMessage, Color iColor, float iInterval)
    {
        MessageStruct messageStructure = new MessageStruct();
        messageStructure._color = iColor;
        messageStructure._message = iMessage;
        messageStructure._interval = iInterval;
        _messageQueue.Enqueue(messageStructure);


    }

    #region private

    const string _prefabName = "BAHMANMessageBox";
    //[MenuItem("BAHMAN Unity Assets/Create Message Box Manager", false, 3)]
    //static void CreateCustomGameObject(MenuCommand menuCommand)
    //{

    //    GameObject newGo = Instantiate(Resources.Load<GameObject>(_prefabName), Vector3.zero, Quaternion.identity);

    //    //GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
    //    //Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
    //    newGo.name = _prefabName;
    //    Selection.activeObject = newGo;

    //}

    // message text placeholder
    [SerializeField] Text _MessageText;

    // message panel to show or hide
    [SerializeField] GameObject _MessagePanel;

    // default hide interval time
    [SerializeField][Range(0, 10)] float _DefaultHideIntervalTime = 2f;


    // default hide interval time
    [SerializeField] Color _DefaultColor = Color.black;


    // message queue for storing data
    Queue<MessageStruct> _messageQueue;


    void Awake()
    {

        if (_INSTANCE == null)
        {
            _INSTANCE = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {

        StartCoroutine(_startupRoutine());
    }
    IEnumerator _startupRoutine()
    {
        yield return 0;
        _messageQueue = new Queue<MessageStruct>();
        _MessagePanel.SetActive(false);
        _MessageText.text = string.Empty;
        StartCoroutine(_MessageManager());
        yield return 0;
    }
    IEnumerator _MessageManager()
    {
        while (true)
        {
            if (_messageQueue.Count > 0)
            {
                var message = _messageQueue.Dequeue();
                if (message._interval < 0)
                {
                    if (_messageQueue.Count <= 0)
                    {
                        _MessagePanel.SetActive(true);
                        _MessageText.text = message._message;
                        _MessageText.color = message._color;
                    }

                }
                else
                {
                    _MessagePanel.SetActive(true);
                    _MessageText.text = message._message;
                    _MessageText.color = message._color;
                    yield return new WaitForSeconds(message._interval);
                    _MessagePanel.SetActive(false);
                    _MessageText.text = string.Empty;
                }
            }
            yield return 0;
        }

    }
    #endregion

}


