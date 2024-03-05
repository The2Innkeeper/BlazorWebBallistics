// File: Services/ITrajectoryCalculationService.cs
using System;
using WebBallistics.Models;

namespace WebBallistics.Services;

public interface ITrajectoryCalculationService
{
    Vector2 CalculatePositionAtTime(float simulationTime, SimulationParameters parameters);
    string CalculatePath(SimulationParameters parameters, int pointCount = 100);
}