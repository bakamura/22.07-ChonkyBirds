using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Could be done within the player with a tag, but would be loaded even when unneeded
public class WaterFountain : MonoBehaviour {

    [SerializeField] private GameObject _dropletParticlesPrefab = null;
    private ParticleSystem _dropletParticles = null;

    [Header("Proxy")]

    private Collider _playerCol;

    private void Start() {
        _playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        _dropletParticles = Instantiate(_dropletParticlesPrefab, Vector3.zero, Quaternion.identity).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other == _playerCol) _dropletParticles.Play();
        _playerCol.GetComponent<PlayerMovement>().IsDirty = false;
    }

    private void OnTriggerExit(Collider other) {
        if(other == _playerCol) _dropletParticles.Stop();
    }

}
