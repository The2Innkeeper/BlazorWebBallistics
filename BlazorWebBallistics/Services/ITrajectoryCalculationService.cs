// File: Services/ITrajectoryCalculationService.cs
using System;
using BlazorWebBallistics.Models;

namespace BlazorWebBallistics.Services;

public interface ITrajectoryCalculationService
{
    Vector2 CalculatePositionAtTime(float simulationTime, SimulationParameters parameters);
    string CalculatePath(SimulationParameters parameters, int pointCount = 100);
}