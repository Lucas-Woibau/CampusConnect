using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            var titulo = new Paragraph("Relatório de Presença")
                .SetFontSize(24)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(10)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));

            document.Add(titulo);

            string filtro = $"Filtro utilizado: {(!string.IsNullOrEmpty(instituicao) ? "Instituição: " + instituicao : "Todas as instituições")}" +
                            $" | {(!string.IsNullOrEmpty(cidade) ? "Cidade: " + cidade : "Todas as cidades")}" +
                            $" | {(!string.IsNullOrEmpty(rota) ? "Rota: " + rota : "Todas as rotas")}";

            document.Add(new Paragraph(filtro).SetFontSize(12).SetItalic().SetBold().SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(5));
            document.Add(new Paragraph($"Data: {DateTime.Now.ToString("dd/MM/yyyy")}").SetFontSize(12).SetBold().SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));

            // Contagem de alunos presentes e ausentes
            var totalAlunos = attendanceList.Count();
            var presentes = attendanceList.Count(a => a.Presente);
            var ausentes = totalAlunos - presentes;

            // Adiciona a informação de total de alunos, presentes e ausentes
            document.Add(new Paragraph($"Total de Alunos: {totalAlunos} | Presentes: {presentes} | Ausentes: {ausentes}")
                .SetFontSize(12).SetBold().SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10));

            Table table = new Table(new float[] { 3, 2, 2, 1 });
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetTextAlignment(TextAlignment.CENTER);
            table.SetBorder(Border.NO_BORDER);

            var headerCellStyle = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetBold()
                .SetFontSize(12)
                .SetPadding(5)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));

            table.AddHeaderCell(new Cell().Add(new Paragraph("Nome")).AddStyle(headerCellStyle));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Instituição")).AddStyle(headerCellStyle));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Cidade")).AddStyle(headerCellStyle));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Status")).AddStyle(headerCellStyle));

            var cellStyle = new Style()
                .SetFontSize(10)
                .SetPadding(5)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f));

            var alunosFiltrados = attendanceList.Where(u =>
                (string.IsNullOrEmpty(instituicao) || u.Instituicao.Equals(instituicao, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(cidade) || u.Cidade.Equals(cidade, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(rota) || u.Rota.Equals(rota, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            foreach (var user in alunosFiltrados)
            {
                table.AddCell(new Cell().Add(new Paragraph(user.Nome)).AddStyle(cellStyle));
                table.AddCell(new Cell().Add(new Paragraph(user.Instituicao)).AddStyle(cellStyle));
                table.AddCell(new Cell().Add(new Paragraph(user.Cidade)).AddStyle(cellStyle));
                table.AddCell(new Cell().Add(new Paragraph(user.Presente ? "Presente" : "Faltou")).AddStyle(cellStyle));
            }

            document.Add(table);

            int totalPages = pdfDocument.GetNumberOfPages();
            for (int i = 1; i <= totalPages; i++)
            {
                document.ShowTextAligned(new Paragraph($"Página {i} de {totalPages}"),
                    30, 20, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
            }

            var rodape = new Paragraph($"Campus Connect, {DateTime.Now.ToString("yyyy")}")
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(20);

            document.Add(rodape);

            document.Close();

            return File(stream.ToArray(), "application/pdf", "Relatorio_Presenca.pdf");
        }
    }
}

// Classe de Dados
public class UserAttendance
{
    public string? Nome { get; set; }
    public string? Instituicao { get; set; }
    public string? Cidade { get; set; }
    public string? Rota { get; set; }
    public bool Presente { get; set; }
}
