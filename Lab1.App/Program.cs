using System;
using System.Globalization;
using System.Text;
using Lab1.App.Task1;
using Lab1.App.Task2;
using Lab1.App.Task3;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

Console.WriteLine("Лабораторная работа №1");
Console.WriteLine("Задание №1 (10 вариантов) -> вариант №5: MyVector");
Console.WriteLine("Задание №2 (8 вариантов)  -> вариант №7: Визитор");
Console.WriteLine("Задание №3 (4 варианта)   -> вариант №3: Дерево с подсчётом узлов");

DemoTask1_MyVector();
DemoTask2_Visitor();
DemoTask3_TreeCount();

static void DemoTask1_MyVector()
{
	Console.WriteLine();
	Console.WriteLine("=== ЛР1 / Задание №1 / Вариант №5: MyVector ===");

	var v1 = new MyVector(10, 15);
	var v2 = new MyVector(-2, 8);

	Console.WriteLine($"v1 = {v1}");
	Console.WriteLine($"v2 = {v2}");

	var sum = v1 + v2;
	var diff = v1 - v2;
	var scaled1 = v1 * 2;
	var scaled2 = 2 * v2;
	var dot = v1 * v2;
	var len1 = +v1;

	Console.WriteLine($"v1 + v2 = {sum}");
	Console.WriteLine($"v1 - v2 = {diff}");
	Console.WriteLine($"v1 * 2 = {scaled1}");
	Console.WriteLine($"2 * v2 = {scaled2}");
	Console.WriteLine($"v1 · v2 = {dot}");
	Console.WriteLine($"|v1| = {len1}");

	Console.WriteLine($"v1 == new MyVector(10, 15) -> {v1 == new MyVector(10, 15)}");
	Console.WriteLine($"v1 после операций всё ещё {v1} (неизменяемость)");

	try
	{
		_ = new MyVector(double.NaN, 0);
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Ожидаемая ошибка при некорректных входных данных: {ex.GetType().Name}: {ex.Message}");
	}
}

static void DemoTask2_Visitor()
{
	Console.WriteLine();
	Console.WriteLine("=== ЛР1 / Задание №2 / Вариант №7: Визитор (экспорт HTML/Markdown) ===");

	var document = new Document();
	document.Add(new Paragraph("Пример документа: абзац, картинка и таблица."));
	document.Add(new ImageElement("https://example.com/image.png"));
	document.Add(
		new TableElement(
			new[]
			{
				new[] { "Name", "Score" },
				new[] { "Alice", "10" },
				new[] { "Bob", "7" },
			}));

	var htmlVisitor = new HtmlVisitor();
	document.Export(htmlVisitor);

	var markdownVisitor = new MarkdownVisitor();
	document.Export(markdownVisitor);

	Console.WriteLine("--- HTML ---");
	Console.WriteLine(htmlVisitor.Result);
	Console.WriteLine("--- Markdown ---");
	Console.WriteLine(markdownVisitor.Result);
}

static void DemoTask3_TreeCount()
{
	Console.WriteLine();
	Console.WriteLine("=== ЛР1 / Задание №3 / Вариант №3: Дерево с подсчётом узлов ===");

	// Конфигурация дерева (создание узлов и связывание) — в Program.cs по условию.
	var root = new TreeNode("Root");
	var a = new TreeNode("A");
	var b = new TreeNode("B");

	root.AddChild(a);
	root.AddChild(b);

	var a1 = new TreeNode("A1");
	var a2 = new TreeNode("A2");
	a.AddChild(a1);
	a.AddChild(a2);

	var a1a = new TreeNode("A1a");
	a1.AddChild(a1a);

	var b1 = new TreeNode("B1");
	b.AddChild(b1);

	Console.WriteLine("Структура дерева:");
	root.TraverseDepthFirst((node, depth) =>
	{
		Console.WriteLine($"{new string(' ', depth * 2)}- {node.Value}");
	});

	Console.WriteLine();
	Console.WriteLine($"Всего узлов в дереве (включая корень): {root.CountSubtreeNodes()}");
	Console.WriteLine($"Количество потомков корня: {root.CountDescendants()}");
	Console.WriteLine($"Узлов в поддереве '{a.Value}': {a.CountSubtreeNodes()} (потомков: {a.CountDescendants()})");
}
