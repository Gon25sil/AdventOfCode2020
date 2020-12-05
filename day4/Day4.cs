using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.day4
{
    public class Day4
    {
        private string input;
        private string[] requiredFields;
        private string[] eyeColor;
        public Day4()
        {
            Console.WriteLine("\n ****** DAY 4 ******");

            input = File.ReadAllText("day4/day4.txt");
            requiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            eyeColor = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            
        }

        /*
         The automatic passport scanners are slow because they're having trouble detecting which passports have all required fields. The expected fields are as follows:
            byr (Birth Year)
            iyr (Issue Year)
            eyr (Expiration Year)
            hgt (Height)
            hcl (Hair Color)
            ecl (Eye Color)
            pid (Passport ID)
            cid (Country ID)
        Passport data is validated in batch files (your puzzle input). Each passport is represented as a sequence of key:value pairs separated by spaces or newlines. Passports are separated by blank lines.
        Here is an example batch file containing four passports:
            ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
            byr:1937 iyr:2017 cid:147 hgt:183cm

            iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
            hcl:#cfa07d byr:1929

            hcl:#ae17e1 iyr:2013
            eyr:2024
            ecl:brn pid:760753108 byr:1931
            hgt:179cm

            hcl:#cfa07d eyr:2025 pid:166559648
            iyr:2011 ecl:brn hgt:59in
        
        The first passport is valid - all eight fields are present. The second passport is invalid - it is missing hgt (the Height field).
        The third passport is interesting; the only missing field is cid, so it looks like data from North Pole Credentials, not a passport at all! Surely, nobody would mind if you made the system temporarily ignore missing cid fields. Treat this "passport" as valid.
        The fourth passport is missing two fields, cid and byr. Missing cid is fine, but missing any other field is not, so this passport is invalid.
        According to the above rules, your improved system would report 2 valid passports.
         */
        public void SolveA()
        {
            Console.WriteLine("-----Part1-----");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            string[] passports = input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            int result = 0;

            foreach (var item in passports)
            {
                if (requiredFields.All(x => item.Contains(x)))
                    result++;

            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedTicks = watch.ElapsedTicks;

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {elapsedMs}");
            Console.WriteLine($"Elapsed ticks: {elapsedTicks}");
        }


        /*
         The line is moving more quickly now, but you overhear airport security talking about how passports with invalid data are getting through. Better add some data validation, quick!
         You can continue to ignore the cid field, but each other field has strict rules about what values are valid for automatic validation:
            byr (Birth Year) - four digits; at least 1920 and at most 2002.
            iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            hgt (Height) - a number followed by either cm or in:
            If cm, the number must be at least 150 and at most 193.
            If in, the number must be at least 59 and at most 76.
            hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            pid (Passport ID) - a nine-digit number, including leading zeroes.
            cid (Country ID) - ignored, missing or not.
        Your job is to count the passports where all required fields are both present and valid according to the above rules
         */
        public void SolveB()
        {
            Console.WriteLine("-----Part2-----");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            string[] passports = input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            int result = 0;
            bool fieldIsValid = false;
            bool passportIsValid = true;
            string value;

            foreach (var passport in passports)
            {
                passportIsValid = true;

                foreach (var requiredField in requiredFields)
                {
                    var matchResult = Regex.Match(passport, $@"{requiredField}:(.*?)(\s|$)");
                    if (!matchResult.Success)
                    {
                        passportIsValid = false;
                        break;
                    }

                    value = matchResult.Groups[1].Value;

                    switch (requiredField)
                    {
                        case "byr":
                            fieldIsValid = isbyrValid(value);
                            break;
                        case "iyr":
                            fieldIsValid = isiyrValid(value);
                            break;
                        case "eyr":
                            fieldIsValid = iseyrValid(value);
                            break;
                        case "hgt":
                            fieldIsValid = ishgtValid(value);
                            break;
                        case "hcl":
                            fieldIsValid = ishclValid(value);
                            break;
                        case "ecl":
                            fieldIsValid = iseclValid(value);
                            break;
                        case "pid":
                            fieldIsValid = ispidValid(value);
                            break;
                        default:
                            fieldIsValid = false;
                            break;
                    }
                    passportIsValid = passportIsValid && fieldIsValid;
                    
                    if (!passportIsValid) //if one field is invalid, we do not need to continue the loop
                        break;
                }

                if (passportIsValid)
                    result++;

            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedTicks = watch.ElapsedTicks;

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {elapsedMs}");
            Console.WriteLine($"Elapsed ticks: {elapsedTicks}");
        }

        public bool isbyrValid(string year)
        {
            if (Int32.TryParse(year, out int convertedYear))
            {
                return convertedYear >= 1920 && convertedYear <= 2002;
            }
            return false;
        }
        public bool isiyrValid(string year)
        {
            if (Int32.TryParse(year, out int convertedYear))
            {
                return convertedYear >= 2010 && convertedYear <= 2020;
            }
            return false;
        }
        public bool iseyrValid(string year)
        {
            if (Int32.TryParse(year, out int convertedYear))
            {
                return convertedYear >= 2020 && convertedYear <= 2030;
            }
            return false;
        }
        public bool ishgtValid(string heightWithUnit)
        {
            var match = Regex.Match(heightWithUnit, @"(\d+)([a-z]+)");

            Int32.TryParse(match.Groups[1].Value, out int height);
            string unit = match.Groups[2].Value;

            return (unit =="cm" && height >= 150 && height <= 193) || (unit == "in" && height >= 59 && height <= 76);
        }

        public bool ishclValid(string color)
        {
            return Regex.IsMatch(color, @"#[A-Za-z0-9]{6}");
        }
        public bool iseclValid(string color)
        {
            return eyeColor.Any(x => x == color);
        }
        public bool ispidValid(string id)
        {
            return Regex.IsMatch(id, @"^\d{9}$");
        }
    }
}
