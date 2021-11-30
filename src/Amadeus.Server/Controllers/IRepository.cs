using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amadeus.Server.Models;

namespace Amadeus.Server.Controllers
{
	/// <summary>
	/// Generic repository interface.
	/// </summary>
	/// <typeparam name="T">The type of the repository.</typeparam>
	public interface IRepository<T>
	{
		/// <summary>
		/// Get all the users in db.
		/// </summary>
		/// <returns>All the users in the db.</returns>
		public Task<IList<T>> GetAll();

		/// <summary>
		/// Get a user by it's id.
		/// </summary>
		/// <param name="id">The id of the element to get.</param>
		/// <returns>The element corresponding at the id.</returns>
		public Task<T> GetById(int id);

		/// <summary>
		/// Create saves the user in the db.
		/// </summary>
		/// <param name="element">The element to create in the db.</param>
		/// <returns>The user saved in db.</returns>
		public Task<T> Create([NotNull] T element);

		/// <summary>
		/// Modify an element by id.
		/// </summary>
		/// <param name="id">id of the element to delete.</param>
		/// <param name="element">The element with new values.</param>
		/// <returns>The deleted element.</returns>
		public Task<T> Modify(int id, T element);

		/// <summary>
		/// Delete a element by id.
		/// </summary>
		/// <param name="id">id of the element to delete.</param>
		/// <returns>The deleted element.</returns>
		public Task<T> Delete(int id);
	}
}
