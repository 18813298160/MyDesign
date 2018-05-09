using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/RockerLock")]
    [RequireComponent(typeof(RockerJoint))]
    [ExecuteInEditMode]
    public class RockerLock : RockerLockMechanism
    {
        #region Protected Method
        //Execute in edit mode.
        protected override void Awake()
        {
            base.Awake();
        }
        #endregion
    }
}