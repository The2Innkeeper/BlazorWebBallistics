namespace WebBallistics.Models;

public struct Vector2(double x, double y)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;

    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator *(Vector2 a, double scalar) => new Vector2(a.X * scalar, a.Y * scalar);
    public static Vector2 operator /(Vector2 a, double scalar) => new Vector2(a.X / scalar, a.Y / scalar);

    public double Magnitude => Math.Sqrt(X * X + Y * Y);
    public double SquareMagnitude => X * X + Y * Y;

    public Vector2 Normalize() => this / Magnitude;

    // Additional methods as needed, e.g., DotProduct, AngleTo, etc.
}