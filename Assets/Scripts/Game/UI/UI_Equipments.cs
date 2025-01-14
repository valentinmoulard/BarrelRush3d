using System.Collections.Generic;
using UnityEngine;
using Game.Ui.Buttons;
using DG.Tweening;

namespace Game.Ui
{
    public class UI_Equipments : MonoBehaviour
    {
        [SerializeField]
        private RectTransform buttonShowWeaponRectTransform = null;

        [SerializeField]
        private Button_ShowWeaponEquipments buttonShowWeaponEquipments = null;

        [SerializeField]
        private RectTransform buttonShowClotheRectTransform = null;

        [SerializeField]
        private Button_ShowClotheEquipments buttonShowClotheEquipments = null;

        [SerializeField]
        private float buttonHeightWhenNotActiveInPixel = 50f;

        [SerializeField]
        private float buttonHeightWhenActiveInPixel = 100f;

        [SerializeField]
        private List<GameObject> weaponCardFrameList = null;

        [SerializeField]
        private List<GameObject> clotheCardFrameList = null;


        private void OnEnable()
        {
            buttonShowWeaponEquipments.OnClickShowWeaponEquipments += OnShowWeaponEquipments;
            buttonShowClotheEquipments.OnClickShowClotheEquipments += OnShowClotheEquipments;

            OnShowWeaponEquipments();
        }

        private void OnDisable()
        {
            buttonShowWeaponEquipments.OnClickShowWeaponEquipments -= OnShowWeaponEquipments;
            buttonShowClotheEquipments.OnClickShowClotheEquipments -= OnShowClotheEquipments;
        }

        private void OnShowWeaponEquipments()
        {
            buttonShowWeaponRectTransform.DOSizeDelta(new Vector2(buttonShowWeaponRectTransform.sizeDelta.x, buttonHeightWhenActiveInPixel), 0.5f);
            buttonShowClotheRectTransform.DOSizeDelta(new Vector2(buttonShowWeaponRectTransform.sizeDelta.x, buttonHeightWhenNotActiveInPixel), 0.5f);

            ToggleEquipmentState(weaponCardFrameList, true);
            ToggleEquipmentState(clotheCardFrameList, false);
        }

        private void OnShowClotheEquipments()
        {
            buttonShowWeaponRectTransform.DOSizeDelta(new Vector2(buttonShowWeaponRectTransform.sizeDelta.x, buttonHeightWhenNotActiveInPixel), 0.5f);
            buttonShowClotheRectTransform.DOSizeDelta(new Vector2(buttonShowWeaponRectTransform.sizeDelta.x, buttonHeightWhenActiveInPixel), 0.5f);

            ToggleEquipmentState(weaponCardFrameList, false);
            ToggleEquipmentState(clotheCardFrameList, true);
        }

        private void ToggleEquipmentState(List<GameObject> list, bool state)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SetActive(state);
            }
        }

    }
}