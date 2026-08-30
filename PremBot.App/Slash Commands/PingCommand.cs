using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;

namespace PremBot.App.Slash_Commands;

public class PingCommand : ApplicationCommandModule
{
    [SlashCommand("ping", "Will pong back to the server.")]
    public async Task PingCommandAsync(InteractionContext context)
    {
        var pingEmbed = new DiscordEmbedBuilder()
        {
            Title = $"Pong 🏓 ``{context.Member.Username}``",
            ImageUrl = "https://i.pinimg.com/originals/15/a0/34/15a034ce504d844d64effa4861cf02e9.gif",
            Color = DiscordColor.SpringGreen,
        };

        await context.CreateResponseAsync(pingEmbed, ephemeral: true);
    }
}