﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType weaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Puncher[] punchers = other.gameObject.GetComponentsInChildren<Puncher>();
            foreach (Puncher puncher in punchers)
            {
                if (puncher.side == Side.Right)
                {
                    puncher.PickupWeapon(weaponType);
                }
            }
            Destroy(gameObject);
        }
    }
}