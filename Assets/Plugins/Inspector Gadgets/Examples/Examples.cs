// Inspector Gadgets // Copyright 2017 Kybernetik //

#if UNITY_EDITOR

using InspectorGadgets;
using UnityEngine;

namespace InspectorGadgets.Examples
{
    /// <summary>
    /// Contains various methods to demonstrate the <see cref="RequireInitialisationAttribute"/>.
    /// </summary>
    public sealed class Examples : MonoBehaviour
    {
        /************************************************************************************************************************/

        private int _ProjectileCount;

        /************************************************************************************************************************/

        public void TestRequireInitialisationAddComponent()
        {
            var obj = new GameObject("Projectile " + _ProjectileCount++);
            obj.AddComponent<RequireInitialisationProjectile>();
            // Warning because the Projectile isn't being initialised.
        }

        /************************************************************************************************************************/

        public void TestRequireInitialisationFire()
        {
            var obj = new GameObject("Projectile " + _ProjectileCount++);
            var projectile = obj.AddComponent<RequireInitialisationProjectile>();
            projectile.Fire(gameObject);
            // No warning since we are firing the projectile properly.
        }

        /************************************************************************************************************************/

        public void TestConstructableBehaviourAddComponent()
        {
            var obj = new GameObject("Projectile " + _ProjectileCount++);
            obj.AddComponent<ConstructableBehaviourProjectile>();
            // Exception because the Projectile isn't being initialised.
        }

        /************************************************************************************************************************/

        public void TestRequireInitialisationUtilsAddComponent()
        {
            var obj = new GameObject("Projectile " + _ProjectileCount++);
            obj.AddComponent<ConstructableBehaviourProjectile, GameObject>(gameObject);
            // No warning since we are constructing the projectile properly.
        }

        /************************************************************************************************************************/
    }
}

#endif