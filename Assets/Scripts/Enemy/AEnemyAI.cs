namespace Scripts.Enemy
{
    public abstract class AEnemyAI
    {
        public abstract void Update();
        public abstract void Stun();

        public EnemyController Enemy;
    }
}
