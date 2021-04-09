using RPG.Characters;
using RPG.Control;
using UnityEngine;


namespace RPG.StateManagment
{
    public class AIStateManager : BaseStateManager
    {
        public EnemyState State { private set; get; }
        private EnemyState state
        {
            get
            {
                return State;
            }
            set
            {
                if (State == value) return;

                State = value;
                Controller.OnStateChanged(State);
            }
        }

        private GameObject playerGameObject;
        private Player player;
        private Enemy enemy;

        protected override void Awake()
        {
            base.Awake();
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.GetComponent<Player>();
            enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            (Controller as AIController).OnSuspicousFinishedEvent += SuspicousStateFinished;
        }

        private void OnDisable()
        {
            (Controller as AIController).OnSuspicousFinishedEvent -= SuspicousStateFinished;
        }

        protected override void UpdateState()
        {
            
            if (!enemy.IsAlive)
            {
                state = EnemyState.Dead;
                return;
            }

            else if (IsPlayerInRange() && player.IsAlive)
            {
                state = EnemyState.Attack;
            }
            // If not in range but previous state was attack than now needs to be in suspicious state
            else if (state == EnemyState.Attack)
            {
                state = EnemyState.Suspicious;
            }
        }

        private bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, playerGameObject.transform.position) <= enemy.ChaseDistance;
        }

        private void SuspicousStateFinished()
        {
            state = EnemyState.Patrol;
        }

        void Start()
        {
            //state = EnemyState.Patrol;
        }

    }

    public enum EnemyState
    {
        None,
        Patrol,
        Suspicious,
        Attack,
        Dead
    }

}
