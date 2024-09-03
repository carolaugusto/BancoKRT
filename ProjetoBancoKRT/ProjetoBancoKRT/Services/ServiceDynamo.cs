using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using ProjetoBancoKRT.Models;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;

namespace ProjetoBancoKRT.Services
{
    public class ServiceDynamo
    {

        public class ContaBancariaService
        {
            private readonly DynamoDBContext _context;

            public ContaBancariaService(IAmazonDynamoDB dynamoDBClient)
            {
                _context = new DynamoDBContext(dynamoDBClient);
            }

            public async Task<ContaBancaria> BuscarContaPorCpfAsync(string cpf)
            {
                var conditions = new List<ScanCondition>
        {
            new ScanCondition("documento", ScanOperator.Equal, cpf)
        };

                var result = await _context.ScanAsync<ContaBancaria>(conditions).GetRemainingAsync();
                return result.FirstOrDefault();
            }

            //public async Task<List<ContaBancaria>> ListarContasAsync()
            //{
            //    var conditions = new List<ScanCondition>();
            //    return await _context.ScanAsync<ContaBancaria>(conditions).GetRemainingAsync();
            //}
        }
    }

}

