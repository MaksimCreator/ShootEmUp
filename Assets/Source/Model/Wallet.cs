using System;

namespace ShootEmUp.Model
{
    public class Wallet : IWallet
    {
        private EnemyVisiter _enemyVisiter = new();

        public event Action onUpdateScore;

        public int Score => _enemyVisiter.AccamulatedScore;

        public void OnKill(Enemy enemy)
        { 
            _enemyVisiter.Visit((dynamic)enemy);
            onUpdateScore.Invoke();
        }

        public void OnKill(Row row)
        => _enemyVisiter.Visit(row);

        public void Reset()
        => _enemyVisiter = new EnemyVisiter();

        private class EnemyVisiter : IEnemyVisiter
        {
            public int AccamulatedScore { get; private set; }

            public void Visit(DefoltEnemy enemy)
            => AccamulatedScore += 8;

            public void Visit(Row row)
            => AccamulatedScore += 16;
        }
    }
}