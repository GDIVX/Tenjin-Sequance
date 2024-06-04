using System;
using Game.Combat;
using UnityEngine;

namespace Game.Queue
{
    [Serializable]
    public class Marble
    {
        [SerializeField] private float speed;
        [SerializeField] private Sprite sprite;
        [SerializeField] private ProjectileModel projectileModel;
        [SerializeField] private Vector3 position;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public Sprite Sprite
        {
            get => sprite;
            set => sprite = value;
        }

        public ProjectileModel ProjectileModel
        {
            get => projectileModel;
            private set => projectileModel = value;
        }

        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        public Marble(float speed, ProjectileModel projectileModel, Sprite sprite)
        {
            Speed = speed;
            ProjectileModel = projectileModel;
            Sprite = sprite;
        }

        public void UpdatePosition(Vector3 goal, float minDistanceToGoal = 0f)
        {
            //Determine if we already reached our goal within the desired distance
            if (Vector3.Distance(goal, Position) <= minDistanceToGoal) return;

            //lerp one frame towards the goal
            Position = Vector3.Lerp(Position, goal, Time.deltaTime * Speed);
        }
    }
}