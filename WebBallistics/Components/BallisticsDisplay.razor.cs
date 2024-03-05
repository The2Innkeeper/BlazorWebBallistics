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

    private const int SVG_WIDTH = Configuration.SVG_WIDTH;
    private const int SVG_HEIGHT = Configuration.SVG_HEIGHT;
    private string trajectoryPath;
    private Vector2 currentPosition = new(SVG_HEIGHT, 0);
    private System.Timers.Timer animationTimer;
    private DateTime animationStartTime;
    private bool isAnimating = false;
    private float simulationTime = 0f;
    private float deltaTime = 0.02f;


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
        currentPosition = CartesianToSvgCoordinates(new Vector2(0, 0));
        RefreshTrajectory(); // Recalculate the trajectory path
    }

    private void SetupAnimationTimer()
    {
        animationTimer?.Stop();
        animationTimer?.Dispose();
        deltaTime = 1f / Parameters.FrameRate;
        animationTimer = new(deltaTime);
        animationTimer.Elapsed += UpdateProjectilePosition;
        animationTimer.AutoReset = true;
        animationTimer.Start();
    }

    // Converts a point from Cartesian coordinates to SVG coordinates
    private Vector2 CartesianToSvgCoordinates(Vector2 cartesianPoint)
    {
        return new Vector2(cartesianPoint.X, SVG_HEIGHT - cartesianPoint.Y);
    }


    private void UpdateProjectilePosition(object source, ElapsedEventArgs e)
    {
        simulationTime += deltaTime * Parameters.TimeScale;
        Vector2 newCartesianPosition = AnimationService.CalculatePositionAtTime(simulationTime, Parameters);
        currentPosition = CartesianToSvgCoordinates(newCartesianPosition);

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