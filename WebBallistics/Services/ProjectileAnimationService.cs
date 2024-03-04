using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebBallistics.Services;

public class ProjectileAnimationService
{
    public double InitialSpeed { get; set; } = 50.0; // meters per second
    public double Angle { get; set; } = 45.0; // degrees
    public double Gravity { get; set; } = 9.81; // m/s^2

    public List<(double X, double Y)> CalculateTrajectory(int pointCount = 100)
    {
        var trajectory = new List<(double X, double Y)>();
        double angleRad = Math.PI * Angle / 180.0;
        double totalTime = 2 * InitialSpeed * Math.Sin(angleRad) / Gravity;
        double timeStep = totalTime / pointCount; // Divide the total time into 100 steps

        for (double t = 0; t <= totalTime; t += timeStep)
        {
            double x = InitialSpeed * Math.Cos(angleRad) * t;
            double y = InitialSpeed * Math.Sin(angleRad) * t - 0.5 * Gravity * t * t;
            trajectory.Add((x, y));
        }

        return trajectory;
    }

    

    public string CalculatePath(int pointCount = 100)
    {
        var path = new System.Text.StringBuilder(); // Start at bottom left
        double angleRad = (Math.PI / 180) * Angle;
        double totalTime = (2 * InitialSpeed * Math.Sin(angleRad)) / Gravity;
        double dt = totalTime / pointCount;

        for (double t = 0; t <= totalTime; t += dt)
        {
            double x = InitialSpeed * Math.Cos(angleRad) * t;
            // Invert y to accommodate SVG's top-left origin
            int SVG_HEIGHT = 600;
            double y = SVG_HEIGHT - (InitialSpeed * Math.Sin(angleRad) * t - 0.5 * Gravity * Math.Pow(t, 2));
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
