// Inspector Gadgets // Copyright 2017 Kybernetik //

#if UNITY_EDITOR

using UnityEngine;

namespace InspectorGadgets.Examples
{
    /// <summary>
    /// This class is an example of the <see cref="RequireInitialisationAttribute"/> in action.
    /// <para></para>
    /// A warning will automatically be logged whenever an instance is created without the Fire method being called.
    /// </summary>
    [RequireInitialisation]
    public sealed class RequireInitialisationProjectile : MonoBehaviour
    {
        /************************************************************************************************************************/

        public void Fire(GameObject owner)
        {
            Debug.Log(name + " was fired by " + owner.name, this);
            this.MarkAsInitialised();
        }

        /************************************************************************************************************************/
    }
}

#endif