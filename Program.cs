using System;
using System.Text.RegularExpressions;

public class Program {
	public static void Main() {
		Console.WriteLine("===== Which Date =====");

		// Tagesdatum
		int day;

		string[] weekdayMapping = new string[7] {"Sonntag", "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag"};

		/**
		 * Monat, julianischer Kalender
		 * Format: März = 1 | April = 2 | Mai = 3 | Juni = 4 | Juli = 5 | August = 6 | September = 7 | Oktober = 8 | November = 9 | Dezember = 10 | Januar = 11 | Februar = 12
		 **/
		int month;

		int[] monthMapping = new int[] { 0, 11, 12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

		/**
		 * Jahrzehnt
		 * Für Jan und Feb die Werte des Vorjahres.
		 **/
		int decade;

		/**
		 * Jahrhundert
		 * Für Jan und Feb die Werte des Vorjahres.
		 **/
		int century;

		Console.Write("Geben Sie bitte das Datum im Format 'DD - MM - YYYY' ein: ");
		string stringDate = Console.ReadLine();

		// Eingabe auf Format 'dd-mm-yyyy' oder 'dd - mm - yyyy' überprüfen.
		if(new Regex("^\\d{1,2}(-| - )\\d{1,2}(-| - )\\d{1,4}$").IsMatch(stringDate)) {
			// Eingabe am Trennzeichen '-' aufteilen und whitespace entfernen.
			string[] dateString = stringDate.Split('-');

			for(int i = 0; i < dateString.Length; ++i) {
				dateString[i] = dateString[i].Trim();
			}

			// Tag formatieren
			day = Convert.ToInt32(dateString[0]);

			// Monat formatieren
			month = monthMapping[Convert.ToInt32(dateString[1])];

			// Jahrzehnt und Jahrhundert berechnen
			int year = Convert.ToInt32(dateString[2]);

			// Für Jan und Feb die Werte des Vorjahres verwenden.
			if(month > 10 && year > 0) {
				--year;
			}

			century = year/100;
			decade = year%100;

			Console.WriteLine(day);Console.WriteLine(month);Console.WriteLine(decade);Console.WriteLine(century);

			/**
			 * calculating day of week
			 * https://de.wikipedia.org/wiki/Wochentagsberechnung#Formel
			 **/
			int weekday = Convert.ToInt32((day + Math.Floor(2.6*month - 0.2) + decade + Math.Floor(decade/4.0) + Math.Floor(century/4.0) - 2*century) % 7);
			if(weekday < 0) {
				weekday += 7;
			}

			Console.WriteLine("Der {0}.{1}.{2} ist ein {3}", dateString[0], dateString[1], dateString[2], weekdayMapping[weekday]);
		} else {
			Console.WriteLine("Die Anwendung wurde beendet.");
			Console.WriteLine("Geben Sie das Datum bitte im korrekten Format ein!");
		}


	}
}

// todo: Fehler bei 01-01-1001, 12-1-1