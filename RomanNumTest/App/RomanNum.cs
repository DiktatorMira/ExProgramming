namespace App {
    public record RomanNum (int value) {
        private readonly int number = value;
        public int Number => number;
        public static RomanNum Parse(string input)  {
            if (string.IsNullOrWhiteSpace(input)) return new RomanNum(0);
            var numConverter = new Dictionary<char, int> {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };
            int total = 0, prevalue = 0;
            foreach (char c in input.ToUpper()) {
                if (numConverter.TryGetValue(c, out int value)) {
                    if (value > prevalue) total += value - 2 * prevalue;
                    else total += value;
                    prevalue = value;
                }
                else throw new ArgumentException($"Недопустимый символ римской цифры: {c}");
            }
            return new RomanNum(total);
        }
    }
    public class Program {
        public static void Main(string[] args) {
        
        }
    }
}