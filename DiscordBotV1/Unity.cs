﻿using Unity;
using Unity.Lifetime;
using Unity.Injection;
using Unity.Resolution;
using Discord.WebSocket;
using DiscordBotV1.Discord;
using DiscordBotV1.Storage;
using DiscordBotV1.Storage.Implementations;

namespace DiscordBotV1
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
			_container.RegisterSingleton<IDataStorage, JsonStorage>();
			_container.RegisterSingleton<ILogger, Logger>();
			_container.RegisterType<DiscordSocketConfig>(new InjectionFactory(i => SocketConfig.GetDefault())); //TODO: Exchange to IUnityContainer
			_container.RegisterSingleton<DiscordSocketClient>(new InjectionConstructor(typeof(DiscordSocketConfig)));
			_container.RegisterSingleton<Discord.Connection>();
		}

		public static T Resolve<T>()
		{
			return (T)Container.Resolve(typeof(T));
		}
	}
}
