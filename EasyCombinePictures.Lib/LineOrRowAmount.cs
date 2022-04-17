using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EasyCombinePictures
{
	public class LineOrRowAmount
	{
		public LineOrRow LineOrRow { get; set; }

		public int Amount { get; set; }

		public static LineOrRowAmount Parse(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new LineOrRowAmount()
				{
					LineOrRow = LineOrRow.Default,
					Amount = 1
				};
			}

			Match match = Regex.Match(value, "^.*?([RrLl])([\\d]*).*?$");
			if (match.Success)
			{
				LineOrRowAmount r = new LineOrRowAmount();
				switch (match.Groups[1].Value[0])
				{
					case 'L':
					case 'l':
						r.LineOrRow = LineOrRow.Line;
						break;
					case 'R':
					case 'r':
						r.LineOrRow = LineOrRow.Row;
						break;
					default:
						throw new Exception("Unknown error.");
				}
				if (match.Groups[2].Value == string.Empty)
				{
					r.Amount = 1;
				}
				else
				{
					r.Amount = int.Parse(match.Groups[2].Value);
					if (r.Amount < 1)
					{
						throw new FormatException("Syntax error: the number should bigger than 0.");
					}
				}
				return r;
			}

			throw new FormatException("Syntax error:.");
		}

		public override string ToString()
		{
			string r;
			switch (LineOrRow)
			{
				case LineOrRow.Default:
					return "";
				case LineOrRow.Line:
					r = "l";
					break;
				case LineOrRow.Row:
					r = "r";
					break;
				default:
					throw new Exception("Unknown error.");
			}
			r += Amount.ToString();
			return r;
		}
	}
}
