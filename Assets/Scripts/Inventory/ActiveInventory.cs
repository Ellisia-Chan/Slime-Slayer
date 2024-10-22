using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour {

    public static ActiveInventory Instance;
    
    private int activeSlotIndexNum = 0;
    private int currentSlotIndexNum = -1;

    private void Awake() {
        Instance = this;

        ToggleActiveSlot(1);
    }

    private void Start() {
        GameInput.Instance.OnInventoryAction += GameInput_OnInventoryAction;
    }

    private void GameInput_OnInventoryAction(object sender, int slotIndex) {
        ToggleActiveSlot(slotIndex);
    }

    private void ToggleActiveSlot(int numValue) {
        ToggleActiveHighlighted(numValue - 1);
    }

    private void ToggleActiveHighlighted(int indexNum) {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in transform) {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon() {
        if (activeSlotIndexNum == currentSlotIndexNum) {
            return;
        }

        if (ActiveWeapon.Instance.GetCurrentActiveWeapon() != null) {
            Destroy(ActiveWeapon.Instance.GetCurrentActiveWeapon().gameObject);
        }

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>()) {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawm = transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawm, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        currentSlotIndexNum = activeSlotIndexNum;

        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    public int GetInventorySlotIndex() {
        return activeSlotIndexNum;
    }

    private void OnDestroy() {
        GameInput.Instance.OnInventoryAction -= GameInput_OnInventoryAction;
    }
}
