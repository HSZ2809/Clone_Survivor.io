namespace ZUN
{
    public abstract class NomalMonster : Monster
    {
        protected MonsterSpawner monsterSpawner;

        public void SetSpawner(MonsterSpawner _monsterSpawner)
        {
            monsterSpawner = _monsterSpawner;
        }
    }
}