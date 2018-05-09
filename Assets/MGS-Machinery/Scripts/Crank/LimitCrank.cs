using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/LimitCrank")]
    public class LimitCrank : FreeCrank
    {
        #region Property and Field
        /// <summary>
        /// Min angle limit of crank.
        /// </summary>
        public float minAngle = -45;

        /// <summary>
        /// Max angle limit of crank.
        /// </summary>
        public float maxAngle = 45;
        #endregion

        #region Protected Method
        /// <summary>
        /// Rotate crank in the range by speed.
        /// </summary>
        /// <param name="rSpeed">Rotate speed.</param>
        protected override void DriveCrank(float rSpeed)
        {
            lockRecord = Angle;
            Angle += rSpeed * Time.deltaTime;
            Angle = Mathf.Clamp(Angle, minAngle, maxAngle);
            DriveCrank();

            if (CheckRockersLock())
            {
                Angle = lockRecord;
                DriveCrank();
            }
        }
        #endregion
    }
}