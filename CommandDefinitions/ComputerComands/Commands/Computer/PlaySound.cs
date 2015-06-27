﻿using System.Media;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [StringParameter("Path")]
    public class PlaySound : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;
            var path = scope.ReadParameter("Path").Value;
            var player = new SoundPlayer(path);
            player.Play();
        }
    }
}
