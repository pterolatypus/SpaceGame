// Inspector Gadgets // Copyright 2017 Kybernetik //

#if UNITY_EDITOR

using System;
using UnityEngine;

namespace InspectorGadgets.Examples
{
    /// <summary>
    /// This class is an example of the <see cref="ConstructableBehaviour{T}"/> in action.
    /// <para></para>
    /// A warning will automatically be logged whenever an instance is created without the Fire method being called.
    /// </summary>
    public sealed class ConstructableBehaviourProjectile : ConstructableBehaviour<GameObject>
    {
        /************************************************************************************************************************/

        private GameObject _Owner;

        /************************************************************************************************************************/

        protected override void OnConstructor(GameObject owner)
        {
            _Owner = owner;

            // Can't access most of the Unity Engine because htis script isn't initialised yet.
            // So we just store the owner so Awake can deal with it.
        }

        private void Awake()
        {
            Debug.Log(name + " was fired by " + _Owner.name, this);
        }

        /************************************************************************************************************************/
    }
}

#endif