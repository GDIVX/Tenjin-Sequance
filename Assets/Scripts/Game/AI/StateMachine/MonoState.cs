using UnityEngine;

namespace Game.AI.StateMachine
{
    public abstract class MonoState : MonoBehaviour, IState
    {
        private StateMachineAgent _stateMachine;

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract void FixedUpdate();

        public void SetStateMachine(StateMachineAgent stateMachine)
        {
            _stateMachine = stateMachine;
        }

        protected void ChangeState(MonoState nextState)
        {
            if (nextState != null)
            {
                _stateMachine.ChangeState(nextState);
            }
        }
    }
}