using UnityEngine;

public class StarGoalObj : MonoBehaviour {

    private Collider _playerCol = null; // Maybe change for a const string
    private StarCollectObject _starCheckScript = null;
    public StarCollectObject StarCheckScript { set { _starCheckScript = value; } }

    private void Start() {
        _playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other == _playerCol) _starCheckScript.star = true;
        Destroy(gameObject); // Maybe just set to invisible and disable depending on how level will be reloaded
    }

}
