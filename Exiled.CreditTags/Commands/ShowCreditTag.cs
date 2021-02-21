// -----------------------------------------------------------------------
// <copyright file="ShowCreditTag.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.CreditTags.Commands
{
    using System;

    using CommandSystem;

    using Exiled.API.Features;

    /// <summary>
    /// A client command to show your credit tag.
    /// </summary>
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ShowCreditTag : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "exiledtag";

        /// <inheritdoc/>
        public string[] Aliases { get; } = new[] { "crtag" };

        /// <inheritdoc/>
        public string Description { get; } = "Shows your EXILED Credits tag, if available.";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var cmdSender = (CommandSender)sender;
            Player player = Player.Get(cmdSender.SenderId);

            void HappyHandler()
            {
                cmdSender.RaReply("Enjoy your credit tag!", true, true, string.Empty);
            }

            void ErrorHandler()
            {
                cmdSender.RaReply("An error has occurred.", true, true, string.Empty);
            }

            bool cached = CreditTags.Singleton.ShowCreditTag(player, ErrorHandler, HappyHandler, true);
            response = cached
                ? "Your credit tag has been shown."
                : "Hold on...";
            return true;
        }
    }
}
