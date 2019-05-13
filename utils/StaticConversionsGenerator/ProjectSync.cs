using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticConversionsGenerator
{
    /// <summary>
    /// Utility class to sync between .Net Standard 2.0 and NetFx projects.
    /// </summary>
    internal class ProjectSync
    {
        public const string ProjectRootFolder = @"..\..\..\..\..\..\Repos\runtime-services\src";
        readonly string ConversionServicesProjectName = $"System.Runtime.{nameof(System.Runtime.ConversionServices)}";
        readonly string RuntimeServicesProjectName = $"System.Runtime.{nameof(System.Runtime.RuntimeServices)}";

        private SyncType syncType;
        private BackupSync backup;

        internal ProjectSync(SyncType toFx, BackupSync backUp)
        {
            this.syncType = toFx;
            this.backup = backUp;
        }

        internal enum SyncType
        {
            ToFx,
            ToStandard
        }

        internal enum BackupSync
        {
            False,
            True
        }

        public static void SyncToFx()
        {
            var sync = new ProjectSync(SyncType.ToFx, BackupSync.True);
            sync.Run();
        }

        public void Run()
        {
            if (syncType == SyncType.ToFx)
                Sync(ConversionServicesProjectName, RuntimeServicesProjectName);
            else
                Sync(RuntimeServicesProjectName, ConversionServicesProjectName);
        }

        private void Sync(string srcProjectName, string destProjectName)
        {
            var repoFullPath = Path.GetFullPath(ProjectRootFolder);
            System.Diagnostics.Debug.Assert(Directory.Exists(repoFullPath));

            var srcProjectFolder = Path.Combine(repoFullPath, srcProjectName);
            var destProjectFolder = Path.Combine(repoFullPath, destProjectName);

            System.Diagnostics.Debug.Assert(Directory.Exists(srcProjectFolder));
            System.Diagnostics.Debug.Assert(Directory.Exists(destProjectFolder));

            var srcFolder = new DirectoryInfo(srcProjectFolder);
            var destFolder = new DirectoryInfo(destProjectFolder);
            CopyCsFiles(srcFolder, destFolder);

            Console.WriteLine($"Sync complete. Files will need to be manualy added to {destProjectName}");

        }

        private void CopyCsFiles(DirectoryInfo srcFolder, DirectoryInfo destFolder)
        {
            destFolder.Create();

            foreach (var srcFile in srcFolder.GetFiles("*.cs"))
            {
                CopyFile(srcFile, destFolder);
            }

            foreach (var childDirectory in srcFolder.GetDirectories())
            {
                var destPath = Path.Combine(destFolder.FullName, childDirectory.Name);
                var destChild = new DirectoryInfo(destPath);
                destChild.Create();
                CopyCsFiles(childDirectory, destChild);
            }
        }

        private void CopyFile(FileInfo srcFile, DirectoryInfo destFolder)
        {

            var destFolderName = destFolder.FullName;
            var destFileName = srcFile.Name;

            if (backup == BackupSync.True && File.Exists(Path.Combine(destFolderName, srcFile.Name)))
                destFileName += $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.bak";

            var destFilePath = Path.Combine(destFolderName, destFileName);

            var srcCode = File.ReadAllText(srcFile.FullName);
            if (syncType == SyncType.ToFx)
            {
                srcCode = srcCode.Replace(ConversionServicesProjectName, RuntimeServicesProjectName);
            }
            else
            {
                srcCode = srcCode.Replace(RuntimeServicesProjectName, ConversionServicesProjectName);
            }
            File.WriteAllText(destFilePath, srcCode);



        }
    }
}
