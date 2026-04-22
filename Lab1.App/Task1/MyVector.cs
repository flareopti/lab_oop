using System;
using System.Globalization;

namespace Lab1.App.Task1;

public sealed class MyVector : IEquatable<MyVector>
{
    public double DirectionX { get; }
    public double DirectionY { get; }

    public MyVector(double directionX, double directionY)
    {
        if (!IsFinite(directionX))
        {
            throw new ArgumentOutOfRangeException(nameof(directionX), directionX, "DirectionX must be a finite number.");
        }

        if (!IsFinite(directionY))
        {
            throw new ArgumentOutOfRangeException(nameof(directionY), directionY, "DirectionY must be a finite number.");
        }

        DirectionX = directionX;
        DirectionY = directionY;
    }

    public override string ToString() => $"({Format(DirectionX)}, {Format(DirectionY)})";

    public bool Equals(MyVector? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return DirectionX.Equals(other.DirectionX) && DirectionY.Equals(other.DirectionY);
    }

    public override bool Equals(object? obj) => obj is MyVector other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(DirectionX, DirectionY);

    public static MyVector operator +(MyVector left, MyVector right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return new MyVector(left.DirectionX + right.DirectionX, left.DirectionY + right.DirectionY);
    }

    public static MyVector operator -(MyVector left, MyVector right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return new MyVector(left.DirectionX - right.DirectionX, left.DirectionY - right.DirectionY);
    }

    // Dot product.
    public static double operator *(MyVector left, MyVector right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return (left.DirectionX * right.DirectionX) + (left.DirectionY * right.DirectionY);
    }

    // Scalar multiplication.
    public static MyVector operator *(MyVector vector, double scalar)
    {
        ArgumentNullException.ThrowIfNull(vector);

        if (!IsFinite(scalar))
        {
            throw new ArgumentOutOfRangeException(nameof(scalar), scalar, "Scalar must be a finite number.");
        }

        return new MyVector(vector.DirectionX * scalar, vector.DirectionY * scalar);
    }

    public static MyVector operator *(double scalar, MyVector vector) => vector * scalar;

    // Vector length.
    public static double operator +(MyVector vector)
    {
        ArgumentNullException.ThrowIfNull(vector);

        var x = vector.DirectionX;
        var y = vector.DirectionY;
        return Math.Sqrt((x * x) + (y * y));
    }

    public static bool operator ==(MyVector? left, MyVector? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(MyVector? left, MyVector? right) => !(left == right);

    private static bool IsFinite(double value) => !(double.IsNaN(value) || double.IsInfinity(value));

    private static string Format(double value) => value.ToString("G", CultureInfo.InvariantCulture);
}
