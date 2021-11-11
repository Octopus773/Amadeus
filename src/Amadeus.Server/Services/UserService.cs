using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Amadeus.Server.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace Amadeus.Server.Services
{
	public static class UserService
	{
		private static List<User> _users { get; }
		private static UInt64 _currentUid;

		public static List<User> getAll() => _users;

		#nullable enable
		public static User? get(UInt64 uid) => _users.FirstOrDefault(el => el.uid == uid);
		#nullable disable

		public static void add(User user) => _users.Add(user);
	}
}
