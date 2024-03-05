// File: Components/BallisticsDisplay.razor.cs
using System;
using System.Timers;
using Microsoft.AspNetCore.Components;
using WebBallistics.Models;
using WebBallistics.Services;
using WebBallistics.Helpers;

namespace WebBallistics.Components;

public partial class BallisticsDisplay : IDisposable
{
    [Inject] private SimulationParameters Parameters { get; set; }
    [Inject] private ITrajectoryCalculationService AnimationService { get; set; }

    private const int SVG_WIDTH = Configuration.SVG_WIDTH;
    private const int SVG_HEIGHT = Configuration.SVG_HEIGHT;
    private string trajectoryPath;
    private Vector2 currentPosition = new(0, SVG_HEIGHT);
    private System.Timers.Timer animationTimer;
    private bool isAnimating = false;
    private float simulationTime = 0f;
    private float deltaTimeMS = 20f;


    protected override void OnInitialized()
    {
        base.OnInitialized();
        Parameters.OnChange += RefreshTrajectory;
        Parameters.OnChange += SetupAnimationTimer;
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
            ResetSimulation();
            StartAnimation();
        }
    }

    private void StartAnimation()
    {
        isAnimating = true;
        SetupAnimationTimer();
    }

    private void StopAnimation()
    {
        animationTimer.Stop();
        animationTimer.Dispose();
        animationTimer = null;
        isAnimating = false;
        ResetSimulation(); // Reset the simulation when stopping
    }

    private void ResetSimulation()
    {
        simulationTime = 0f; // Reset the simulation time
        currentPosition = CoordinateConverter.CartesianToSvg(new Vector2(0, 0));
        RefreshTrajectory(); // Recalculate the trajectory path
    }

    private void SetupAnimationTimer()
    {
        animationTimer?.Stop();
        animationTimer?.Dispose();
        deltaTimeMS = 1000f / Parameters.FrameRate;
        animationTimer = new(deltaTimeMS);
        animationTimer.Elapsed += UpdateProjectilePosition;
        animationTimer.AutoReset = true;

        if (!isAnimating) return;
        animationTimer.Start();
    }

    private void UpdateProjectilePosition(object source, ElapsedEventArgs e)
    {
        float deltaTimeInSeconds = deltaTimeMS / 1000f;
        simulationTime += deltaTimeInSeconds * Parameters.TimeScale;
        Vector2 newCartesianPosition = AnimationService.CalculatePositionAtTime(simulationTime, Parameters);
        currentPosition = CoordinateConverter.CartesianToSvg(newCartesianPosition);

        if (newCartesianPosition.Y < 0 || newCartesianPosition.X > SVG_WIDTH)
        {
            StopAnimation();
        }
        else
        {
            InvokeAsync(StateHasChanged);
        }
    }



    public void Dispose()
    {
        animationTimer?.Stop();
        animationTimer?.Dispose();
        Parameters.OnChange -= RefreshTrajectory;
    }
}