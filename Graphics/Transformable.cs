using System;
using Map.Core;
using Map.Core.Structs;
using Map.Graphics.Structs;

namespace Map.Graphics
{
    /// <summary>
    /// Decomposed transform by a position, a rotation and a scale
    /// </summary>
    public class Transformable : ObjectBase
    {
        #region Attributes

        private Vector2Df _origin = new Vector2Df(0, 0);
        private Vector2Df _position = new Vector2Df(0, 0);
        private float _rotation = 0;
        private Vector2Df _scale = new Vector2Df(1, 1);
        private Transform _transform;
        private Transform _inverseTransform;
        private bool _transformNeedUpdate = true;
        private bool _inverseNeedUpdate = true;

        #endregion

        #region Properties

        /// <summary>
        /// The origin of an object defines the center point for
        /// all transformations (position, scale, rotation).
        /// The coordinates of this point must be relative to the
        /// top-left corner of the object, and ignore all
        /// transformations (position, scale, rotation).
        /// </summary>
        public Vector2Df Origin
        {
            get => _origin;
            set
            {
                _origin = value;
                _transformNeedUpdate = true;
                _inverseNeedUpdate = true;
            }
        }

        /// <summary>
        /// Position of the object
        /// </summary>
        public Vector2Df Position
        {
            get => _position;
            set
            {
                _position = value;
                _transformNeedUpdate = true;
                _inverseNeedUpdate = true;
            }
        }
        
        /// <summary>
        /// Rotation of the object
        /// </summary>
        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _transformNeedUpdate = true;
                _inverseNeedUpdate = true;
            }
        }
        
        /// <summary>
        /// Scale of the object
        /// </summary>
        public Vector2Df Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _transformNeedUpdate = true;
                _inverseNeedUpdate = true;
            }
        }
        
        /// <summary>
        /// The combined transform of the object
        /// </summary>
        public Transform Transform
        {
            get
            {
                if (!_transformNeedUpdate)
                {
                    return _transform;
                }

                _transformNeedUpdate = false;

                var angle = -_rotation * 3.141592654F / 180.0F;
                var cosine = (float ) Math.Cos(angle);
                var sine = (float) Math.Sin(angle);
                var sxc = _scale.X * cosine;
                var syc = _scale.Y * cosine;
                var sxs = _scale.X * sine;
                var sys = _scale.Y * sine;
                var tx = -_origin.X * sxc - _origin.Y * sys + _position.X;
                var ty = _origin.X * sxs - _origin.Y * syc + _position.Y;

                _transform = new Transform(sxc, sys, tx,
                    -sxs, syc, ty,
                    0.0F, 0.0F, 1.0F);

                return _transform;
            }
        }
        
        /// <summary>
        /// The combined transform of the object
        /// </summary>
        public Transform InverseTransform
        {
            get
            {
                if (!_inverseNeedUpdate)
                {
                    return _inverseTransform;
                }

                _inverseNeedUpdate = false;
                _inverseTransform = Transform.GetInverse();

                return _inverseTransform;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the object from its internal C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to the object in the C library</param>
        protected Transformable(IntPtr cPointer) : base(cPointer) {}

        #endregion

        #region Methods

        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the garbage collector disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            // Does nothing, this instance is either pure C# (if created by the user)
            // or not the final object (if used as a base for a drawable class)
        }

        #endregion
    }
}
