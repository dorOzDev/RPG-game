using RPG.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
            

        }

        private void SpawnPlayerAtDestination()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            
            Vector3 position = GetDestinationPosition();

            player.transform.position = position;
        }

        private Vector3 GetDestinationPosition()
        {
            Portal portal = GetDestinationPortal();

            if (portal == null) return spawnPoint.position;

            return portal.spawnPoint.position;
        }

        private Portal GetDestinationPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach(Portal portal in portals)
            {
                if (portal.identifier == destination)
                    return portal;
            }

            return null;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(this);
            yield return SceneManager.LoadSceneAsync(scene);
            SpawnPlayerAtDestination();
            Destroy(this);
        }

        private Portal GetOtherPortal()
        {
            return null;
        }
    }

}