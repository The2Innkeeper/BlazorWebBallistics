using Microsoft.AspNetCore.Components;
using BlazorWebBallistics.Models;

namespace BlazorWebBallistics.Components;

public partial class SimulationControls
{
    [Inject]
    private SimulationParameters Parameters { get; set; }
    private void UpdateFrameRate(ChangeEventArgs e)
    {
        if (float.TryParse(e.Value?.ToString(), out float newRate))
        {
            Parameters.FrameRate = newRate;
            Parameters.NotifyStateChanged();
        }
    }

    private void UpdateTimeScale(ChangeEventArgs e)
    {
        if (float.TryParse(e.Value?.ToString(), out float newTimeScale))
        {
            Parameters.TimeScale = newTimeScale;
            Parameters.NotifyStateChanged();
        }
    }

    private void UpdateSpeed(ChangeEventArgs e)
    {
        if (float.TryParse(e.Value?.ToString(), out float newSpeed))
        {
            Parameters.InitialSpeed = newSpeed;
            Parameters.NotifyStateChanged();
        }
    }

    private void UpdateAngle(ChangeEventArgs e)
    {
        if (float.TryParse(e.Value?.ToString(), out float newAngle))
        {
            Parameters.Angle = newAngle;
            Parameters.NotifyStateChanged();
        }
    }

    private void UpdateGravity(ChangeEventArgs e)
    {
        if (float.TryParse(e.Value?.ToString(), out float newGravity))
        {
            Parameters.Gravity = newGravity;
            Parameters.NotifyStateChanged();
        }
    }
}