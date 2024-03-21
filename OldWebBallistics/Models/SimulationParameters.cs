using System;

namespace OldWebBallistics.Models;

public class SimulationParameters
{
    public float InitialSpeed { get; set; } = 50.0f;
    public float Angle { get; set; } = 45.0f;
    public float Gravity { get; set; } = 9.81f;
    public float FrameRate { get; set; } = 50.0f;
    public float TimeScale { get; set; } = 1.0f;

    public event Action? OnChange;

    public void SetParameters(float initialSpeed, float angle, float gravity, float frameRate, float timeScale)
    {
        InitialSpeed = initialSpeed;
        Angle = angle;
        Gravity = gravity;
        FrameRate = frameRate;
        TimeScale = timeScale;

        NotifyStateChanged();
    }

    public void NotifyStateChanged() => OnChange?.Invoke();
}