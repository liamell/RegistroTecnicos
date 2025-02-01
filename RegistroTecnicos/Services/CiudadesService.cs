using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Migrations;
using RegistroTecnicos.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CiudadesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Ciudades ciudades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(ciudades.CiudadId))
            return await Insertar(ciudades);

        else
            return await Modificar(ciudades);
        
    }

    private async Task<bool> Existe(int ciudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .AnyAsync(c => c.CiudadId == ciudadId);

    }

    private async Task<bool> Insertar(Ciudades ciudades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Ciudades.Add(ciudades);
        return await contexto.SaveChangesAsync() > 0;
            
    }

    private async Task<bool> Modificar(Ciudades ciudades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(ciudades);
        var modificado = await contexto.SaveChangesAsync() > 0;
        contexto.Entry(ciudades).State = EntityState.Modified;
        return modificado;

    }

    public async Task<bool> Eliminar(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Where(c => c.ClienteId == clienteId)
            .ExecuteDeleteAsync() > 0;

    }

    public async Task<Ciudades> Buscar(int ciudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
        .FirstOrDefaultAsync(c => c.CiudadId == ciudadId);
    }

    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .Where(criterio)
            .ToListAsync();

    }



}
