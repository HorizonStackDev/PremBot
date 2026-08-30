using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace PremBot.App.Slash_Commands;

public class CaptionCommand : ApplicationCommandModule
{
    [SlashCommand("caption", "Give any image a caption.")]
    public async Task CaptionCommandAsync(InteractionContext context,
        [Option("caption", "The caption you want the image to have")]
        string caption,
        [Option("image", "The image you want to upload")]
        DiscordAttachment image)
    {
        await context.DeferAsync();

        var captionEmbed = new DiscordEmbedBuilder()
        {
            Title = caption,
            ImageUrl = image.Url,
            Color = DiscordColor.SpringGreen,
            Timestamp = DateTimeOffset.UtcNow
        };

        await context.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(captionEmbed));
    }
}