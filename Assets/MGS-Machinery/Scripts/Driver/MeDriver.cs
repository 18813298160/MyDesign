using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/MeDriver")]
    [RequireComponent(typeof(Mechanism))]
    public class MeDriver : MonoBehaviour
    {
        #region Property and Field
        public KeyCode positiveKey = KeyCode.P;
        public KeyCode negativeKey = KeyCode.N;
        public ModelUnit unit;
        public string modelName;

        protected Mechanism mechanism;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            mechanism = GetComponent<Mechanism>();
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
			mechanism.DriveMechanism(dir);
        }

        public void SetUnit(ModelUnit unit)
        {
            this.unit = unit;
            this.modelName = unit.name;
        }
        #endregion
    }
}