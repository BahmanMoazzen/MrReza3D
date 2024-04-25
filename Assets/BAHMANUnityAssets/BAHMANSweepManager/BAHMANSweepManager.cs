using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum SweepDirections { Left, Right, Up, Down };
public class BAHMANSweepManager : MonoBehaviour
{
    [SerializeField] float _MaximumXDrag = 200f, _MaximumYDrag = 100f,_DragDeadTime = .8f;
    /// <summary>
    /// fires when sweep completed;
    /// </summary>
    public static event UnityAction<SweepDirections> OnSweep;
    /// <summary>
    /// fires when dragging started
    /// </summary>
    public static event UnityAction<Vector3> OnStartDragging;
    /// <summary>
    /// fires when dragging continues
    /// </summary>
    public static event UnityAction<Vector3> OnDragging;
    /// <summary>
    /// fires when dragging ended
    /// </summary>
    public static event UnityAction<Vector3> OnEndDragging;
    /// <summary>
    /// fires when a complete drag occured
    /// </summary>
    public static event UnityAction<Vector3, Vector3, float> OnCompleteDragOccured;


    bool _isDragging = false;
    float _dragDuration = 0;
    Vector3 _dragStartPosition = new Vector3(0, 0, 0);
    Vector3 _dragEndPosition = new Vector3(0, 0, 0);
    [SerializeField] SweepDirections _Direction;
    /// <summary>
    /// interprete the drag which is occured
    /// </summary>
    void _describeAction()
    {
        if (_dragDuration > _DragDeadTime) return;
        OnCompleteDragOccured?.Invoke(_dragStartPosition, _dragEndPosition, _dragDuration);
        float xDelta = _dragStartPosition.x - _dragEndPosition.x;
        float yDelta = _dragStartPosition.y - _dragEndPosition.y;
        if ((Mathf.Abs(xDelta) > _MaximumXDrag)
            || (Mathf.Abs(yDelta) > _MaximumYDrag))
        {

            if (Mathf.Abs(xDelta) > Mathf.Abs(yDelta))
            {
                // move along the x axis
                if (xDelta > 0)
                {
                    OnSweep?.Invoke(SweepDirections.Left);
                    _Direction = SweepDirections.Left;
                    Debug.Log("Sweep Left");
                }
                else if (xDelta < 0)
                {
                    Debug.Log("Sweep Right");
                    OnSweep?.Invoke(SweepDirections.Right);
                    _Direction = SweepDirections.Right;
                }
                else
                {
                    Debug.Log("No Magnetiude");
                }
            }
            else
            {
                // move along the y axis
                if (yDelta > 0)
                {
                    OnSweep?.Invoke(SweepDirections.Down);
                    _Direction = SweepDirections.Down;
                    Debug.Log("Sweep Down");
                }
                else if (yDelta < 0)
                {
                    Debug.Log("Sweep Up");
                    OnSweep?.Invoke(SweepDirections.Up);
                    _Direction = SweepDirections.Up;
                }
                else
                {
                    Debug.Log("No Magnetiude");
                }
            }
        }
        else
        {
            Debug.Log("No Magnetiude");
        }
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (!_isDragging)
            {
                _dragDuration = 0;
                _isDragging = true;
                _dragStartPosition = Input.mousePosition;
                OnStartDragging?.Invoke(_dragStartPosition);
            }
            else
            {
                _dragDuration += Time.deltaTime;
                OnDragging?.Invoke(Input.mousePosition);

            }
            Debug.Log("Dragging.");
        }
        else
        {
            if (_isDragging)
            {
                _isDragging = false;
                Debug.Log("Drag Duration= " + _dragDuration);
                _dragEndPosition = Input.mousePosition;
                Debug.Log("Drag Magnetitude= " + Vector3.Distance(_dragStartPosition, _dragEndPosition));
                OnEndDragging?.Invoke(_dragEndPosition);
                _describeAction();
            }
            else
            {

                Debug.Log("IDLE");
            }
        }

    }
}
