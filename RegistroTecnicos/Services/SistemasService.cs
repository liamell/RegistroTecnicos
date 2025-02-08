using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class SistemasService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(sistema.SistemaId))
            return await Insertar(sistema);

        else
            return await Modificar(sistema);

    }

    private async Task<bool> Existe(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .AnyAsync(s => s.SistemaId == sistemaId);

    }

    private async Task<bool> Insertar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(sistema);
        return await contexto.SaveChangesAsync() > 0;

    }

    private async Task<bool> Modificar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(sistema);
        var modificado = await contexto.SaveChangesAsync() > 0;
        contexto.Entry(sistema).State = EntityState.Modified;
        return modificado;

    }

    public async Task<bool> Eliminar(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .Where(s => s.SistemaId == sistemaId)
            .ExecuteDeleteAsync() > 0;

    }

    public async Task<Sistemas> Buscar(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
        .FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .Where(criterio)
            .ToListAsync();

    }
}
