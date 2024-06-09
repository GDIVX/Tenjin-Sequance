namespace Game.AI.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void FixedUpdate();
        void SetStateMachine(StateMachineAgent stateMachine);
    }
}