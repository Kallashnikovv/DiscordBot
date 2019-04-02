﻿using Unity;
using Unity.Resolution;
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
			_container.RegisterType<IDataStorage, InMemoryStorage>();
		}

		public static T Resolve<T>()
		{
			return (T)Container.Resolve(typeof(T));
		}
	}
}
