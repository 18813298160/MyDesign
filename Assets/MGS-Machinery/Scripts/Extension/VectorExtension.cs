using UnityEngine;

namespace Developer.VectorExtention
{
    public static class EVector3
    {
        #region Public Method
        /// <summary>
        /// Calculate rotate angle of two vectors int the range(0~360).
        /// </summary>
        /// <param name="from">Start vector.</param>
        /// <param name="to">End vector.</param>
        /// <param name="normal">Custom normal.</param>
        /// <returns>Rotate angle of two vectors.</returns>
        public static float RotateAngle(Vector3 from, Vector3 to, Vector3 normal)
        {
            //Project.
            //投影到以normal为法向量的平面上
            from = Vector3.ProjectOnPlane(from, normal);
            to = Vector3.ProjectOnPlane(to, normal);

            //Project is Vector3.zero.
            if (from == Vector3.zero || to == Vector3.zero)
                return 0;

            //Calculate reference.
            var ftCross = Vector3.Cross(from, to);
            var toCross = Vector3.Cross(from, ftCross);
            var tcDot = Vector3.Dot(to, toCross);
            var ncDot = Vector3.Dot(normal, ftCross);

            //Convert to rotate angle.
            var angle = Vector3.Angle(from, to);
            if ((ncDot > 0 && tcDot >= 0) || (ncDot < 0 && tcDot <= 0))
                angle = 360 - angle;
            return angle;
        }
        #endregion
    }
}