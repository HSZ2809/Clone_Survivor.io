using UnityEngine;
using System;
using TMPro;
using Zenject;

namespace ZUN
{
    public class LobbyHUDCtrl : MonoBehaviour
    {
        [Inject] private IManager_Storage storage;
        [Inject] private IUserDataManager userDataManager;

        [SerializeField] TextMeshProUGUI energy;
        [SerializeField] TextMeshProUGUI gem;
        [SerializeField] TextMeshProUGUI gold;

        private void OnEnable()
        {
            if (userDataManager != null)
                userDataManager.OnCoreDataChanged += HandleCoreDataChanged;
        }

        private void OnDisable()
        {
            if (userDataManager != null)
                userDataManager.OnCoreDataChanged -= HandleCoreDataChanged;
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