using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Queue;
using Game.Utility;
using UnityEngine;

namespace Game.UI
{
    public class QueueHUD : MonoBehaviour
    {
        [SerializeField] private MarbleQueue marbleQueue;
        [SerializeField] private MarbleUI marbleUIPrefab;
        [SerializeField] private Transform marbleUIParent;
        [SerializeField] private Transform start, end;
        [SerializeField] private float spacing;

        private ObjectPool<MarbleUI> marbleUIPool;
        private Dictionary<Marble, MarbleUI> activeMarbles = new Dictionary<Marble, MarbleUI>();
        private List<Marble> marblesToProcess = new List<Marble>();

        private void Awake()
        {
            marbleUIPool = new ObjectPool<MarbleUI>(marbleUIPrefab);
        }

        private void OnEnable()
        {
            marbleQueue.onMarbleCreated.AddListener(OnMarbleCreated);
            marbleQueue.onMarbleEjected.AddListener(OnMarbleEjected);
        }

        private void OnDisable()
        {
            marbleQueue.onMarbleCreated.RemoveListener(OnMarbleCreated);
            marbleQueue.onMarbleEjected.RemoveListener(OnMarbleEjected);
        }

        private void OnMarbleCreated(Marble marble)
        {
            MarbleUI marbleUI = marbleUIPool.Get();
            marbleUI.Initialize(marble.Sprite);
            marbleUI.transform.position = start.transform.position;
            marbleUI.transform.SetParent(marbleUIParent);
            activeMarbles[marble] = marbleUI;
            marblesToProcess.Add(marble);
            marbleUI.gameObject.SetActive(true);
        }

        private void OnMarbleEjected(Marble marble)
        {
            if (!activeMarbles.TryGetValue(marble, out MarbleUI marbleUI)) return;
            marbleUI.gameObject.SetActive(false);
            marbleUIPool.ReturnToPool(marbleUI);
            activeMarbles.Remove(marble);
        }

        private void Update()
        {
            int queueLength = marbleQueue.Count;
            float uiDistance = Vector3.Distance(start.position, end.position);
            foreach (Marble marble in marblesToProcess)
            {
                if (!activeMarbles.TryGetValue(marble, out MarbleUI marbleUI)) continue;
                float relativeDistance = marble.Position.magnitude / queueLength;
                float uiRelativeDistance = relativeDistance * uiDistance;


                Vector3 direction = (start.position - end.position).normalized;
                float distance = spacing * uiRelativeDistance;
                marbleUI.transform.position = direction * distance + end.position;
            }
        }
    }
}