using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    //This script should be attached to any player weapon
    public GameObject playerHand;
    public Image weaponSlot;
    public Item weapon;

    public Vector3 position;
    public Vector3 rotation;

    public void PickUp()
    {
        //Swapping the UI image with the assigned sprite of the weapon that has been picked up
        weaponSlot.sprite = weapon.icon;
        Debug.Log("Equip");

        //Placing the transform of the weapon into the player's "hand"
        transform.parent = playerHand.transform;
        transform.localPosition = position;
        transform.localEulerAngles = rotation;
    }
}
