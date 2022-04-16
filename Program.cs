using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program {
	public static void Main() {
		GameLoop();
	}

	public static void GameLoop() {
		Console.WriteLine("===== Which Date =====");

		// Flag für die Programmschleife. Das Programm läuft, solange der Variablenwert true bleibt.
		bool programLoop = true;

		// Programmschleife
		while(programLoop) {
			Console.Write("Geben Sie bitte das Datum im Format 'DD - MM - YYYY' ein: ");
			string stringDate = Console.ReadLine();
			// Eingabe auf Format 'dd-mm-yyyy' oder 'dd - mm - yyyy' überprüfen.
			if(ValidateString(stringDate)) {
				// Wochentag berechnen
				Console.WriteLine(
					"Der " + stringDate + " ist ein " + CalculateDate(stringDate) + ".");
			} else {
				// andernfalls: Fehlermeldung ausgeben
				Console.WriteLine("Geben Sie das Datum bitte im korrekten Format ein!");
			}
			// Erfragen, ob das Programm beendet werden soll.
			Console.Write("Möchten Sie ein weiteres Datum ermitteln lassen? J / N: ");
			programLoop = Console.ReadLine().ToLower() == "j"? true : false;
		}
		Console.WriteLine("Bis bald.");
	}

	public static bool ValidateString(string candidate){
		return new Regex("^\\d{1,2}(-| - )\\d{1,2}(-| - )\\d{1,4}$").IsMatch(candidate);
	}

	public static string CalculateDate(string stringDate) {
		// Tagesdatum
		int day;

		List<string> weekdayMapping = new List<string> {"Sonntag", "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag"};

		int month;

		/**
		 * Monate, julianischer Kalender
		 * Format: März = 1 | April = 2 | Mai = 3 | Juni = 4 | Juli = 5 | August = 6 | September = 7 | Oktober = 8 | November = 9 | Dezember = 10 | Januar = 11 | Februar = 12
		 **/
		List<int> monthMapping = new List<int> {11, 12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

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


		string result = "";
		// Eingabe am Trennzeichen '-' aufteilen und whitespace entfernen.
		string[] dateString = stringDate.Split('-');

		for(int i = 0; i < dateString.Length; ++i) {
			dateString[i] = dateString[i].Trim();
		}

		// Tag formatieren
		day = Convert.ToInt32(dateString[0]);

		// Monat formatieren
		month = monthMapping[Convert.ToInt32(dateString[1])-1];

		// Jahrzehnt und Jahrhundert berechnen
		int year = Convert.ToInt32(dateString[2]);

		// Für Jan und Feb die Werte des Vorjahres verwenden.
		if(month > 10 && year > 0) {
			--year;
		}

		// Jahrhundert errechnen.
		century = year/100;

		// Jahrzehnt errechnen.
		decade = year%100;

		/**
		 * Wochentag berechnen
		 * https://de.wikipedia.org/wiki/Wochentagsberechnung#Formel
		 **/
		int weekday = Convert.ToInt32(
				(day + Math.Floor(2.6*month - 0.2) + decade + Math.Floor(decade/4.0) + Math.Floor(century/4.0) - 2*century + 7 ) % 7
			);
		return weekdayMapping[weekday];
	}
}
