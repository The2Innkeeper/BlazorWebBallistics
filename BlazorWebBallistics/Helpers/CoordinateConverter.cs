using BlazorWebBallistics.Models;

namespace BlazorWebBallistics.Helpers;

public static class CoordinateConverter
{
    private const int SvgWidth = Configuration.SVG_WIDTH; // Assuming a fixed SVG width
    private const int SvgHeight = Configuration.SVG_HEIGHT; // Assuming a fixed SVG height

    // Converts a point from Cartesian coordinates to SVG coordinates
    public static Vector2 CartesianToSvg(Vector2 cartesianPoint)
    {
        return new Vector2(cartesianPoint.X, SvgHeight - cartesianPoint.Y);
    }

    // Converts a point from SVG coordinates to Cartesian coordinates
    public static Vector2 SvgToCartesian(Vector2 svgPoint)
    {
        return new Vector2(svgPoint.X, SvgHeight - svgPoint.Y);
    }

    // Add other conversion methods as needed
}