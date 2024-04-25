using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject

{
    public abstract void Apply(GameObject gameObject);

    public abstract void Cancel(GameObject gameObject);
}
