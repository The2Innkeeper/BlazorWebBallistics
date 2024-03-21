// File: Services/ITrajectoryCalculationService.cs
using System;
using OldWebBallistics.Models;

namespace OldWebBallistics.Services;

public interface ITrajectoryCalculationService
{
    Vector2 CalculatePositionAtTime(float simulationTime, SimulationParameters parameters);
    string CalculatePath(SimulationParameters parameters, int pointCount = 100);
}