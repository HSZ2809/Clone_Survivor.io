using UnityEngine;

namespace ZUN
{
    public class Menu : MonoBehaviour
    {
        Character character;

        [SerializeField] private GameObject pause = null;

        [Space]
        [SerializeField] private SkillDisplay[] activesDisplay;
        [SerializeField] private SkillDisplay[] passivesDisplay;
        [SerializeField] private Sprite yellowStar;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        private void OnEnable()
        {
            for (int i = 0; i < character.Actives.Length; i++)
            {
                if (character.Actives[i] == null)
                    break;

                activesDisplay[i].img_bg.gameObject.SetActive(true);
                activesDisplay[i].img_skill.sprite = character.Actives[i].Sprite;
                for (int k = 0; k < character.Actives[i].Level; k++)
                    activesDisplay[i].img_star[k].sprite = yellowStar;
            }

            for(int i = 0; i < character.Passives.Length; i++)
            {
                if (character.Passives[i] == null)
                    break;

                passivesDisplay[i].img_bg.gameObject.SetActive(true);
                passivesDisplay[i].img_skill.sprite = character.Passives[i].Sprite;
                for(int k = 0; k < character.Passives[i].Level; k++)
                    passivesDisplay[i].img_star[k].sprite = yellowStar;
            }
        }

        private void OnDisable()
        {
            /* 엑티브 스킬이 줄어들 시 대응 로직 */
        }

        public void Continue()
        {
            pause.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void Home()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}