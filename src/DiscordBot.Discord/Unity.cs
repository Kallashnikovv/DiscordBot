﻿using Unity;
using Unity.Injection;
using Unity.Resolution;
using Discord.WebSocket;
using DiscordBot.Discord.Configurations;
using DiscordBot.Discord;
using DiscordBot.Core.Services.Logger;
using System.Reflection;
using System.Linq;
using Discord.Commands;

namespace DiscordBot.Discord
{
	public static class Unity
	{
		private static UnityContainer _container;

		public static UnityContainer Container
		{
			get
			{
				if (_container == null)
					RegisterTypes();
				return _container;
			}
		}

		public static void RegisterTypes()
		{
            _container = new UnityContainer();
			_container.RegisterSingleton<ILogger, DiscordBotLogger>();
			_container.RegisterType<DiscordSocketConfig>(new InjectionFactory(i => SocketConfig.GetDefault())); //TODO: Exchange to IUnityContainer
			_container.RegisterType<CommandServiceConfig>(new InjectionFactory(i => CommandServicesConfig.GetDefault())); //TODO: Exchange to IUnityContainer
			_container.RegisterSingleton<DiscordSocketClient>(new InjectionConstructor(typeof(DiscordSocketConfig)));
			_container.RegisterSingleton<Connection>();
		}

		public static T Resolve<T>(this IUnityContainer container, object ParameterOverrides)
		{
			var properties = ParameterOverrides
				.GetType()
				.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var overridesArray = properties
				.Select(p => new ParameterOverride(p.Name, p.GetValue(ParameterOverrides, null)))
				.Cast<ResolverOverride>()
				.ToArray();
			return Container.Resolve<T>(null, overridesArray);
		}
	}
}
