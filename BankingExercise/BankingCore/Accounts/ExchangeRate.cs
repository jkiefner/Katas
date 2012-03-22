using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingCore.Accounts
{
	public class ExchangeRate
	{
		public static decimal GetExchangeRateFactor(CurrencyType currencyType)
		{
			switch (currencyType)
			{
				case CurrencyType.USDollar:
					return 1M;
				case CurrencyType.Euro:
					return 0.5M;
				default:
					return 1M;
			}
		}
	}
}
