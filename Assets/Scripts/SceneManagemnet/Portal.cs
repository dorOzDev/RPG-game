using RPG.Characters;
using RPG.Movement;
using RPG.SceneManagemnet;
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
        [SerializeField] private int sceneIndex = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destination;
        [SerializeField] private DestinationIdentifier identifier;
        [SerializeField] private Fader fader;

        private GameObject player;

        private void Awake()
        {

        }

        private void Start()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if(sceneIndex < 0)
            {
                Debug.LogError("Scene index to load has not been set yet");
                return;
            }

            if(other.tag == PLAYER_TAG)
            {
                StartCoroutine(Transition());       
            }
        }

        private void SpawnPlayerAtDestination()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            Mover mover = player.GetComponent<Mover>();

            mover.DisableMover();
            
            Vector3 position = GetDestinationPosition();

            player.transform.position = position;

            mover.EnableMover();
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

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(this);
            yield return fader.FadeOutEffect();

            SaveScene();
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            
            LoadScene();
            SpawnPlayerAtDestination();
            SaveScene();
            yield return fader.FadeInEffect();
            Destroy(this);
        }

        private void SaveScene()
        {
            SavingWrapper savingWrapper = GetSavingWrapper();
            savingWrapper.Save();
        }

        private void LoadScene()
        {
            SavingWrapper savingWrapper = GetSavingWrapper();
            savingWrapper.Load();
        }


        private SavingWrapper GetSavingWrapper() 
        {
            return FindObjectOfType<SavingWrapper>();
        }
    }

}