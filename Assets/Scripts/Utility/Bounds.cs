public struct Bounds
{
    private static readonly Bounds rZero = new Bounds( 0f, 0f );

    public Bounds(float left, float right)
    {
        Left = left;
        Right = right;
    }

    public Bounds(float doubled)
    {
        Left = doubled;
        Right = doubled;
    }

    public static Bounds operator +(Bounds a, Bounds b)
    {
        return new Bounds( a.Left + b.Left, a.Right + b.Right );
    }

    public static Bounds operator -(Bounds a, Bounds b)
    {
        return new Bounds( a.Left - b.Left, a.Right - b.Right );
    }

    public static Bounds operator* (Bounds a, float f)
    {
        return new Bounds( a.Left * f, a.Right * f );
    }

    public override string ToString()
    {
        return "[Left = " + Left + ", Right = " + Right + "]";
    }

    public float Left { get; set; }
    public float Right { get; set; }
    public static Bounds Zero { get => rZero; }
}