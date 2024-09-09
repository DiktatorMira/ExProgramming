using System.Text;

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

                if (digit != 0 && prevDigit / digit > 10) throw new FormatException($"Неверный порядок '{c}' перед '{input[pos + 1]}' в позиции {pos}");
                if (prevDigit > digit &&!((digit == 1 && (prevDigit == 5 || prevDigit == 10)) ||(digit == 10 && (prevDigit == 50 || prevDigit == 100)) || (digit == 100 && (prevDigit == 500 || prevDigit == 1000)))) {
                    throw new FormatException($"Недопустимая комбинация '{c}' в позиции {pos}");
                }

                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }
            if (errors.Any()) throw new FormatException(string.Join("; ", errors));
            return new(value);
        }
        public static int DigitalValue(string digit) => digit switch {
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
        public override string ToString() {
            if (number == 0) return "N";
            Dictionary<int, string> parts = new() {
                {1 , "I" },
                {5 , "V" },
                {10 , "X"},
                {50 , "L"},
                {100 , "C" },
                {500 , "D" },
                {1000 , "M"}
            };
            int val = value;
            StringBuilder sb = new ();
            foreach (var part in parts) {
                while(val >= part.Key) {
                    val -= part.Key;
                    sb.Append(part.Value);
                }
            }
            return sb.ToString();
        }
    }
    public class Program {
        public static void Main(string[] args) {
        
        }
    }
}