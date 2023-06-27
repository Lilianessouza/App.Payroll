using Application.Validator;

namespace Application.Interface;

public interface IContraChequeService
{
    Task<ResultService> GetAllFuncionario();
    Task<ResultService> GetContraChequeByFuncionarioId(int id);
}