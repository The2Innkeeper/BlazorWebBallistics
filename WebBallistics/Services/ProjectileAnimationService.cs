using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebBallistics.Services;

public class ProjectileAnimationService
{
    public double InitialSpeed { get; set; } = 50.0; // meters per second
    public double Angle { get; set; } = 45.0; // degrees
    public double Gravity { get; set; } = 9.81; // m/s^2

    public List<(double X, double Y)> CalculateTrajectory()
    {
        var trajectory = new List<(double X, double Y)>();
        double angleRad = Math.PI * Angle / 180.0;
        double totalTime = 2 * InitialSpeed * Math.Sin(angleRad) / Gravity;
        double timeStep = totalTime / 100; // Divide the total time into 100 steps

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
        var path = new System.Text.StringBuilder("M0,300"); // Start at middle of the SVG vertically
        double angleRad = (Math.PI / 180) * Angle;
        double totalTime = (2 * InitialSpeed * Math.Sin(angleRad)) / Gravity;
        double dt = totalTime / pointCount;

        for (double t = 0; t <= totalTime; t += dt)
        {
            double x = InitialSpeed * Math.Cos(angleRad) * t;
            // Invert y to accommodate SVG's top-left origin
            double y = 300 - (InitialSpeed * Math.Sin(angleRad) * t - 0.5 * Gravity * Math.Pow(t, 2));
            path.Append($" L{x.ToString("F2")},{y.ToString("F2")}");
        }

        return path.ToString();
    }
}
