using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scri_PlayerPickup : MonoBehaviour
{
    #region Unused
    //This script should be attached to any player weapon
    //public GameObject playerHand;
    #endregion

    public Image weaponSlot;
    public scri_Item weapon;

    #region Unused
    //public Vector3 position;
    //public Vector3 rotation;
    #endregion

    public void PickUp()
    {
        //Swapping the UI image with the assigned sprite of the weapon that has been picked up
        weaponSlot.sprite = weapon.icon;
        Debug.Log("Equip");

        #region Unused
        //Placing the transform of the weapon into the player's "hand"
        //transform.parent = playerHand.transform;
        //transform.localPosition = position;
        //transform.localEulerAngles = rotation;
        #endregion
    }
}
