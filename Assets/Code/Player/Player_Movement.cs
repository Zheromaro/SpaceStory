using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;
using SpaceGame.Core.Stats;
using SpaceGame.Core.SaveSystem;

namespace SpaceGame.Player
{
    public class Player_Movement : MonoBehaviour, IDataPersistence
    {
        public bool freeMovement = true;
        public bool Direction = false;
        [SerializeField] private float LevelDegreeOfControling;

        [HideInInspector] public Vector2 movement;
        [HideInInspector] public float Manual = 1f;
        private float x;
        private float y;
        private InputAction Input_Move;
        private Rigidbody2D rb;
        private Animator animator;

        //------------------------------------------------------------------

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Input_Move = InputManager.inputActions.Player.Movement;
        }

        #region Saving...
        public void LoadData(GameData data)
        {
            this.transform.position = data.playerPosition;
        }

        public void SaveData(GameData data)
        {
            // Nothing...
        }
        #endregion

        private void Update()
        {
            if (freeMovement)
            {
                movement.x = Input_Move.ReadValue<Vector2>().x;
                movement.y = Input_Move.ReadValue<Vector2>().y;
            }
            else
            {
                if (Direction)
                {
                    y = Input_Move.ReadValue<Vector2>().y * LevelDegreeOfControling;
                    movement.y = Manual + y;

                    movement.x = Input_Move.ReadValue<Vector2>().x;
                }
                else
                {
                    x = Input_Move.ReadValue<Vector2>().x * LevelDegreeOfControling;
                    movement.x = Manual + x;

                    movement.y = Input_Move.ReadValue<Vector2>().y;
                }
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            if (movement.x > 0.1f || movement.x < -0.1f || movement.y > 0.1f || movement.y < -0.1f)
            {
                rb.AddForce(new Vector2(movement.x * StatsManager.statsManager._PlayerSpeed.speed, movement.y * StatsManager.statsManager._PlayerSpeed.speed), ForceMode2D.Impulse);
            }
        }
    }
}