using CampusConnect.Models;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Identity;
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

            var totalAlunos = attendanceList.Count();
            var presentes = attendanceList.Count(a => a.Presente);
            var ausentes = totalAlunos - presentes;

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

            return File(stream.ToArray(), "application/pdf", $"Relatorio_Presenca{DateTime.Now.ToString("dd/MM/yyyy")}.pdf");
        }
}

    [HttpPost]
    public IActionResult GenerateReportFull(List<UserAttendance> attendanceList, string instituicao, string cidade, string rota, SelectedFields selectedFields)
    {
        using (var stream = new MemoryStream())
        {
            var pdfWriter = new PdfWriter(stream);
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            var titulo = new Paragraph("Relatório Completo")
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
            document.Add(new Paragraph($"Data: {DateTime.Now:dd/MM/yyyy}").SetFontSize(12).SetBold().SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));

            var alunosFiltrados = attendanceList.Where(u =>
                (string.IsNullOrEmpty(instituicao) || u.Instituicao.Equals(instituicao, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(cidade) || u.Cidade.Equals(cidade, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(rota) || u.Rota.Equals(rota, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            int selectedCount = 0;
            if (selectedFields.Nome) selectedCount++;
            if (selectedFields.Instituicao) selectedCount++;
            if (selectedFields.Cidade) selectedCount++;
            if (selectedFields.Curso) selectedCount++;
            if (selectedFields.Rota) selectedCount++;
            if (selectedFields.Telefone) selectedCount++;
            if (selectedFields.Email) selectedCount++;
            if (selectedFields.Matricula) selectedCount++;
            if (selectedFields.Periodo) selectedCount++;

            float fontSize = 10f; 
            if (selectedCount > 5)
            {
                fontSize = 9f; 
            }
            if (selectedCount > 7)
            {
                fontSize = 8f; 
            }

            List<float> columnWidths = new List<float>();

            if (selectedFields.Nome) columnWidths.Add(1f);
            if (selectedFields.Instituicao) columnWidths.Add(1f);
            if (selectedFields.Curso) columnWidths.Add(1f);
            if (selectedFields.Telefone) columnWidths.Add(1f);
            if (selectedFields.Email) columnWidths.Add(1f);
            if (selectedFields.Matricula) columnWidths.Add(1f);
            if (selectedFields.Periodo) columnWidths.Add(1f);
            if (selectedFields.Cidade) columnWidths.Add(1f);
            if (selectedFields.Rota) columnWidths.Add(1f);

            Table table = new Table(columnWidths.ToArray());
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetTextAlignment(TextAlignment.CENTER);
            table.SetBorder(Border.NO_BORDER);

            if (selectedFields.Nome) table.AddHeaderCell(new Cell().Add(new Paragraph("Nome").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Instituicao) table.AddHeaderCell(new Cell().Add(new Paragraph("Instituição").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Cidade) table.AddHeaderCell(new Cell().Add(new Paragraph("Cidade").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Curso) table.AddHeaderCell(new Cell().Add(new Paragraph("Curso").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Rota) table.AddHeaderCell(new Cell().Add(new Paragraph("Rota").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Telefone) table.AddHeaderCell(new Cell().Add(new Paragraph("Telefone").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Email) table.AddHeaderCell(new Cell().Add(new Paragraph("Email").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Matricula) table.AddHeaderCell(new Cell().Add(new Paragraph("Matrícula").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));
            if (selectedFields.Periodo) table.AddHeaderCell(new Cell().Add(new Paragraph("Período").SetFontSize(fontSize).SetBold().SetTextAlignment(TextAlignment.CENTER)));

            foreach (var user in alunosFiltrados)
            {
                if (selectedFields.Nome)
                    table.AddCell(new Cell().Add(new Paragraph(user.Nome + " " + user.Sobrenome).SetFontSize(fontSize)));
                if (selectedFields.Instituicao)
                    table.AddCell(new Cell().Add(new Paragraph(user.Instituicao).SetFontSize(fontSize)));
                if (selectedFields.Cidade)
                    table.AddCell(new Cell().Add(new Paragraph(user.Cidade).SetFontSize(fontSize)));
                if (selectedFields.Curso)
                    table.AddCell(new Cell().Add(new Paragraph(user.Curso).SetFontSize(fontSize)));
                if (selectedFields.Rota)
                    table.AddCell(new Cell().Add(new Paragraph(user.Rota).SetFontSize(fontSize)));
                if (selectedFields.Telefone)
                    table.AddCell(new Cell().Add(new Paragraph(user.Telefone).SetFontSize(fontSize)));
                if (selectedFields.Email)
                    table.AddCell(new Cell().Add(new Paragraph(user.Email).SetFontSize(fontSize)));
                if (selectedFields.Matricula)
                    table.AddCell(new Cell().Add(new Paragraph(user.Matricula).SetFontSize(fontSize)));
                if (selectedFields.Periodo)
                    table.AddCell(new Cell().Add(new Paragraph(user.Periodo).SetFontSize(fontSize)));
            }

            document.Add(table);

            var rodape = new Paragraph($"Campus Connect, {DateTime.Now:yyyy}")
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(20);

            document.Add(rodape);

            document.Close();

            return File(stream.ToArray(), "application/pdf", $"Relatorio_Geral{DateTime.Now:ddMMyyyy}.pdf");
        }
    }
}

public class UserAttendance
{
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Instituicao { get; set; }
    public string? Curso { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get;set; }
    public string? Matricula { get; set; }
    public string? Periodo { get; set; }
    public string? Cidade { get; set; }
    public string? Rota { get; set; }
    public bool Presente { get; set; }
}

public class SelectedFields
{
    public bool Nome { get; set; }
    public bool Sobrenome { get; set; }
    public bool Instituicao { get; set; }
    public bool Curso { get; set; }
    public bool Telefone { get; set; }
    public bool Email { get; set; }
    public bool Matricula { get; set; }
    public bool Periodo { get; set; }
    public bool Cidade { get; set; }
    public bool Rota { get; set; }
}

