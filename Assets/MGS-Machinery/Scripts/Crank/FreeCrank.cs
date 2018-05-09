using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/FreeCrank")]
    public class FreeCrank : CrankMechanism
    {
        #region Protected Method
        /// <summary>
        /// Rotate the crank by speed.
        /// </summary>
        /// <param name="rSpeed">Speed of rotate crank.</param>
        protected virtual void DriveCrank(float rSpeed)
        {
            lockRecord = Angle;
            Angle += rSpeed * Time.deltaTime;
            DriveCrank();

            if (CheckRockersLock())
            {
                Angle = lockRecord;
                DriveCrank();
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
            DriveCrank(speed * speedControl);
        }
        #endregion
    }
}