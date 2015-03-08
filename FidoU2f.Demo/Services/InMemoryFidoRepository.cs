﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using FidoU2f.Models;

namespace FidoU2f.Demo.Services
{
	/// <summary>
	/// Sample FIDO repository which is not bound to a user name.
	/// </summary>
	/// <remarks>
	/// You would need to adapt it to bind registration/authentication information to users in your database.
	/// User names are ignored in this implementation.
	/// </remarks>
	public class InMemoryFidoRepository : IFidoRepository
	{
		private static readonly ConcurrentDictionary<string, FidoStartedRegistration> StartedRegistrations = new ConcurrentDictionary<string, FidoStartedRegistration>();
		private static readonly List<FidoDeviceRegistration> DeviceRegistrations = new List<FidoDeviceRegistration>();

		public void StoreStartedRegistration(string userName, FidoStartedRegistration startedRegistration)
		{
			StartedRegistrations[startedRegistration.Challenge] = startedRegistration;
		}

		public FidoStartedRegistration GetStartedRegistration(string userName, string challenge)
		{
			FidoStartedRegistration result;
			StartedRegistrations.TryGetValue(challenge, out result);
			return result;
		}

		public IEnumerable<FidoStartedRegistration> GetAllStartedRegistrationsOfUser(string userName)
		{
			return StartedRegistrations.Values;
		}

		public void RemoveStartedRegistration(string userName, string challenge)
		{
			FidoStartedRegistration startedRegistration;
			StartedRegistrations.TryRemove(challenge, out startedRegistration);
		}

		public void StoreDeviceRegistration(string userName, FidoDeviceRegistration deviceRegistration)
		{
			DeviceRegistrations.Add(deviceRegistration);
		}

		public IEnumerable<FidoDeviceRegistration> GetDeviceRegistrationsOfUser(string userName)
		{
			return DeviceRegistrations;
		}
	}
}
