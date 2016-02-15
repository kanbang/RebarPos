using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void PosHelp()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n");
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(GetColumnFormattedText(GetCommandsAndDescriptions(), 80));
            Autodesk.AutoCAD.ApplicationServices.Application.DisplayTextScreen = true;
        }

        private void PosCategories()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n");
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(GetColumnFormattedText(GetCategoriesAndCommands(), 80));
        }

        private Dictionary<string, string> GetCommandsAndDescriptions()
        {
            ResourceManager resources = new ResourceManager(typeof(RebarPosCommands.MyCommands));
            return Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(CommandMethodAttribute), false).Length > 0)
                .Where(m => !string.IsNullOrEmpty(((CommandMethodAttribute)m.GetCustomAttributes(typeof(CommandMethodAttribute), false)[0]).LocalizedNameId))
                    .ToDictionary(m =>
                    {
                        object[] commandAttributes = m.GetCustomAttributes(typeof(CommandMethodAttribute), false);
                        return resources.GetString(((CommandMethodAttribute)commandAttributes[0]).LocalizedNameId);
                    },
                    m =>
                    {
                        object[] descriptionAttributes = m.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        return descriptionAttributes.Length == 0 ? "" : ((DescriptionAttribute)descriptionAttributes[0]).Description;
                    }
                );
        }

        private Dictionary<string, string> GetCategoriesAndCommands()
        {
            ResourceManager resources = new ResourceManager(typeof(RebarPosCommands.MyCommands));
            return Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(CommandMethodAttribute), false).Length > 0)
                .Where(m => !string.IsNullOrEmpty(((CommandMethodAttribute)m.GetCustomAttributes(typeof(CommandMethodAttribute), false)[0]).LocalizedNameId))
                .GroupBy(m =>
                {
                    object[] categoryAttributes = m.GetCustomAttributes(typeof(CategoryAttribute), false);
                    return categoryAttributes.Length == 0 ? "" : ((CategoryAttribute)categoryAttributes[0]).Category;
                },
                    m =>
                    {
                        object[] commandAttributes = m.GetCustomAttributes(typeof(CommandMethodAttribute), false);
                        return resources.GetString(((CommandMethodAttribute)commandAttributes[0]).LocalizedNameId);
                    }).ToDictionary(g =>
                    {
                        return g.Key;
                    },
                    g =>
                    {
                        return string.Join(", ", g.ToArray());
                    }
                );
        }

        private string GetColumnFormattedText(Dictionary<string, string> columnTexts, int columnWidth)
        {
            int maxWidth = 0;

            foreach (var info in columnTexts)
            {
                maxWidth = Math.Max(maxWidth, info.Key.Length);
            }
            maxWidth += 2; // add 2 chars for colon and space after name

            StringBuilder formattedText = new StringBuilder();
            foreach (var info in columnTexts)
            {
                string name = info.Key;
                string[] desc = WordWrap(info.Value, Math.Max(20, columnWidth - maxWidth));
                for (int i = 0; i < desc.Length; i++)
                {
                    if (i == 0)
                    {
                        formattedText.Append(string.Format("{0,-" + (maxWidth - 2).ToString() + "}: ", name));
                    }
                    else
                    {
                        formattedText.Append(new string(' ', maxWidth));
                    }
                    formattedText.Append(desc[i]);
                    formattedText.AppendLine();
                }
            }

            return formattedText.ToString();
        }

        public string[] WordWrap(string text, int lineLength)
        {
            var charCount = 0;
            var lines = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                            .GroupBy(w => (charCount += w.Length + 1) / lineLength)
                            .Select(g => string.Join(" ", g.ToArray()));
            return lines.ToArray();
        }
    }
}
