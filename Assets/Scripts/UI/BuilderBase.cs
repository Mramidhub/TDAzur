using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBase : MonoBehaviour
{
    public virtual GameObject BuildProduct(Transform parent) { return new GameObject(); }
}
