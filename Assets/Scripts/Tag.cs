using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    public List<Tags> tags = new List<Tags>();

    public enum Tags
    {
        Projectile,
        Loot,
        Ball,
        Player
    }
}
