using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Tests;
using PsyConnect.Core.Models;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class OpcionRespuestaRepository : Repository<OpcionRespuesta>, IOpcionRespuestaRepository
    {
        public OpcionRespuestaRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<List<OpcionRespuesta>> GetByPreguntaIdAsync(int preguntaId)
        {
            return await _dbSet
                .Where(o => o.PreguntaID == preguntaId)
                .OrderBy(o => o.OpcionID)
                .ToListAsync();
        }
    }
}