using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        private ParticleSystem.MainModule particleSystem;
        [SerializeField] private GameObject targetToDestroy;
        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>().main;
            particleSystem.stopAction = ParticleSystemStopAction.Callback;
        }

        public void OnParticleSystemStopped()
        {
            if (targetToDestroy == null)
            {
                Destroy(targetToDestroy);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
