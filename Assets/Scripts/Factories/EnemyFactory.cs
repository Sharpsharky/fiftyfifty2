public static class EnemyFactory
{
    public static float xOffsetEasy = 1f;
    public static float xOffsetMedium = 1.3f;
    public static float xOffsetHard = 1.6f;

    public static float GetSpawnOffsetX(Difficulty difficulty)
    {
        switch (difficulty) {
            case Difficulty.EASY:
                return xOffsetEasy;
            case Difficulty.MEDIUM:
                return xOffsetMedium;
            case Difficulty.HARD:
                return xOffsetHard;
        }
        throw new ColdCry.Exception.MissingTypeException( "Not implemented type: " + difficulty );
    }


}