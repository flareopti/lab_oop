using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1.App.Task2;

public interface IDocumentVisitor
{
    void VisitParagraph(Paragraph paragraph);
    void VisitImage(ImageElement image);
    void VisitTable(TableElement table);
}

public sealed class HtmlVisitor : IDocumentVisitor
{
    private readonly StringBuilder _builder = new();

    public string Result => _builder.ToString();

    public void VisitParagraph(Paragraph paragraph)
    {
        _builder.Append("<p>");
        _builder.Append(HtmlEscape(paragraph.Text));
        _builder.AppendLine("</p>");
    }

    public void VisitImage(ImageElement image)
    {
        _builder.Append("<img src=\"");
        _builder.Append(HtmlEscapeAttribute(image.Path));
        _builder.AppendLine("\" />");
    }

    public void VisitTable(TableElement table)
    {
        _builder.AppendLine("<table>");

        foreach (var row in table.Rows)
        {
            _builder.AppendLine("  <tr>");

            foreach (var cell in row)
            {
                _builder.Append("    <td>");
                _builder.Append(HtmlEscape(cell));
                _builder.AppendLine("</td>");
            }

            _builder.AppendLine("  </tr>");
        }

        _builder.AppendLine("</table>");
    }

    private static string HtmlEscape(string text) =>
        text
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;");

    private static string HtmlEscapeAttribute(string text) =>
        HtmlEscape(text).Replace("\"", "&quot;");
}

public sealed class MarkdownVisitor : IDocumentVisitor
{
    private readonly StringBuilder _builder = new();

    public string Result => _builder.ToString();

    public void VisitParagraph(Paragraph paragraph)
    {
        _builder.AppendLine(paragraph.Text);
        _builder.AppendLine();
    }

    public void VisitImage(ImageElement image)
    {
        _builder.Append("![image](");
        _builder.Append(image.Path.Replace(")", "\\)"));
        _builder.AppendLine(")");
        _builder.AppendLine();
    }

    public void VisitTable(TableElement table)
    {
        if (table.Rows.Count == 0)
        {
            _builder.AppendLine();
            return;
        }

        var columnCount = table.Rows.Max(r => r.Count);

        static string Cell(string value) => value.Replace("|", "\\|");

        // Use the first row as a header.
        var header = table.Rows[0];
        _builder.AppendLine(RowLine(header, columnCount));
        _builder.AppendLine(SeparatorLine(columnCount));

        foreach (var row in table.Rows.Skip(1))
        {
            _builder.AppendLine(RowLine(row, columnCount));
        }

        _builder.AppendLine();

        static string RowLine(IReadOnlyList<string> row, int columnCount)
        {
            var cells = new List<string>(columnCount);
            for (var i = 0; i < columnCount; i++)
            {
                var value = i < row.Count ? row[i] : string.Empty;
                cells.Add(Cell(value));
            }

            return "| " + string.Join(" | ", cells) + " |";
        }

        static string SeparatorLine(int columnCount) =>
            "| " + string.Join(" | ", Enumerable.Repeat("---", columnCount)) + " |";
    }
}
