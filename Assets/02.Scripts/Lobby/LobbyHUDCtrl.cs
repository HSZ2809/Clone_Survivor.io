using UnityEngine;
using TMPro;

namespace ZUN
{
    public class LobbyHUDCtrl : MonoBehaviour
    {
        Manager_Storage storage;

        [SerializeField] TextMeshProUGUI energy;
        [SerializeField] TextMeshProUGUI gem;
        [SerializeField] TextMeshProUGUI gold;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
            {
                Debug.LogWarning("Manager_Storage not found");
            }
        }

        private void OnEnable()
        {
            if (UserDataManager.instance != null)
                UserDataManager.instance.OnCoreDataChanged += HandleCoreDataChanged;
        }

        private void OnDisable()
        {
            if (UserDataManager.instance != null)
                UserDataManager.instance.OnCoreDataChanged -= HandleCoreDataChanged;
        }

        private void Start()
        {
            RefreshUIFromStorage();
        }

        private void HandleCoreDataChanged(UserData data)
        {
            OnEnergyChange();
            OnGemChange();
            OnGoldChange();
        }

        private void OnEnergyChange()
        {
            energy.text = storage.Energy.ToString() + "/30";
        }

        private void OnGemChange()
        {
            gem.text = storage.Gem.ToString();
        }

        private void OnGoldChange()
        {
            gold.text = storage.GetGoldFormatKNotation();
        }

        private void RefreshUIFromStorage()
        {
            OnEnergyChange();
            OnGemChange();
            OnGoldChange();
        }
    }
}