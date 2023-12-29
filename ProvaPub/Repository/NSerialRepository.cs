using ProvaPub.Infra.Db;
using ProvaPub.Models.DTOs;
using ProvaPub.Models.Entities;
using ProvaPub.Repository.Interfaces;
using System.Transactions;

namespace ProvaPub.Repository
{
    public class NSerialRepository : INSerialRepository
    {
        private readonly TestDbContext _ctx;
        public NSerialRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public IResult CreateSerial(NSerialDTO nSerial)
        {
            NSerial nSerialCreated = new NSerial(nSerial.Modelo, nSerial.Minimo, nSerial.Maximo, nSerial.Incremento, nSerial.Descricao);

            _ctx.NSerials.Add(nSerialCreated);

            _ctx.SaveChanges();

            return Results.Ok("Criado!");
        }


        public async Task<IResult> GetSerial(int modelo)
        {
            using(var transactionScope =  new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    NSerial findSerial = await _ctx.NSerials.FindAsync(modelo);

                    if (findSerial == null)
                    {
                        return Results.NotFound();
                    }

                    findSerial.getSerial();

                    _ctx.SaveChanges();

                    await Task.Delay(TimeSpan.FromSeconds(10));

                    transactionScope.Complete();

                    return Results.Ok(findSerial);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro durante a atualização: {ex.Message}");
                }
            }
    }
    }
}
