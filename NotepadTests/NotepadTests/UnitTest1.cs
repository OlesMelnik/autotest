using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading;
namespace NotepadTests
{
    public class UnitTest1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            using (var automation = new UIA3Automation())
            {
                var app = FlaUI.Core.Application.Launch(@"C:\WINDOWS\system32\notepad.exe");
                var window = app.GetMainWindow(automation);
                window.Title.Should().Be("Без імені: Блокнот");
                app.Close();
                Retry.WhileException(() => app.ExitCode.Should().Be(0));
            }
        }

        [Test]
        public void Test2()
        {
            var app = FlaUI.Core.Application.Launch("notepad.exe");
            var timeOut = TimeSpan.FromSeconds(200);
            Retry.DefaultTimeout = timeOut;
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                window.Title.Should().Be("Без імені: Блокнот");
                var fileTab = window.FindAllByXPath("/MenuBar/MenuItem[1]")[0];
                fileTab.WaitUntilClickable();
                fileTab.Click();
                var openElement = window.FindAllDescendants().Single(x => x.Name == "Відкрити..." && x.ControlType == ControlType.MenuItem).AsMenuItem();
                openElement.WaitUntilClickable(timeOut);
                openElement.Invoke();
                Retry.WhileTrue(() => window.ModalWindows.Length == 1);
                var openFile = window.ModalWindows[0].AsWindow();
                openFile.Name.Should().Be("Відкриття");
                var edit = openFile.FindFirstChild("1148");
                edit.WaitUntilClickable();
                edit.Click();
                Retry.WhileTrue(() => edit.FrameworkAutomationElement.HasKeyboardFocus);
                Keyboard.Type(@"C:\Users\olesm\Desktop\test.txt");
                var fileOpen = openFile.FindFirstChild("1");
                fileOpen.WaitUntilClickable(timeOut);
                fileOpen.Click();

                Retry.WhileTrue(() => window.Title.Equals("test.txt: Блокнот"));

                var editText = window.FindFirstChild("15").AsTextBox();
                editText.Text.Should().Be("some text here");
                editText.Enter("new text");

                fileTab.Click();
                var saveElement = window.FindAllDescendants().Single(x => x.Name == "Зберегти як…" && x.ControlType == ControlType.MenuItem).AsMenuItem();
                saveElement.WaitUntilClickable(timeOut);
                saveElement.Invoke();
                Retry.WhileTrue(() => window.ModalWindows.Length == 1);
                var saveFile = window.ModalWindows[0].AsWindow();
                var dynamicPart = DateTime.Now.ToShortDateString().Replace("/", string.Empty);
                var path = $"C:\\Users\\olesm\\Desktop\\test2{dynamicPart}.txt";
                Keyboard.Type(path);
                var fileTypeDropDown = saveFile.FindAllDescendants(x => x.ByClassName("AppControlHost")).First(x => x.Name == "Тип файлу:").AsComboBox();
                fileTypeDropDown.Expand();
                fileTypeDropDown.Select("Текстовий документ (*.txt)");
                saveFile.FindFirstChild("1").AsButton().Invoke();
                Retry.WhileTrue(() => window.Title.Equals($"test2{dynamicPart}: Блокнот"));
                //Retry.WhileTrue(() => window.ModalWindows.Length == 1 && window.ModalWindows[0].Title == "Пітвердження збереження" && window.ClassName == "#32770");
                //var windowModalWarning = window.ModalWindows[0];
                //var okButton = windowModalWarning.FindFirstChild("CommandButton_6").AsButton();
                //okButton.Click();
                
                File.ReadAllText(path).Should().Be("new text");
                app.Close();
                //Thread.Sleep(500);
                //File.Delete(path);
            }
        }

    }
}
