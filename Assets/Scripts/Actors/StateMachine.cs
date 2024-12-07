public abstract class StateMachine
{
    public EntityBeing entity;

    public abstract void OnEnter();

    public abstract void Action();

    public abstract void Next(StateMachine stateMachine);

    public abstract void OnExit();
}