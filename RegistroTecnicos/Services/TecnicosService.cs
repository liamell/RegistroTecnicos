using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using static Azure.Core.HttpHeader;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    private async Task<bool> Existe(int tecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId == tecnicoId);

    }

    public async Task<bool> ExisteTecnico(int tecnicoId, string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId != tecnicoId
            && t.Nombres.ToLower().Equals(nombre.ToLower()));
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(tecnico);
        var modificado = await contexto.SaveChangesAsync() > 0;
        contexto.Entry(tecnico).State = EntityState.Modified;
        return modificado;

    }

    public async Task<bool> Eliminar(int tecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .Where(t => t.TecnicoId == tecnicoId)
            .ExecuteDeleteAsync() > 0;

    }

    public async Task<Tecnicos> Buscar(int tecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
        .FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .Where(criterio)
            .ToListAsync();

    }
}


