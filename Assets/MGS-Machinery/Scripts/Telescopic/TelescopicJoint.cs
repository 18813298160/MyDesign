﻿using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/TelescopicJoint")]
    public class TelescopicJoint : TelescopicJointMechanism
    {
        #region Protected Method
        /// <summary>
        /// Drive joint.
        /// </summary>
        /// <param name="mSpeed">Move speed.</param>
        protected virtual void DriveJoint(float mSpeed)
        {
            lockRecord = Displacement;
            Displacement += mSpeed * Time.deltaTime;
            Displacement = Mathf.Clamp(Displacement, 0, stroke);
            DriveJoint();

            if (CheckRockersLock())
            {
                Displacement = lockRecord;
                DriveJoint();
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            DriveJoint(speed * speedControl);

            if (Displacement <= 0)
                TState = TelescopicState.Shrink;
            else if (Displacement >= stroke)
                TState = TelescopicState.Extend;
        }
        #endregion
    }
}