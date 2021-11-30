using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Amadeus.Server.Data;
using Amadeus.Server.Exceptions;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amadeus.Server.Controllers
{
	public class WidgetRepository : IRepository<Widget>
	{
		/// <summary>
		/// The Widget ORM.
		/// </summary>
		private readonly ServerDB _context;

		public WidgetRepository(ServerDB context)
		{
			_context = context;
		}

		public async Task<IList<Widget>> GetAll()
		{
			return await _context.Widgets
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IList<Widget>> GetByUser(User user)
		{
			return await _context.Widgets.AsNoTracking().Where(x => x.UserId == user.Id).ToListAsync();
		}

		public async Task<Widget> GetById(int id)
		{
			Widget u = await _context.Widgets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
			if (u == null)
				throw new ElementNotFound($"The widget with the id: {id} wasn't found");
			return u;
		}

		public Task<Widget> Modify(int id, Widget element)
		{
			throw new NotImplementedException();
		}

		public Task<Widget> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IList<Widget>> GetWhere(Expression<Func<Widget, bool>> pred)
		{
			return await _context.Widgets.AsNoTracking().Where(pred).ToListAsync();
		}

		public async Task<Widget> Create(Widget element)
		{
			// widget.UserId
			_context.Widgets.Add(element);
			await _context.SaveChangesAsync();
			return element;
		}


	}
}
