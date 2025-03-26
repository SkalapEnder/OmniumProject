public interface IAttackComponent : ICharacterComponent
{
    public float Damage { get; }
    public float AttackRange { get; }

    public void MakeAttack();
    public void OnUpdate();
}
