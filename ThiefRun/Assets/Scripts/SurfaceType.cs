using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceType : MonoBehaviour
{
    public enum FLOORTYPE { WOOD, STONE, DEFAULT }
    public FLOORTYPE floor = FLOORTYPE.DEFAULT;
}
