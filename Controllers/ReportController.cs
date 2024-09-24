using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

public class ReportController : Controller
{
    [HttpPost]
    public IActionResult GenerateReport(List<UserAttendance> attendanceList, string rota, string cidade, string instituicao)
    {
        using (var stream = new MemoryStream())
        {
            var pdfWriter = new PdfWriter(stream);
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            string filtro = $"Filtro utilizado: {(!string.IsNullOrEmpty(instituicao) ? "Instituição: " + instituicao : "Todas as instituições")}" +
                            $" | {(!string.IsNullOrEmpty(cidade) ? "Cidade: " + cidade : "Todas as cidades")}" +
                            $" | {(!string.IsNullOrEmpty(rota) ? "Rota: " + rota : "Todas as rotas")}";

            document.Add(new Paragraph("Relatório de Presença").SetBold().SetFontSize(20));
            document.Add(new Paragraph(filtro).SetFontSize(12).SetItalic());
            document.Add(new Paragraph($"Data: {DateTime.Now.ToString("dd/MM/yyyy")}").SetFontSize(12));

            Table table = new Table(new float[] { 1, 1, 1, 1 });
            table.SetWidth(UnitValue.CreatePercentValue(100));

            table.AddHeaderCell("Nome");
            table.AddHeaderCell("Instituição");
            table.AddHeaderCell("Cidade");
            table.AddHeaderCell("Presença");

            var alunosFiltrados = attendanceList.Where(u =>
                (string.IsNullOrEmpty(instituicao) || u.Instituicao.Equals(instituicao, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(cidade) || u.Cidade.Equals(cidade, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(rota) || u.Rota.Equals(rota, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            foreach (var user in alunosFiltrados)
            {
                table.AddCell(user.Nome);
                table.AddCell(user.Instituicao);
                table.AddCell(user.Cidade);
                table.AddCell(user.Presente == "true" ? "Presente" : "Faltou");
            }

            document.Add(table);
            document.Close();

            return File(stream.ToArray(), "application/pdf", "Relatorio_Presenca.pdf");
        }
    }

}

public class UserAttendance
{
    public string? Nome { get; set; }
    public string? Instituicao { get; set; }
    public string? Cidade { get; set; }
    public string? Rota { get; set; }
    public string? Presente { get; set; }
}
