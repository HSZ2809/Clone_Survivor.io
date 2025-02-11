using UnityEngine;

namespace ZUN
{
    public class BossSpawn : MonoBehaviour
    {
        // 플레이어 상단에 보스 등장 예고
        // 필드 몬스트 다이 처리
        // 보스 기준으로 이동 불가 처리
        // 일정 시간 후 보스 등장
        // 이동 불가였던 곳에 바리케이트 설치
        Character character;

        [Header("Boss")]
        [SerializeField] Monster boss;
        [SerializeField] float hp;
        [SerializeField] float attackPower;

        [Header("Range")]
        [SerializeField] float disistance = 10f;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        public void SetBoss()
        {
            Vector2 setLoc = character.transform.position + new Vector3(0, disistance);
            Monster mon = Instantiate(boss, setLoc, character.transform.rotation);
            mon.SetMonsterSpec(hp, attackPower);
            mon.gameObject.SetActive(true);
        }
    }
}