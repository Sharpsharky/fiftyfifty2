[System.Serializable]
public struct Dimensions
{
    public Dimensions(float minH, float maxH, float minW, float maxW) : this()
    {
        MinH = minH;
        MaxH = maxH;
        MinW = minW;
        MaxW = maxW;
    }

    public float MinH { get; set; }
    public float MaxH { get; set; }
    public float MinW { get; set; }
    public float MaxW { get; set; }
}