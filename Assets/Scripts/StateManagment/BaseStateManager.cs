
using RPG.Control;
using UnityEngine;


namespace RPG.StateManagment
{
    public abstract class BaseStateManager : MonoBehaviour
    {

        protected BaseController Controller { get; private set; }
        protected virtual void Awake()
        {
            Controller = GetComponent<BaseController>();
        }

        protected abstract void UpdateState();


        // Update is called once per frame
        void Update()
        {
            UpdateState();
        }
    }

}
