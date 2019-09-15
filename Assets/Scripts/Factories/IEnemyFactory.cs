public interface IEnemyFactory<T> where T: FlyingEnemy
{
    T GetInstance();
    void Return(T t);
}