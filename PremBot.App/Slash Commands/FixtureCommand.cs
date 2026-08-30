using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using PremBot.App.Enums;
using PremBot.App.Services;

namespace PremBot.App.Slash_Commands;

public class FixtureCommand : ApplicationCommandModule
{
    [SlashCommand("teamFixture", "Displays the matches for the corresponding team.")]
    public async Task FixtureCommandAsync(InteractionContext context,
        [Option("Team", "Fixtures for a team")]
        Team team)
    {
        var instance = PremService.GetInstance();
        var fixtures = await instance.GetMatches(Convert.ToInt32(team));

        if (fixtures.Count != 0)
        {
            var fixtureEmbed = new DiscordEmbedBuilder();

            foreach (var fixture in fixtures)
            {
                fixtureEmbed.Title = $"{team.GetName()} Fixtures: {fixture.Season.StartDate.Substring(0, 4)}/" +
                                     $"{fixture.Season.EndDate.Substring(0, 4)} ⚽ 🦁";
                fixtureEmbed.Description = "*Only shows next 25 games and may have some non Premiere League games";
                fixtureEmbed.Color = DiscordColor.SpringGreen;
            }

            foreach (var fixture in fixtures.Take(25))
            {
                var easternTimeGame = TimeZoneInfo.ConvertTimeFromUtc(fixture.UtcDate,
                    TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

                fixtureEmbed.AddField(
                    $"{fixture.HomeTeam.Name} vs. {fixture.AwayTeam.Name}",
                    $"{easternTimeGame.ToString($"MMMM dd, h:mm tt")}\n", inline: true);
            }

            await context.CreateResponseAsync(fixtureEmbed, ephemeral: true);
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