using RPG.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            A,
            B,
            C,
            D,
            E
        }


        private string PLAYER_TAG = "Player";
        [SerializeField] private int scene = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destination;
        [SerializeField] private DestinationIdentifier identifier;

        private GameObject player;

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == PLAYER_TAG)
            {
                StartCoroutine(Transition());
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            print(destination);
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = spawnPoint.position;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private IEnumerator Transition()
        {
            yield return SceneManager.LoadSceneAsync(scene);
        }

        private Portal GetOtherPortal()
        {
            return null;
        }
    }

}