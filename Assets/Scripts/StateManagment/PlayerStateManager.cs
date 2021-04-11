using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.StateManagment
{
    class PlayerStateManager : BaseStateManager
    {
        protected override void UpdateState()
        {
            
        }
    }

    public enum PlayerState
    {
        None,
        Attack,
        Move,
        Dead
    }
}
