using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace PremBot.App.Slash_Commands;

public class HelpCommand : ApplicationCommandModule
{
    [SlashCommand("help", "Gives information about the bot & available commands.")]
    public async Task HelpCommandAsync(InteractionContext context)
    {
        var helpEmbed = new DiscordEmbedBuilder()
        {
            Title = "📝 Getting Started",
            Color = DiscordColor.SpringGreen,
            Description =
                "Get information about the Premiere League! Type one of the commands below to get started. " +
                $"PremBot powered by [DSharpPlus 4.4.2]" +
                $"(https://dsharpplus.github.io/DSharpPlus/index.html) and [Docker](https://www.docker.com/).",
        };

        helpEmbed.AddField
        (
            "**⚽  Premiere League Commands**",
            $"🏆 </standings:1174749725413617748>\n" +
            $"👕 </teamfixture:1178465057621946592>\n" +
            $"📅 </matchdayfixture:1178462218782773298>",
            inline: true
        );

        helpEmbed.AddField
        (
            "🛠️  Other Commands",
            $"🆘  </help:1175112211291705457>\n" +
            "🏓  </ping:1174725698007605278>\n" +
            "🖼️ </caption:1174732729800208416>",
            inline: true
        );

        var serverCount = context.Client.Guilds.Count;
        var shardCount = context.Client.ShardCount;
        var ping = context.Client.Ping;
        var botVersion = context.Client.VersionString;

        helpEmbed.WithFooter
        ($"Bot Info:  " +
         $"Version: 1.3.0  •  " +
         $"Total Servers: {serverCount}  •  " +
         $"Shard: {shardCount}  •  " +
         $"Ping: {ping}"
        );

        var addButton = new DiscordLinkButtonComponent
        (
            "https://discord.com/api/oauth2/authorize?client_id=1174716762793721936&" +
            "permissions=8&scope=bot%20applications.commands",
            "Add PremBot To A Server"
        );

        var apiButton = new DiscordLinkButtonComponent("https://www.football-data.org/", "Utilize Soccer API");

        var messageBuilder = new DiscordInteractionResponseBuilder {IsEphemeral = true,};

        messageBuilder.AddEmbed(helpEmbed);
        messageBuilder.AddComponents(addButton, apiButton);

        await context.CreateResponseAsync(messageBuilder.AsEphemeral());
    }
}