// File: Services/TrajectoryCalculationService.cs
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebBallistics.Models;

namespace WebBallistics.Services;

public class TrajectoryCalculationService_ClosedSystem : ITrajectoryCalculationService
{
    public Vector2 CalculatePositionAtTime(float simulationTime, SimulationParameters parameters)
    {
        float angleRad = MathF.PI * parameters.Angle / 180f;
        float x = parameters.InitialSpeed * MathF.Cos(angleRad) * simulationTime;
        float y = parameters.InitialSpeed * MathF.Sin(angleRad) * simulationTime - 0.5f * parameters.Gravity * simulationTime * simulationTime;

        return new Vector2(x, y);
    }

    public string CalculatePath(SimulationParameters parameters, int pointCount = 100)
    {
        var path = new StringBuilder();
        float angleRad = (MathF.PI / 180f ) * parameters.Angle;
        float totalTime = (2f * parameters.InitialSpeed * MathF.Sin(angleRad)) / parameters.Gravity;
        float dt = totalTime / pointCount;
        int SVG_HEIGHT = 600;

        for (float t = 0; t <= totalTime; t += dt)
        {
            float x = parameters.InitialSpeed * MathF.Cos(angleRad) * t;
            float y = SVG_HEIGHT - (parameters.InitialSpeed * MathF.Sin(angleRad) * t - 0.5f * parameters.Gravity * MathF.Pow(t, 2));
            if (t == 0)
            {
                path.AppendFormat("M {0:F2} {1:F2}", x, y);
            }
            else
            {
                path.AppendFormat(" L {0:F2} {1:F2}", x, y);
            }
        }

        return path.ToString();
    }
}