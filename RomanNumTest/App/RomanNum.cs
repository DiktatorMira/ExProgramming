using System.Text;

namespace App {
    public record RomanNum(int value) {
        private readonly int number = value;
        public int Number {
            get => number;
            init { if(number == 0) number = value; }
        }
        public static RomanNum Parse(string input){
            if (string.IsNullOrEmpty(input)) throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: Входная строка пуста или имеет значение null.");

            int value = 0, prevDigit = 0, pos = input.Length;
            CheckValidity(input);
            foreach (char c in input.Reverse()) {
                pos -= 1;
                int digit = DigitalValue(c.ToString());

                try { digit = DigitalValue(c.ToString()); }
                catch { throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: Недопустимый символ '{c}' в позиции {pos}"); }

                if (prevDigit > digit && !((digit == 1 && (prevDigit == 5 || prevDigit == 10)) ||
                    (digit == 10 && (prevDigit == 50 || prevDigit == 100)) || (digit == 100 && (prevDigit == 500 || prevDigit == 1000)))) {
                    throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: Недопустимая комбинация '{c}' в позиции {pos}");
                }

                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }
            return new(value);
        }
        private static void CheckSubs(string input) {
            HashSet<char> subs = new HashSet<char>();
            Dictionary<char, int> counts = new Dictionary<char, int>();

            for (int i = 0; i < input.Length; i++)  {
                char current = input[i];
                if (!counts.ContainsKey(current)) counts[current] = 0;
                counts[current]++;

                if (i < input.Length - 1) {
                    char next = input[i + 1];
                    if ((current == 'I' || current == 'X' || current == 'C') &&
                        DigitalValue(current.ToString()) < DigitalValue(next.ToString())) {
                        if (!((current == 'I' && (next == 'V' || next == 'X')) ||
                              (current == 'X' && (next == 'L' || next == 'C')) ||
                              (current == 'C' && (next == 'D' || next == 'M')))) 
                            throw new FormatException($"Недопустимая вычитающая пара: {current}{next}");
                        if (subs.Contains(current)) throw new FormatException($"Повторяющаяся вычитательная нотация: {current}");
                        if (counts[current] > 1) throw new FormatException($"Недопустимое повторение перед вычитательной записью: {current}");
                        subs.Add(current);
                        i++;
                    }
                }
            }
        }
        private static void CheckFormat(string input) {
            int maxDigit = 0;
            Dictionary<char, int> counts = new Dictionary<char, int>();
            bool hasLesserDigit = false;

            foreach (char c in input.Reverse()) {
                int digit = DigitalValue(c.ToString());
                if (!counts.ContainsKey(c)) counts[c] = 0;
                counts[c]++;
                if ((c == 'I' || c == 'X' || c == 'C' || c == 'M') && counts[c] > 3) throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: Недопустимое повторение '{c}'");
                if ((c == 'V' || c == 'L' || c == 'D') && counts[c] > 1) throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: Недопустимое повторение '{c}'");

                if (digit < maxDigit) {
                    if (hasLesserDigit) throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: недопустимая последовательность: более 1 меньшей цифры перед '{input[^1]}'");
                    hasLesserDigit = true;
                } else if (digit > maxDigit) {
                    maxDigit = digit;
                    hasLesserDigit = false;
                }
            }
        }
        private static void CheckPairs(string input) {
            for (int i = 0; i < input.Length - 1; i++) {
                int rightDigit = DigitalValue(input[i + 1].ToString()), leftDigit = DigitalValue(input[i].ToString());
                if (leftDigit != 0 && leftDigit < rightDigit && (rightDigit / leftDigit > 10 || (leftDigit == 5 || leftDigit == 50 || leftDigit == 500))) {
                    throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: Неверный порядок '{input[i]}' перед '{input[i + 1]}' в позиции {i}");
                }
            }
        }
        private static void CheckValidity(string input) {
            CheckSymbols(input);
            CheckPairs(input);
            CheckFormat(input);
            CheckSubs(input);
        }
        private static void CheckSymbols(string input) {
            int pos = 0;
            foreach (char c in input) {
                try { DigitalValue(c.ToString()); }
                catch { throw new FormatException($"{nameof(RomanNum)}.{nameof(Parse)}: Недопустимый символ '{c}' в позиции {pos}"); }
            }
        }
        private static int method1() => 1;
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
        public RomanNum Plus(RomanNum val) => this with { Number = Number + val.Number };
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
}