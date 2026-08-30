using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using PremBot.App.Enums;
using PremBot.App.Services;

namespace PremBot.App.Slash_Commands;

public class MatchDayFixture : ApplicationCommandModule
{
    [SlashCommand("matchDayFixture", "Shows the matches for the selected match day.")]
    public async Task MatchDayFixtureAsync(InteractionContext context,
        [Option("MatchDay", "Pick a match day (1-38)")]
        double matchDay)
    {
        var instance = PremService.GetInstance();
        var matchDayRound = Convert.ToInt32(Math.Floor(matchDay));
        var matchDayFixture = await instance.GetMatchesByMatchDay(matchDayRound);

        if (matchDayFixture.Count != 0)
        {
            var matchDayFixtureEmbed = new DiscordEmbedBuilder();

            foreach (var fixture in matchDayFixture)
            {
                matchDayFixtureEmbed.Title =
                    $"Match Day {Math.Floor(matchDay)} Fixture: {fixture.Season.StartDate.Substring(0, 4)}/" +
                    $"{fixture.Season.EndDate.Substring(0, 4)} ⚽ 🦁";
                matchDayFixtureEmbed.Color = DiscordColor.SpringGreen;
            }

            foreach (var fixture in matchDayFixture)
            {
                var easternTimeGame = TimeZoneInfo.ConvertTimeFromUtc(fixture.UtcDate,
                    TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

                matchDayFixtureEmbed.AddField(
                    $"{fixture.HomeTeam.Name} vs. {fixture.AwayTeam.Name}",
                    $"{easternTimeGame.ToString($"MMMM dd, h:mm tt")}\n", inline: true);
            }

            await context.CreateResponseAsync(matchDayFixtureEmbed, ephemeral: true);
        }
        else
        {
            var errorEmbed = new DiscordEmbedBuilder()
            {
                Title = "⚠️ No fixtures available at this time.",
                Color = DiscordColor.Red,
                Timestamp = DateTimeOffset.UtcNow
            };

            await context.CreateResponseAsync(errorEmbed, ephemeral: true);
        }
    }
}