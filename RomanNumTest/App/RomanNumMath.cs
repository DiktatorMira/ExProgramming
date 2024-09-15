namespace App {
    public class RomanNumMath {
        public static RomanNum Plus(params RomanNum[] args) => new(args.Sum(r => r.value));
    }
}