namespace App {
    public class RomanNumFactory {
        public static int DigitValue(string digit) => digit switch {
            "N" => 0,
            "I" => 1,
            "V" => 5,
            "X" => 10,
            "L" => 50,
            "C" => 100,
            "D" => 500,
            "M" => 1000,
            _ => throw new ArgumentException($"{nameof(RomanNum)}::{nameof(DigitValue)}: 'digit' имеет недопустимое значение '{digit}'")
        };
        public static RomanNum Parse(string input) => new(ParseAsInt(input));
        public static int ParseAsInt(string input) {
            int value = 0, rightDigit = 0;
            CheckValidity(input);
            foreach (char c in input.Reverse()) {
                int digit = DigitValue(c.ToString());
                value += digit >= rightDigit ? digit : -digit;
                rightDigit = digit;
            }
            return value;
        }
        private static void CheckValidity(string input) {
            CheckSubs(input);
            CheckPairs(input);
            CheckFormat(input);
            CheckSymbols(input);
            CheckSequence(input);
        }
        private static void CheckSubs(string input) {
            HashSet<char> subs = [];
            for (int i = 0; i < input.Length - 1; i++) {
                char c = input[i];
                if (DigitValue(c.ToString()) < DigitValue(input[i + 1].ToString())) {
                    if (subs.Contains(c)) throw new FormatException();
                    subs.Add(c);
                }
            }
        }
        private static void CheckPairs(string input) {
            for (int i = 0; i < input.Length - 1; ++i) {
                int rightDigit = DigitValue(input[i + 1].ToString()), leftDigit = DigitValue(input[i].ToString());
                if (leftDigit != 0 && leftDigit < rightDigit && (rightDigit / leftDigit > 10 || (leftDigit == 5 || leftDigit == 50 || leftDigit == 500)))
                    throw new FormatException($"Неверный порядок '{input[i]}' перед '{input[i + 1]}' в позиции {i}");
            }
        }
        private static void CheckFormat(string input) {
            int maxDigit = 0;
            bool wasLess = false, wasMax = false;
            foreach (char c in input.Reverse()) {
                int digit = DigitValue(c.ToString());
                if (digit < maxDigit) {
                    if (wasLess || wasMax) throw new FormatException(input);
                    wasLess = true;
                }
                else if (digit == maxDigit)wasMax = true;
                else {
                    maxDigit = digit;
                    wasLess = false;
                }
            }
        }
        private static void CheckSymbols(string input) {
            int pos = 0;
            foreach (char c in input){
                try { DigitValue(c.ToString()); }
                catch { throw new FormatException($"Недопустимый символ '{c}' в позиции {pos}"); }
                pos += 1;
            }
        }
        private static void CheckSequence(string input) {
            int digit = 0, rightDigit = 0, previousDigit = 0;
            foreach (char c in input.Reverse()) {
                try { digit = DigitValue(c.ToString()); }
                catch (ArgumentException ex) { }
                if (previousDigit > 0 && digit <= rightDigit && digit < previousDigit) 
                    throw new FormatException($"Неверная последовательность цифр: {c} меньше, чем {rightDigit} и {previousDigit}");
                previousDigit = rightDigit;
                rightDigit = digit;
            }
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
    }
}