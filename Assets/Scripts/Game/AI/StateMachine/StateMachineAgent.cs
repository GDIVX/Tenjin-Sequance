using System;
using UnityEngine;

namespace Game.AI.StateMachine
{
    public class StateMachineAgent : MonoBehaviour
    {
        [SerializeField] private MonoState initialState;
        private IState _currentState;

        public void ChangeState(IState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.SetStateMachine(this);
            _currentState.Enter();
        }

        private void Start()
        {
            if (initialState != null)
            {
                ChangeState(initialState);
            }
        }

        private void Update()
        {
            _currentState?.Update();
        }

        private void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}