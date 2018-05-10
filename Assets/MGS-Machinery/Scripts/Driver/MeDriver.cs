using UnityEngine;

namespace Developer.Machinery
{
    //[RequireComponent(typeof(Mechanism))]
    public class MeDriver : MonoBehaviour
    {
        #region Property and Field
        public KeyCode positiveKey = KeyCode.P;
        public KeyCode negativeKey = KeyCode.N;
        public ModelUnit unit;
        public string modelName;

        private bool driveCapability = true;
        private bool isSingle = true;

        protected Mechanism mechanism;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            mechanism = GetComponent<Mechanism>();
            if (!mechanism)
                isSingle = false;
        }

        protected virtual void Update()
        {
            if (Input.GetKey(positiveKey))
                DriveMechanism(true);
            else if (Input.GetKey(negativeKey))
                DriveMechanism(false);
        }

        public void DriveMechanism(bool positive = true)
        {
            float dir = positive ? 1.0f : -1.0f;
            if (!mechanism || !driveCapability) return;
			mechanism.DriveMechanism(dir);
        }

		public void SetDriveCapability(bool able)
		{
			driveCapability = able;
		}

        public void SetUnit(ModelUnit unit)
        {
            this.unit = unit;
            this.modelName = unit.name;
        }

        public bool IsSingle()
        {
            return isSingle;
        }
        #endregion
    }
}