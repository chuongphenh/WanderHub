﻿using System.Reflection;

namespace WanderHub.Infrastructure.MessageBus;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
