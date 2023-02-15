using System;
using System.Collections.Generic;

using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace SmartPortal.Common
{
    /// <summary>
    /// Summary description for ZipHelp
    /// </summary>
    public class ZipHelp
    {
        /// <summary>
        /// Zip a file
        /// </summary>
        /// <param name="SrcFile">source file path</param>
        /// <param name="DstFile">zipped file path</param>
        /// <param name="BufferSize">buffer to use</param>
        public static void Zip(string SrcFile, string DstFile, int BufferSize)
        {
            FileStream fileStreamIn = new FileStream(SrcFile, FileMode.Open, FileAccess.Read);
            FileStream fileStreamOut = new FileStream(DstFile, FileMode.Create, FileAccess.Write);
            ZipOutputStream zipOutStream = new ZipOutputStream(fileStreamOut);

            byte[] buffer = new byte[BufferSize];

            ZipEntry entry = new ZipEntry(Path.GetFileName(SrcFile));
            zipOutStream.PutNextEntry(entry);

            int size;
            do
            {
                size = fileStreamIn.Read(buffer, 0, buffer.Length);
                zipOutStream.Write(buffer, 0, size);
            } while (size > 0);

            zipOutStream.Close();
            fileStreamOut.Close();
            fileStreamIn.Close();
        }

        /// <summary>
        /// UnZip a file
        /// </summary>
        /// <param name="SrcFile">source file path</param>
        /// <param name="DstFile">unzipped file path</param>
        /// <param name="BufferSize">buffer to use</param>
        public static void UnZip(string SrcFile, string DstFile, int BufferSize,int fileNumber)
        {
            FileStream fileStreamIn = new FileStream(SrcFile, FileMode.Open, FileAccess.Read);
            ZipInputStream zipInStream = new ZipInputStream(fileStreamIn);
            ZipEntry entry;
            FileStream fileStreamOut = null;
            for (int i = 0; i < fileNumber; i++)
            {
                entry = zipInStream.GetNextEntry();

                if (entry == null)
                {
                    return;
                }

                if (entry.IsFile)
                {
                    fileStreamOut = new FileStream(DstFile + @"\" + entry.Name, FileMode.Create, FileAccess.Write);

                    int size;
                    byte[] buffer = new byte[BufferSize];
                    do
                    {
                        size = zipInStream.Read(buffer, 0, buffer.Length);
                        fileStreamOut.Write(buffer, 0, size);
                    } while (size > 0);
                }
                else
                {
                    if (entry.IsDirectory)
                    {
                        System.IO.Directory.CreateDirectory(DstFile + @"\" + entry.Name);
                    }
                }
            }
            zipInStream.Close();
            fileStreamOut.Close();
            fileStreamIn.Close();
        }
    }
}
