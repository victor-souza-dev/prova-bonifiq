using ProvaPub.Models.DTOs;

namespace ProvaPub.Repository.Interfaces
{
    public interface INSerialRepository
    {
        Task<IResult> GetSerial(int modelo);
        IResult CreateSerial(NSerialDTO nSerial);
    }
}
