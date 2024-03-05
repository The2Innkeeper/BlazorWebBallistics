// File: Components/BallisticsDisplay.razor.cs
using System;
using System.Timers;
using Microsoft.AspNetCore.Components;
using WebBallistics.Models;
using WebBallistics.Services;

namespace WebBallistics.Components;

public partial class BallisticsDisplay : IDisposable
{
    [Inject] private SimulationParameters Parameters { get; set; }
    [Inject] private ITrajectoryCalculationService AnimationService { get; set; }

    private const int SVG_WIDTH = 1000;
    private const int SVG_HEIGHT = 600;
    private string trajectoryPath;
    private Vector2 currentPosition = new(0, 0);
    private System.Timers.Timer animationTimer;
    private DateTime animationStartTime;
    private bool isAnimating = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Parameters.OnChange += RefreshTrajectory;
        RefreshTrajectory();
    }

    private void RefreshTrajectory()
    {
        trajectoryPath = AnimationService.CalculatePath(Parameters);
        if (!isAnimating)
        {
            StateHasChanged();
        }
    }

    private void Simulate()
    {
        if (!isAnimating)
        {
            animationStartTime = DateTime.Now;
            StartAnimation();
        }
    }

    private void StartAnimation()
    {
        isAnimating = true;
        SetupAnimationTimer();
    }

    private void SetupAnimationTimer()
    {
        animationTimer?.Stop();
        animationTimer?.Dispose();
        animationTimer = new(1000 / Parameters.FrameRate);
        animationTimer.Elapsed += UpdateProjectilePosition;
        animationTimer.AutoReset = true;
        animationTimer.Start();
    }

    // Converts a point from Cartesian coordinates to SVG coordinates
    private Vector2 ConvertToSvgCoordinates(Vector2 cartesianPoint)
    {
        return new Vector2(cartesianPoint.X, SVG_HEIGHT - cartesianPoint.Y);
    }

    private void UpdateProjectilePosition(object source, ElapsedEventArgs e)
    {
        float elapsedTime = (float)((DateTime.Now - animationStartTime).TotalSeconds * Parameters.TimeScale);
        Vector2 cartesianPosition = AnimationService.CalculatePositionAtTime(elapsedTime, Parameters);
        currentPosition = ConvertToSvgCoordinates(cartesianPosition);

        if (cartesianPosition.Y <= 0 || cartesianPosition.X >= SVG_WIDTH)
        {
            StopAnimation();
        }
        else
        {
            InvokeAsync(StateHasChanged);
        }
    }

    private void StopAnimation()
    {
        animationTimer.Stop();
        animationTimer.Dispose();
        animationTimer = null;
        isAnimating = false;
    }

    public void Dispose()
    {
        animationTimer?.Stop();
        animationTimer?.Dispose();
        Parameters.OnChange -= RefreshTrajectory;
    }
}