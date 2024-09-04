namespace App {
    public record RomanNum(int value) {
        private readonly int number = value;
        public int Number => number;
        public static RomanNum Parse(string input) {
            int value = 0, prevDigit = 0;
            foreach (char c in input.Reverse()) {
                int digit = DigitalValue(c.ToString());
                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }
            return new(value);
        }
        public static int DigitalValue(String digit) => digit switch {
            "N" => 0,
            "I" => 1,
            "V" => 5,
            "X" => 10,
            "L" => 50,
            "C" => 100,
            "D" => 500,
            "M" => 1000
        };
    }
    public class Program {
        public static void Main(string[] args) {
        
        }
    }
}