using System;
using UnityEngine;

public class AnimationImpactWatcher : MonoBehaviour
{
    /// <summary>
    /// Called by Animation
    /// </summary>
    public event Action OnImpact; 

    private void Impact()
    {
        if (OnImpact != null)
            OnImpact();
    }
}
