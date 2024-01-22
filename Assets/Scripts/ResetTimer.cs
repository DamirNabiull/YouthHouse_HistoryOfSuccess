using Notifications;
using UnityEngine;

public class ResetTimer : MonoBehaviour, INotifier
{
    private float _lastUpdateTime = 0;
    private float _restartTime;
    private bool _countTime = false;
    private ISubscriber _subscriber;

    private void Start()
    {
        _subscriber = FindObjectOfType<UIController>();
        ConfigReader.ReadConfig();
        _restartTime = ConfigReader.AppConfig.timer;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Reset();
        }

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Reset();
            }
        }

        if (Time.time - _lastUpdateTime > _restartTime && _countTime)
        {
            _countTime = false;
            Notify();
        }
    }

    private void Reset()
    {
        _countTime = true;
        _lastUpdateTime = Time.time;
    }

    public void Notify()
    {
        _subscriber.Notify();
    }
}