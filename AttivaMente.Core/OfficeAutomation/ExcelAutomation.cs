using AttivaMente.Core.Models;
using OfficeOpenXml;

namespace AttivaMente.Core.OfficeAutomation
{
    public class ExcelAutomation
    {
        public static void CreateUsersList(List<Utente> utenti, string xlsxPath)
        {
            if (File.Exists(xlsxPath)) File.Delete(xlsxPath);

            ExcelPackage.License.SetNonCommercialOrganization("Vallauri");

            using var package = new ExcelPackage(new FileInfo(xlsxPath));
            CreatePackageContent(package, utenti);

            // salvo il file
            package.Save();
        }

        public static byte[] GetUsersXlsxBytes(List<Utente> utenti)
        {
            ExcelPackage.License.SetNonCommercialOrganization("Vallauri");

            using var package = new ExcelPackage();
            CreatePackageContent(package, utenti);

            // restituisco array di byte
            return package.GetAsByteArray();
        }

        private static void CreatePackageContent(ExcelPackage package, List<Utente> utenti)
        {
            // costruisco i contenuti del file
            var ws = package.Workbook.Worksheets.Add("Utenti");

            // intestazioni
            ws.Cells[1, 1].Value = "Id";
            ws.Cells[1, 2].Value = "Nome";
            ws.Cells[1, 3].Value = "Cognome";
            ws.Cells[1, 4].Value = "Email";
            ws.Cells[1, 5].Value = "Ruolo";
            // dati
            int row = 2;
            foreach (var utente in utenti)
            {
                ws.Cells[row, 1].Value = utente.Id;
                ws.Cells[row, 2].Value = utente.Nome;
                ws.Cells[row, 3].Value = utente.Cognome;
                ws.Cells[row, 4].Value = utente.Email;
                // ws.Cells[row, 5].Value = utente.Ruolo!.Nome;
                row++;
            }
            // larghezza colonne automatica
            ws.Cells.AutoFitColumns();
        }
    }
}
