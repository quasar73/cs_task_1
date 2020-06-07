using System;
using System.Security.Cryptography;
using System.Text;

namespace cs1
{
	// ИСКЛЮЧЕНИЕ
	// ИСКЛЮЧЕНИЕ
	// ИСКЛЮЧЕНИЕ
	// ИСКЛЮЧЕНИЕ
	// ИСКЛЮЧЕНИЕ
	// ИСКЛЮЧЕНИЕ
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 3 || args.Length % 2 == 0)
				throw new ArgumentException("Wrong arguments!");
			
			byte[] key = GenerateKey();
			Random random = new Random();

			int computerChoice = random.Next(args.Length),
				humanChoice;

			var hmac = new HMACSHA256(key);
			byte[] buffer = Encoding.UTF8.GetBytes(args[computerChoice]);
			byte[] hmacResult = hmac.ComputeHash(buffer);

			while (true)
			{
				Console.Clear();
				Console.WriteLine($"HMAC: {ConvertByteToString(hmacResult)}");
				Console.WriteLine("Available moves: ");

				for(int i = 0; i < args.Length; i++)
				{
					Console.WriteLine($"{i + 1} - {args[i]}");
				}
				Console.WriteLine("0 - exit");
				Console.Write("Enter your move: ");

				bool isOk = Int32.TryParse(Console.ReadLine(), out humanChoice);

				if (!isOk || humanChoice < 0 || humanChoice > args.Length)
					continue;
				else if (humanChoice == 0)
					return;

				break;
			}
			humanChoice--;

			Console.WriteLine($"Your move: {args[humanChoice]}");
			Console.WriteLine($"Computer move: {args[computerChoice]}");

			if((humanChoice + 1) % args.Length == computerChoice)
				Console.WriteLine("Computer win!");
			else if (humanChoice == (computerChoice + 1) % args.Length)
				Console.WriteLine("You win!");
			else
				Console.WriteLine("Draw!");

			Console.WriteLine($"HMAC key: {ConvertByteToString(key)}");

		}

		static byte[] GenerateKey()
		{
			byte[] key = new byte[32];

			RandomNumberGenerator rng = RNGCryptoServiceProvider.Create();
			rng.GetBytes(key);

			return key;
		}

		static string ConvertByteToString(byte[] arr)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var b in arr)
				builder.Append(b.ToString("x2"));

			return builder.ToString();
		}
	}
}
