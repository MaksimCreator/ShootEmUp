using UnityEngine;
namespace ShootEmUp.View
{
    using Model;

    public class TransformableView : MonoBehaviour
    {
        private Transformable _model;

        public void Init(Transformable transformable)
        {
            _model = transformable;
        }

        private void LateUpdate()
        => transform.position = _model.Position;
    }
}