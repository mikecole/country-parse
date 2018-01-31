using System;
using System.Globalization;
using System.Linq;
using CountryParse;
using Shouldly;

namespace CountryParseTests
{
    public class CountryTests
    {
        private static string[] NonCountryNames = new[] { "Caribbean", "Latin America", "World" };

        public void AllSpecificCultureCountries_ShouldBeRepresented()
        {
            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var regionInfo = new RegionInfo(cultureInfo.LCID);

                string countryName = regionInfo.EnglishName;

                //Filter out non-countries
                if (NonCountryNames.Contains(countryName))
                {
                    continue;
                }

                Country.TryParse(countryName, out Country country).ShouldBeTrue($"{countryName} not represented.");

                //Handle common mixups
                switch (countryName)
                {
                    case "Syria":
                        country.EnglishName.ShouldBe("Syrian Arab Republic");
                        break;
                    case "Russia":
                        country.EnglishName.ShouldBe("Russian Federation");
                        break;
                    case "United States":
                        country.EnglishName.ShouldBe("United States of America");
                        break;
                    case "United Kingdom":
                        country.EnglishName.ShouldBe("United Kingdom of Great Britain and Northern Ireland");
                        break;
                    case "Hong Kong SAR":
                        country.EnglishName.ShouldBe("Hong Kong");
                        break;
                    case "Bolivia":
                        country.EnglishName.ShouldBe("Bolivia (Plurinational State of)");
                        break;
                    case "Venezuela":
                        country.EnglishName.ShouldBe("Venezuela (Bolivarian Republic of)");
                        break;
                    case "Iran":
                        country.EnglishName.ShouldBe("Iran (Islamic Republic of)");
                        break;
                    case "Congo (DRC)":
                        country.EnglishName.ShouldBe("Congo (Democratic Republic of the)");
                        break;
                    case "Côte d’Ivoire":
                        country.EnglishName.ShouldBe("Côte d'Ivoire");
                        break;
                    case "Korea":
                        country.EnglishName.ShouldBe("Korea (Republic of)");
                        break;
                    case "Laos":
                        country.EnglishName.ShouldBe("Lao People's Democratic Republic");
                        break;
                    case "Macedonia, FYRO":
                        country.EnglishName.ShouldBe("Macedonia (the former Yugoslav Republic of)");
                        break;
                    case "Brunei":
                        country.EnglishName.ShouldBe("Brunei Darussalam");
                        break;
                    case "Moldova":
                        country.EnglishName.ShouldBe("Moldova (Republic of)");
                        break;
                    case "Vietnam":
                        country.EnglishName.ShouldBe("Viet Nam");
                        break;
                    case "Macao SAR":
                        country.EnglishName.ShouldBe("Macao");
                        break;
                    case "Taiwan":
                        country.EnglishName.ShouldBe("Taiwan, Province of China");
                        break;
                    default:
                        country.EnglishName.ShouldBe(countryName);
                        break;
                }

                country.ThreeLetterISOLanguageName.ShouldBe(regionInfo.ThreeLetterISORegionName);
                country.TwoLetterISOLanguageName.ShouldBe(regionInfo.TwoLetterISORegionName);
            }
        }

        public void AllSpecifiedCultureCountryThreeLetterAbbreviations_ShouldBeRepresented()
        {
            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var regionInfo = new RegionInfo(cultureInfo.LCID);

                //Filter out non-countries
                if (NonCountryNames.Contains(regionInfo.EnglishName))
                {
                    continue;
                }

                Country.TryParse(regionInfo.ThreeLetterISORegionName, out Country country).ShouldBeTrue($"{regionInfo.ThreeLetterISORegionName} not represented.");
                country.ThreeLetterISOLanguageName.ShouldBe(regionInfo.ThreeLetterISORegionName);
                country.TwoLetterISOLanguageName.ShouldBe(regionInfo.TwoLetterISORegionName);
            }
        }

        public void AllSpecifiedCultureCountryTwoLetterAbbreviations_ShouldBeRepresented()
        {
            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var regionInfo = new RegionInfo(cultureInfo.LCID);

                //Filter out non-countries
                if (NonCountryNames.Contains(regionInfo.EnglishName))
                {
                    continue;
                }

                Country.TryParse(regionInfo.TwoLetterISORegionName, out Country country).ShouldBeTrue($"{regionInfo.TwoLetterISORegionName} not represented.");
                country.ThreeLetterISOLanguageName.ShouldBe(regionInfo.ThreeLetterISORegionName);
                country.TwoLetterISOLanguageName.ShouldBe(regionInfo.TwoLetterISORegionName);
            }
        }

        public void TryParse_ThrowsArgumentNullException_WhenArgumentIsNull()
        {
            Should.Throw<ArgumentNullException>(() => Country.TryParse(null, out Country result));
        }

        public void TryParse_ReturnsTrue_WhenCountryIsFound()
        {
            Country.TryParse("USA", out Country result).ShouldBeTrue();
        }

        public void TryParse_ReturnsFalse_WhenCountryIsNotFound()
        {
            Country.TryParse("Latveria", out Country result).ShouldBeFalse();
        }

        public void Parse_ReturnsCountry_WhenCountryIsFound()
        {
            var usa = Country.Parse("USA");

            usa.ShouldNotBeNull();
            usa.EnglishName.ShouldBe("United States of America");
        }

        public void Parse_ReturnsThrowsFormatException_WhenCountryIsNotFound()
        {
            Should.Throw<FormatException>(() => Country.Parse("Foo"));
        }
    }
}