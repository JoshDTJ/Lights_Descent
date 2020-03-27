using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    public GameObject playerHand;
    public Image weaponSlot;
    public Item weapon;

    public Vector3 position;
    public Vector3 rotation;

    public void PickUp()
    {
        weaponSlot.sprite = weapon.icon;
        Debug.Log("Equip");

        transform.parent = playerHand.transform;
        transform.localPosition = position;
        transform.localEulerAngles = rotation;
    }
}
