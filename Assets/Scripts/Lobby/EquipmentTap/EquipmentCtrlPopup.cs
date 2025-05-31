using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class EquipmentCtrlPopup : MonoBehaviour
    {
        [SerializeField] Image tierTitle;
        [SerializeField] Sprite[] tierTitleSprites;
        [SerializeField] TextMeshProUGUI tier;
        [SerializeField] string[] tierStrings;
        [SerializeField] TextMeshProUGUI equipmentName;
        [SerializeField] Image equipmentBg;
        [SerializeField] Sprite[] equipmentBgSprites;
        [SerializeField] Image img_equipment;
        [SerializeField] Image equipmentTypeBg;
        [SerializeField] Sprite[] equipmentTypeBgSprites;
        [SerializeField] Image equipmentType;
        [SerializeField] Sprite[] equipmentTypeSprites;
        [SerializeField] TextMeshProUGUI tmp_level;
        [SerializeField] Image img_stat;
        [SerializeField] Sprite[] stats;
        [SerializeField] TextMeshProUGUI tmp_stat;
        [SerializeField] TextMeshProUGUI tmp_description;
        [SerializeField] SkillDescriptionCtrl[] skillDescriptionCtrls;
        [SerializeField] TextMeshProUGUI tmp_gold;
        [SerializeField] TextMeshProUGUI tmp_scroll;

        Equipment _equipment;
        Manager_Storage manager_Storage;

        const int HP = 0, ATK = 1;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out manager_Storage))
                Debug.LogWarning("Storage not found");
        }

        public void SetPopup(Equipment equipment)
        {
            _equipment = equipment;

            tierTitle.sprite = tierTitleSprites[(int)_equipment.Tier];
            tier.text = tierStrings[(int)_equipment.Tier];
            equipmentName.text = _equipment.Data.EquipmentName;
            equipmentBg.sprite = equipmentBgSprites[(int)_equipment.Tier];
            img_equipment.sprite = (_equipment.Tier < EquipmentTier.Elite) ? _equipment.Data.IconSprite[0] : _equipment.Data.IconSprite[1];
            equipmentTypeBg.sprite = equipmentTypeBgSprites[(int)_equipment.Tier];
            equipmentType.sprite = equipmentTypeSprites[(int)_equipment.Data.Type];
            tmp_level.text = "레벨: " + _equipment.Level.ToString() + "/" + _equipment.Data.MaxLevel[(int)_equipment.Tier].ToString();
            switch(_equipment.Data.Type)
            {
                case EquipmentType.Weapom:
                    Weapon weapon = _equipment as Weapon;
                    tmp_stat.text = weapon.Atk.ToString();
                    img_stat.sprite = stats[ATK];
                    break;
                case EquipmentType.Armor:
                    Armor armor = _equipment as Armor;
                    tmp_stat.text = armor.Hp.ToString();
                    img_stat.sprite = stats[HP];
                    break;
                case EquipmentType.Necklace:
                    Necklace necklace = _equipment as Necklace;
                    tmp_stat.text = necklace.Atk.ToString();
                    img_stat.sprite = stats[ATK];
                    break;
                case EquipmentType.Belt:
                    Belt belt = _equipment as Belt;
                    tmp_stat.text = belt.Hp.ToString();
                    img_stat.sprite = stats[HP];
                    break;
                case EquipmentType.Gloves:
                    Gloves gloves = _equipment as Gloves;
                    tmp_stat.text = gloves.Atk.ToString();
                    img_stat.sprite = stats[ATK];
                    break;
                case EquipmentType.Shoes:
                    Shoes shoes = _equipment as Shoes;
                    tmp_stat.text = shoes.Hp.ToString();
                    img_stat.sprite = stats[HP];
                    break;
            }
            tmp_description.text = _equipment.Data.Comment;
            string[] tierSkillDescription = _equipment.Data.GetTierSkillDescription();
            for (int i = 0; i < skillDescriptionCtrls.Length; i++)
            {
                if (tierSkillDescription[i] == "EMPTY")
                    skillDescriptionCtrls[i].gameObject.SetActive(false);
                else
                {
                    skillDescriptionCtrls[i].SetDescription(tierSkillDescription[i]);
                    skillDescriptionCtrls[i].gameObject.SetActive(true);
                }
            }
            for(int i = 0; i < (int)_equipment.Tier; i++)
            {
                skillDescriptionCtrls[i].Unlock();
            }
            tmp_gold.text = _equipment.LevelupGoldCost.ToString() + "/" + manager_Storage.GetGoldFormatKNotation();
        }
    }
}