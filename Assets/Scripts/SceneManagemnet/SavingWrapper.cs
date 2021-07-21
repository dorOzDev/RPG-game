using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagemnet
{
    public class SavingWrapper : MonoBehaviour
    {

        [SerializeField] private SavingSystem savingSystemInstace;
        [SerializeField] private Fader fader;

        private const string defaultSaveFile = "save";

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            fader.FadeOutImmidiate();
            yield return savingSystemInstace.LoadLastScene(defaultSaveFile);
            yield return fader.FadeInEffect();
        }

        void Update()
        {

            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Save()
        {
            savingSystemInstace.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystemInstace.Load(defaultSaveFile);
        }

        public void Delete()
        {
            savingSystemInstace.Delete(defaultSaveFile);
        }
    }
}
