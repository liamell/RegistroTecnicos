using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TicketsService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(ticket.TicketId))
            return await Insertar(ticket);
        else
            return await Modificar(ticket);
    }

    private async Task<bool> Existe(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .AnyAsync(t => t.TicketId == ticketId);

    }

    private async Task<bool> Insertar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(ticket);
        return await contexto.SaveChangesAsync() > 0;

    }

    private async Task<bool> Modificar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(ticket);
        var modificado = await contexto.SaveChangesAsync() > 0;
        contexto.Entry(ticket).State = EntityState.Modified;
        return modificado;

    }

    public async Task<bool> Eliminar(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .Where(t => t.TicketId == ticketId)
            .ExecuteDeleteAsync() > 0;

    }

    public async Task<Tickets> Buscar(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
        .FirstOrDefaultAsync(t => t.TicketId == ticketId);
    }

    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .Where(criterio)
            .ToListAsync();

    }

}
