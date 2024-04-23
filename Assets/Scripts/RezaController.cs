using UnityEngine;
using UnityEngine.Events;

public class RezaController : MonoBehaviour
{
    public static event UnityAction OnGameWon;
    [SerializeField] GameObject _movie;
    // Start is called before the first frame update
    void Start()
    {
        _movie.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            _movie.SetActive(true);
            OnGameWon?.Invoke();
        }
    }
}
