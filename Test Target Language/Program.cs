using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sdl.ProjectAutomation.Core;
using Sdl.ProjectAutomation.FileBased;
using Studio.AssemblyResolver;
using System.Reflection;

namespace Test_Target_Language
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (currentPath == null) return;

            var projectInfo = new ProjectInfo()
            {
                Name = "Test project",
                Description = "some description",
                DueDate = new DateTime(2015, 01, 23),
                LocalProjectFolder = Path.Combine(currentPath,"Lab")
            };

            var templateReference =
                new ProjectTemplateReference(Path.Combine(currentPath, @"\Templates\test-target-language.sdltpl"));

            var project = new FileBasedProject(projectInfo, templateReference);

            var addedFiles = project.AddFolderWithFiles(Path.Combine(currentPath, @"\Files\"), true);

            project.Save();

            TaskSequence taskSequence = project.RunAutomaticTasks(addedFiles.GetIds(), TaskSequences.PrepareNoProjectTm);

            project.Save();
        }
    }
}
