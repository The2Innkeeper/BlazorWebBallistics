namespace OldWebBallistics.Models;

public struct Vector2(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;

    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator *(Vector2 a, float scalar) => new Vector2(a.X * scalar, a.Y * scalar);
    public static Vector2 operator /(Vector2 a, float scalar) => new Vector2(a.X / scalar, a.Y / scalar);

    public float Magnitude => MathF.Sqrt(X * X + Y * Y);
    public float SquareMagnitude => X * X + Y * Y;

    public Vector2 Normalize() => this / Magnitude;

    public float Dot(Vector2 other) => X * other.X + Y * other.Y;

    public float AngleTo(Vector2 other)
    {
        float dot = Dot(other);
        float magnitudeProduct = Magnitude * other.Magnitude;
        return MathF.Acos(dot / magnitudeProduct);
    }

    public static float Distance(Vector2 a, Vector2 b) => (a - b).Magnitude;

    public static Vector2 Lerp(Vector2 a, Vector2 b, float t) => a + (b - a) * t;

    public Vector2 Reflect(Vector2 vector2)
    {
        Vector2 normalized = vector2.Normalize();
        float dot = Dot(normalized);
        return this - (normalized * dot * 2);
    }

    public Vector2 Rotate(float angle)
    {
        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);
        return new Vector2(X * cos - Y * sin, X * sin + Y * cos);
    }
}