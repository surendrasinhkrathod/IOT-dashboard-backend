namespace RealTimeCharts.Server.TimerFeatures
{
    public class TimerManager
    {
        private Timer? _chartTimer;
        private Timer? _widgetTimer;
        private Action? _chartAction;
        private Action? _widgetAction;
        private AutoResetEvent? _autoResetEvent;

        public DateTime TimerStarted { get; set; }
        public bool IsTimerStarted { get; set; }

        public void PrepareChartTimer(Action chartAction)
        {
            _chartAction = chartAction;
            _autoResetEvent = new AutoResetEvent(false);
            _chartTimer = new Timer(ExecuteChartAction, _autoResetEvent, 5000, 2000);
            TimerStarted = DateTime.Now;
            IsTimerStarted = true;
        }

        public void PrepareWidgetTimer(Action widgetAction)
        {
            _widgetAction = widgetAction;
            _autoResetEvent = new AutoResetEvent(false);
            _widgetTimer = new Timer(ExecuteWidgetAction, _autoResetEvent, 5000, 2000);
        }

        private void ExecuteChartAction(object? stateInfo)
        {
            _chartAction?.Invoke();
        }

        private void ExecuteWidgetAction(object? stateInfo)
        {
            _widgetAction?.Invoke();
        }
    }
}
