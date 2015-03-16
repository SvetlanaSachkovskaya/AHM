using System.Threading.Tasks;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IBillPdfGenerator
    {
        Task<string> GenerateAsync(int billId, string directoryPath);
    }
}