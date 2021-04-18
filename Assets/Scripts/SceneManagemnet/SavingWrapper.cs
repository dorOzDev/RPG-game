using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagemnet
{
    public class SavingWrapper : MonoBehaviour
    {

        private const string defaultSaveFile = "save";
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
        }

        public void Save()
        {
            GetComponent<SavingSystemComponent>().SavingSystemInstance.Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystemComponent>().SavingSystemInstance.Load(defaultSaveFile);
        }
    }
}
