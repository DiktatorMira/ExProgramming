namespace App {
    public record RomanNum(int value) {
        private readonly int number = value;
        public int Number => number;
        public static RomanNum Parse(string input){
            int value = 0, prevDigit = 0, pos = input.Length;
            List<string> errors = new();
            foreach (char c in input.Reverse()) {
                pos -= 1;
                int digit;
                try { digit = DigitalValue(c.ToString()); }
                catch {
                    errors.Add($"Недопустимый символ '{c}' в позиции {pos}");
                    continue;
                }
                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }
            if (errors.Any()) throw new FormatException(string.Join("; ", errors));
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
            "M" => 1000,
            "W" => 5000,
            _ => throw new ArgumentException($"{nameof(RomanNum)} :: {nameof(DigitalValue)}: «digit» имеет недопустимое значение «{digit}»")
        };
    }
    public class Program {
        public static void Main(string[] args) {
        
        }
    }
}