using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/RockerJoint")]
    [ExecuteInEditMode]
    public class RockerJoint : RockerMechanism
    {
        #region Property and Field
        /// <summary>
        /// Keep up mode.
        /// </summary>
        public KeepUpMode keepUp = KeepUpMode.TransformUp;

        /// <summary>
        /// Transform's forward as world up for look at.
        /// </summary>
        [HideInInspector]
        public Transform reference;

        /// <summary>
        /// World up for look at.
        /// </summary>
        public Vector3 WorldUp
        {
            get
            {
                if (keepUp == KeepUpMode.ReferenceForward && reference)
                    return reference.forward;
                else
                    return transform.up;
            }
        }
        #endregion

        #region Protected Method
#if UNITY_EDITOR
        protected virtual void Update()
        {
            //不知道是什么鬼，目前没发现这句话的作用
            if (!Application.isPlaying && rockJoint)
                DriveMechanism();
        }
#endif
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        public override void DriveMechanism()
        {
            transform.LookAt(rockJoint, WorldUp);
        }
        #endregion
    }
}