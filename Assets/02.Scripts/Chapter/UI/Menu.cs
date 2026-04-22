using UnityEngine;
using Zenject;

namespace ZUN
{
    public class Menu : MonoBehaviour
    {
        [Inject] private IManager_Scene manager_Scene;
        [Inject] private IManager_Audio manager_Audio;
        private Character character;

        [SerializeField] private GameObject pause = null;

        [Space]
        [SerializeField] private SkillDisplay[] activesDisplay;
        [SerializeField] private SkillDisplay[] passivesDisplay;
        [SerializeField] private Sprite[] star;

        readonly int EMPTY = 0, YELLOW = 1, RED = 2;

        private void Awake()
        {
            character = FindFirstObjectByType<Character>();
        }

        private void OnEnable()
        {
            for (int i = 0; i < character.Actives.Length; i++)
            {
                if (character.Actives[i] == null)
                    break;

                activesDisplay[i].img_bg.gameObject.SetActive(true);

                if (character.Actives[i].Level == character.Actives[i].SkillInfo.MaxLevel)
                {
                    activesDisplay[i].img_star[2].sprite = star[RED];

                    activesDisplay[i].img_skill.sprite = character.Actives[i].SkillInfo.Sprite[1];
                }
                else
                {
                    for (int k = 0; k < character.Actives[i].Level; k++)
                        activesDisplay[i].img_star[k].sprite = star[YELLOW];

                    activesDisplay[i].img_skill.sprite = character.Actives[i].SkillInfo.Sprite[0];
                }

            }

            for(int i = 0; i < character.Passives.Length; i++)
            {
                if (character.Passives[i] == null)
                    break;

                passivesDisplay[i].img_bg.gameObject.SetActive(true);
                passivesDisplay[i].img_skill.sprite = character.Passives[i].SkillInfo.Sprite[0];
                for(int k = 0; k < character.Passives[i].Level; k++)
                    passivesDisplay[i].img_star[k].sprite = star[YELLOW];
            }
        }

        private void OnDisable()
        {
            foreach (var display in activesDisplay)
                foreach (var img in  display.img_star)
                    img.sprite = star[EMPTY];
        }

        public void Continue()
        {
            pause.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void Home()
        {
            //#if UNITY_EDITOR
            //UnityEditor.EditorApplication.isPlaying = false;              
            //#endif

            manager_Scene.LoadScene("Lobby", gameObject.scene.buildIndex);
        }

        public void ToggleVolume()
        {
            manager_Audio.ToggleEffectSound();
            manager_Audio.ToggleMusic();
        }
    }
}