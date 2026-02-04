using UnityEngine;
﻿
﻿namespace Develop.Player.Entity
﻿{
﻿    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Develop/PlayerConfig")]
﻿    public class PlayerConfig : ScriptableObject
﻿    {
﻿        public float WalkSpeed => _walkSpeed;
﻿        public float RunSpeed => _runSpeed;
﻿        public float Damping => _damping;
﻿        public float EndSpeed => _endSpeed;
﻿
﻿        public float MaxAngle => _maxAngel;
﻿        public float LookSpeed => _lookSpeed;
﻿        public float DecelerationRate => _decelerationRate;
﻿        public float GroundCheckDistance => _groundCheckDistance;
﻿        public LayerMask GroundLayer => _groundLayer;
﻿        
﻿        public float ImpactStrength => _impactStrength;
﻿        public float DiagonalThreshold => _diagonalThreshold;
﻿
﻿        [Header("MovementState")]
﻿        [SerializeField] private float _walkSpeed;
﻿        [SerializeField] private float _runSpeed;
﻿        [SerializeField] private float _damping;
﻿        [SerializeField] private float _endSpeed;
﻿        [Header("LookState")]
﻿        [SerializeField] private float _maxAngel;
﻿        [SerializeField] private float _lookSpeed;
﻿        [Header("SlideState")]
﻿        [SerializeField] private float _decelerationRate;
﻿        [SerializeField] private float _groundCheckDistance;
﻿        [SerializeField] private LayerMask _groundLayer;
﻿        [Header("Impact Settings")]
﻿        [SerializeField] private float _impactStrength = 0.2f;
﻿        [SerializeField] private float _diagonalThreshold = 0.7f;
﻿
﻿
﻿    }
﻿}
﻿