namespace App {
    public static class RomanNumExtension {
        public static RomanNum Plus(this RomanNum rn, params RomanNum[] other) => RomanNumMath.Plus([rn, .. other]);
    }
}