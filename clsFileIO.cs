using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebFileIO
{
    public class clsFileIO
    {
        private clsFileIO()
        {
            //status=0 -> Failure
            //status=1 -> success
        }//eof

        /*The following TAG must be added in Web.Config File
        * <httpRuntime maxRequestLength="524288" executionTimeout="360"/>
        * before <pages> tag
        * Here 524288 = 5 MB.... means mention the max file size
        * */
        
        #region upload-download functions

        #region Delete the uploaded file
        /// <summary>
        /// Delete a File from a specified Directory (If the File isExist)
        /// </summary>
        /// <param name="dirPath">Define the Directory Path</param>
        /// <param name="fileName">Define the File Name with Extension</param>
        public static void deleteAttachFile(string dirPath, string fileName)
        {
            string path;
            string lastChar = "";
            System.IO.FileInfo docFile;
            try
            {                
                if (string.IsNullOrEmpty(dirPath.Trim()) == true)
                {
                    throw new System.Exception("Invalid Directory Name...............");
                }
                else
                {
                    if (string.IsNullOrEmpty(fileName.Trim()) == false)
                    {
                        lastChar = dirPath.Substring(dirPath.Length - 1);
                        if (lastChar != "\\")
                        {
                            path = dirPath + "\\" + fileName.Trim();
                        }
                        else
                        {
                            path = dirPath + fileName.Trim();
                        }
                        docFile = new System.IO.FileInfo(path);
                        if (docFile.Exists)
                        {
                            docFile.Delete();
                        }
                    }                    
                }
            }
            catch (System.IO.DirectoryNotFoundException di)
            {
                throw new Exception("Directory not found......");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                docFile = null;
            }
        }//eof
        #endregion

        #region verify the file existance
        /// <summary>
        /// This Function will Search the Any specified file within the specified directory 
        /// </summary>
        /// <param name="dirPath">Define the Directory Path</param>
        /// <param name="fileName">Define the File Name with Extension</param>
        /// <returns></returns>
        public static bool attachFileIsExist(string dirPath, string fileName)
        {
            string path;
            string lastChar = "";
            bool status = false;
            System.IO.FileInfo docFile;
            try
            {
               
                if (string.IsNullOrEmpty(fileName.Trim()) == true)
                {
                    throw new System.Exception("Invalid File Name...............");
                }
                else if (string.IsNullOrEmpty(dirPath.Trim()) == true)
                {
                    throw new System.Exception("Invalid Directory Name...............");
                }
                else
                {
                    lastChar = dirPath.Substring(dirPath.Length - 1);
                    if (lastChar != "\\")
                    {
                        path = dirPath + "\\" + fileName.Trim();
                    }
                    else
                    {
                        path = dirPath + fileName.Trim();
                    }
                    docFile = new System.IO.FileInfo(path);
                    if (docFile.Exists)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                return status;
            }
            catch (System.IO.DirectoryNotFoundException di)
            {
                throw new Exception("Directory not found......");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                docFile = null;
            }
        }//eof
        /// <summary>
        /// This Function will search the specified file name with the extension from File Uploader within the specified directory (USefull when file uploaded with a user - specific name)
        /// </summary>
        /// <param name="dirPath">Define the Directory Path</param>
        /// <param name="fileName">Define the File Name WITHOUT Extension</param>
        /// <param name="fileUp">ID of fileuploader control</param>
        /// <returns></returns>
        public static bool attachFileIsExist(string dirPath, string fileName, System.Web.UI.WebControls.FileUpload fileUp)
        {
            bool status = false;
            try
            {
                if (fileUp.HasFile)
                {
                    fileName = fileName.Replace("/", "");
                    fileName = fileName.Replace("\\", "");
                    fileName = fileName.Replace(".", "");

                    if (string.IsNullOrEmpty(fileName.Trim()) == true)
                    {
                        throw new System.Exception("Invalid File Name...............");
                    }
                    else
                    {
                        fileName = fileName + System.IO.Path.GetExtension(fileUp.PostedFile.FileName);
                        status=attachFileIsExist(dirPath,fileName);
                    }
                }
                return status;
            }
            catch (System.IO.DirectoryNotFoundException di)
            {
                throw new Exception("Directory not found......");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                
            }
        }//eof
        /// <summary>
        /// This Function will search the file from File Uploader within the specified directory
        /// </summary>
        /// <param name="dirPath">Define the Directory Path</param>
        /// <param name="fileUp">ID of fileuploader control</param>
        /// <returns></returns>
        public static bool attachFileIsExist(string dirPath, System.Web.UI.WebControls.FileUpload fileUp)
        {
            string fileName = "";
            bool status = false;
            try
            {
                if (fileUp.HasFile)
                {
                    fileName = fileUp.FileName;
                    status = attachFileIsExist(dirPath, fileName);
                }
                return status;
            }
            catch (System.IO.DirectoryNotFoundException di)
            {
                throw new Exception("Directory not found......");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                
            }
        }//eof

        #endregion

        
        #region download the existing file
        public static void downloadAttachFile(string dirPath, string fileName)
        {
            string path = "";
            string lastChar = "";
            System.IO.FileInfo docFile;

            try
            {
                if (dirPath.Trim() != "" && fileName.Trim() != "")
                {
                    dirPath = dirPath.Replace("'", "_");
                    lastChar = dirPath.Substring(dirPath.Length - 1);
                    if (lastChar != "\\")
                    {
                        path = dirPath + "\\" + fileName.Trim();
                    }
                    else
                    {
                        path = dirPath + fileName.Trim();
                    }
                    docFile = new System.IO.FileInfo(path);
                    if (docFile.Exists)
                    {
                        System.Web.HttpContext.Current.Response.Clear();
                        System.Web.HttpContext.Current.Response.ClearContent();
                        System.Web.HttpContext.Current.Response.ClearHeaders();
                        ////To forcefully download, even for Excel, PDF files, regardless of your IE's settings which may allow to open the files right in the browser.
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + docFile.Name);
                        System.Web.HttpContext.Current.Response.WriteFile(path);
                        System.Web.HttpContext.Current.Response.End();
                    }
                    else
                    {
                        throw new Exception("Path not found......");
                    }
                }
                else
                {
                    throw new Exception("Empty Directory or File Name......");
                }
            }
            catch (System.Threading.ThreadAbortException lException)
            {

                // do nothing
                //Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack

            }
            catch (System.IO.DirectoryNotFoundException di)
            {
                throw new Exception("Directory not found......");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                docFile = null;
                //newPage = null;
            }
        }//eof
        public static void downloadAttachFile(string filePath)
        {
            string path = "";
            System.IO.FileInfo docFile;

            try
            {
                path = filePath.Trim();
                if (string.IsNullOrWhiteSpace(path) == false)
                {
                    docFile = new System.IO.FileInfo(path);
                    if (docFile.Exists)
                    {
                        System.Web.HttpContext.Current.Response.Clear();
                        System.Web.HttpContext.Current.Response.ClearContent();
                        System.Web.HttpContext.Current.Response.ClearHeaders();
                        ////To forcefully download, even for Excel, PDF files, regardless of your IE's settings which may allow to open the files right in the browser.
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + docFile.Name);
                        System.Web.HttpContext.Current.Response.WriteFile(path);
                        System.Web.HttpContext.Current.Response.End();
                    }
                    else
                    {
                        throw new Exception("Path not found......");
                    }
                }
                else
                {
                    throw new Exception("Empty Directory or File Name......");
                }
            }
            catch (System.Threading.ThreadAbortException lException)
            {

                // do nothing
                //Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack

            }
            catch (System.IO.DirectoryNotFoundException di)
            {
                throw new Exception("Directory not found......");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                docFile = null;
                //newPage = null;
            }
        }//eof
        #endregion
        

        #region save the attach file
        /// <summary>
        /// Save the upload file in cusrom name
        /// </summary>
        /// <param name="fileUp">ASP.Net File uploader control instance</param>
        /// <param name="dirPath">Directory Path to store the file</param>
        /// <param name="fileName">File Name</param>
        /// <param name="fileNameLength">File Name Length cannot be less than or equal zero; working only on File Name, not on Extension</param>
        /// <returns></returns>
        public static string saveAs(System.Web.UI.WebControls.FileUpload fileUp, string dirPath, string fileName, int fileNameLength)
        {
            string dir = "";
            string lastChar = "";
            string strExt = "";
            int count = 0;

            int fnl = 0;
            //string fn = "";
            try
            {
                if (fileUp.HasFile)
                {
                    dir = dirPath.Trim();

                    strExt = System.IO.Path.GetExtension(fileUp.PostedFile.FileName);
                    count = strExt.Length;
                    fileName = fileName.Replace("/", "");
                    fileName = fileName.Replace("\\", "");
                    fileName = fileName.Replace(".", "");

                    if (fileNameLength <= 0)
                    {
                        throw new System.Exception("Invalid Name length for this type of file...............(File Name Length cannot be less than or equal zero)");
                    }
                    else if (string.IsNullOrEmpty(fileName.Trim()) == true)
                    {
                        throw new System.Exception("Invalid File Name...............");
                    }
                    else if (string.IsNullOrEmpty(dir.Trim()) == true)
                    {
                        throw new System.Exception("Invalid Directory Name...............");
                    }
                    else
                    {
                        fnl = fileName.Trim().Length;
                        if (fnl > fileNameLength)
                        {
                            fileName = fileName.Substring(0, fileNameLength);
                        }
                        fileName = fileName + strExt;

                        lastChar = dir.Substring(dir.Length - 1);

                        if (lastChar != "\\")
                        {
                            dir = dir + "\\" + fileName;
                        }
                        else
                        {
                            dir = dir + fileName;
                        }
                        fileUp.PostedFile.SaveAs(dir);
                    }//exceptions end
                }

                return fileName;
            }
            catch (System.IO.DirectoryNotFoundException di)
            {
                throw new Exception("Directory not found......");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        }//eof
        /// <summary>
        /// Save the upload file in cusrom name
        /// </summary>
        /// <param name="fileUp">ASP.Net File uploader control instance</param>
        /// <param name="dirPath">Directory Path to store the file</param>
        /// <param name="fileName">File Name</param>
        /// <returns></returns>
        public static string saveAs(System.Web.UI.WebControls.FileUpload fileUp, string dirPath, string fileName)
        {
            string strPostedFile = "";
            int fnl = 0;
            try
            {
                if (fileUp.HasFile)
                {
                    fnl = fileName.Length;

                    strPostedFile = saveAs(fileUp, dirPath, fileName, fnl);
                }
                return strPostedFile;
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        }//eof
        /// <summary>
        /// Save the upload file
        /// </summary>
        /// <param name="fileUp">File Name</param>
        /// <param name="dirPath">Directory Path to store the file</param>
        /// <returns></returns>
        public static string save(System.Web.UI.WebControls.FileUpload fileUp, string dirPath)
        {
            string strPostedFile = "";
            string strFileName = "";
            int fnl = 0;
            try
            {
                if (fileUp.HasFile)
                {
                    strFileName = System.IO.Path.GetFileName(fileUp.PostedFile.FileName.ToString().Trim());
                    strFileName = strFileName.Substring(0, strFileName.Trim().LastIndexOf(".")); //pick up the filename
                    fnl = strFileName.Length;

                    strPostedFile = saveAs(fileUp, dirPath, strFileName, fnl);
                }

                return strPostedFile;
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        }//eof
        /// <summary>
        /// Save the upload file
        /// </summary>
        /// <param name="fileUp">File Name</param>
        /// <param name="dirPath">Directory Path to store the file</param>
        /// <param name="fileNameLength">File Name Length cannot be less than or equal zero; working only on File Name, not on Extension</param>
        /// <returns></returns>
        public static string save(System.Web.UI.WebControls.FileUpload fileUp, string dirPath, int fileNameLength)
        {
            string strPostedFile = "";
            string strFileName = "";
            int fnl = 0;
            try
            {
                if (fileUp.HasFile)
                {
                    strFileName = System.IO.Path.GetFileName(fileUp.PostedFile.FileName.ToString().Trim());
                    fnl = fileNameLength;
                    strFileName = strFileName.Substring(0, strFileName.Trim().LastIndexOf(".")); //pick up the filename

                    strPostedFile = saveAs(fileUp, dirPath, strFileName, fnl);
                }
                return strPostedFile;
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }//eof

        #endregion

        #region checking file size and extensions
        public static bool checkFileSize(System.Web.UI.WebControls.FileUpload fileUp, int fileSize_in_Byte)
        {
            bool status = true;
            try
            {
                if (fileUp.HasFile)
                {
                    if (fileUp.PostedFile.ContentLength > fileSize_in_Byte)
                    {
                        status = false;
                    }
                    if (fileUp.PostedFile.ContentLength <= fileSize_in_Byte)
                    {
                        status = true;
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        }//eof
        public static bool checkFileExtension(System.Web.UI.WebControls.FileUpload fileUp, string[] extension)
        {
            bool status = true;
            string extn = "";
            string fileName = "";
            try
            {
                if (fileUp.HasFile && extension.Length > 0)
                {
                    fileName = System.Web.HttpContext.Current.Server.HtmlEncode(fileUp.PostedFile.FileName.ToString().Trim());
                    fileName = System.IO.Path.GetExtension(fileName);
                    for (int i = 0; i < extension.Length; i++)
                    {
                        extn = extension[i].Trim();
                        extn = extn.Substring(0, 1);
                        if (extn == ".")
                        {
                            extn = extension[i].Trim();
                        }
                        else
                        {
                            extn = "." + extension[i].Trim();
                        }
                        if (fileName.Trim().ToUpper() == extn.ToUpper())
                        {
                            status = true;
                            break;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        }//eof

        #endregion
        
        #endregion

        #region file informations
        public static System.Data.DataTable fileInfos(string dirPath, string fileNameOnly, string extensionOnly, ALL_OR_TOP ALLorTop)
        {
            System.Data.DataTable tblFileInfo;
            System.Data.DataColumn colInfo;
            System.Data.DataRow rowInfo;
            System.IO.DirectoryInfo dirInfo = null;
            System.IO.FileInfo[] fiInfo = null;
            try
            {
                tblFileInfo = new System.Data.DataTable();
                for (int i = 1; i <= 6; i++)
                {
                    colInfo = new System.Data.DataColumn();
                    colInfo.DataType = typeof(string);
                    if (i == 1)
                    {
                        colInfo.ColumnName = "FileName";
                    }
                    else if (i == 2)
                    {
                        colInfo.ColumnName = "FileSize";
                    }
                    else if (i == 3)
                    {
                        colInfo.ColumnName = "Extension";
                    }
                    else if (i == 4)
                    {
                        colInfo.ColumnName = "CreationTime";
                    }
                    else if (i == 5)
                    {
                        colInfo.ColumnName = "LastAccessTime";
                    }
                    else if (i == 6)
                    {
                        colInfo.ColumnName = "LastWriteTime";
                    }
                    tblFileInfo.Columns.Add(colInfo);
                }
                if (System.IO.Directory.Exists(dirPath))
                {
                    dirInfo = new System.IO.DirectoryInfo(dirPath);
                    if (ALLorTop == ALL_OR_TOP.Top)
                    {
                        fiInfo = dirInfo.GetFiles(fileName(fileNameOnly, extensionOnly), SearchOption.TopDirectoryOnly);
                    }
                    else
                    {
                        fiInfo = dirInfo.GetFiles(fileName(fileNameOnly, extensionOnly), SearchOption.AllDirectories);
                    }

                    if (fiInfo.Length > 0)
                    {
                        foreach (FileInfo info in fiInfo)
                        {
                            if (string.Compare(info.Name.ToString().Trim(), "Thumbs.db", true) != 0)
                            {
                                rowInfo = tblFileInfo.NewRow();
                                rowInfo["FileName"] = info.Name.ToString().Trim();
                                rowInfo["Extension"] = info.Extension.ToString().Trim();
                                rowInfo["FileSize"] = info.Length.ToString().Trim();
                                rowInfo["CreationTime"] = info.CreationTime.ToString("dd-MMM-yyyy");
                                rowInfo["LastAccessTime"] = info.LastAccessTime.ToString("dd-MMM-yyyy");
                                rowInfo["LastWriteTime"] = info.LastWriteTime.ToString("dd-MMM-yyyy");
                                tblFileInfo.Rows.Add(rowInfo);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Directory not found...........");
                }
                return tblFileInfo;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //tblFileInfo = null;
            }
        }//eof
        public static System.Data.DataTable fileInfos(string dirPath, string fileNameOnly, string extensionOnly, ALL_OR_TOP ALLorTop, out int rows)
        {
            System.Data.DataTable tblFileInfo;
            System.Data.DataColumn colInfo;
            System.Data.DataRow rowInfo;
            System.IO.DirectoryInfo dirInfo = null;
            System.IO.FileInfo[] fiInfo = null;
            rows = 0;
            try
            {
                tblFileInfo = new System.Data.DataTable();
                for (int i = 1; i <= 6; i++)
                {
                    colInfo = new System.Data.DataColumn();
                    colInfo.DataType = typeof(string);
                    if (i == 1)
                    {
                        colInfo.ColumnName = "FileName";
                    }
                    else if (i == 2)
                    {
                        colInfo.ColumnName = "FileSize";
                    }
                    else if (i == 3)
                    {
                        colInfo.ColumnName = "Extension";
                    }
                    else if (i == 4)
                    {
                        colInfo.ColumnName = "CreationTime";
                    }
                    else if (i == 5)
                    {
                        colInfo.ColumnName = "LastAccessTime";
                    }
                    else if (i == 6)
                    {
                        colInfo.ColumnName = "LastWriteTime";
                    }
                    tblFileInfo.Columns.Add(colInfo);
                }
                if (System.IO.Directory.Exists(dirPath))
                {
                    dirInfo = new System.IO.DirectoryInfo(dirPath);
                    if (ALLorTop == ALL_OR_TOP.Top)
                    {
                        fiInfo = dirInfo.GetFiles(fileName(fileNameOnly, extensionOnly), SearchOption.TopDirectoryOnly);
                    }
                    else
                    {
                        fiInfo = dirInfo.GetFiles(fileName(fileNameOnly, extensionOnly), SearchOption.AllDirectories);
                    }

                    if (fiInfo.Length > 0)
                    {
                        foreach (FileInfo info in fiInfo)
                        {
                            if (string.Compare(info.Name.ToString().Trim(), "Thumbs.db", true) != 0)
                            {
                                rowInfo = tblFileInfo.NewRow();
                                rowInfo["FileName"] = info.Name.ToString().Trim();
                                rowInfo["Extension"] = info.Extension.ToString().Trim();
                                rowInfo["FileSize"] = info.Length.ToString().Trim();
                                rowInfo["CreationTime"] = info.CreationTime.ToString("dd-MMM-yyyy");
                                rowInfo["LastAccessTime"] = info.LastAccessTime.ToString("dd-MMM-yyyy");
                                rowInfo["LastWriteTime"] = info.LastWriteTime.ToString("dd-MMM-yyyy");
                                tblFileInfo.Rows.Add(rowInfo);
                                rows += 1;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Directory not found.........");
                }
                return tblFileInfo;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //tblFileInfo = null;
            }
        }//eof
        private static string fileName(string fileNameOnly, string extensionOnly)
        {
            string strFn = "";
            string strExt = "";
            try
            {
                if (fileNameOnly.Trim() == "" && extensionOnly.Trim() == "")
                {
                    strFn = "*.*";
                }
                else if (fileNameOnly.Trim() == "" && extensionOnly.Trim() != "")
                {
                    strExt = extensionOnly.Trim();
                    strExt = extensionOnly.Substring(0, 1);
                    if (strExt == ".")
                    {
                        strExt = extensionOnly.Trim();
                    }
                    else
                    {
                        strExt = "." + extensionOnly.Trim();
                    }
                    strFn = "*" + strExt.Trim();
                }
                else if (fileNameOnly.Trim() != "" && extensionOnly.Trim() == "")
                {
                    strFn = fileNameOnly.Trim().Replace(".", " ");
                    strFn = "*" + strFn.Trim() + "*";
                }
                else if (fileNameOnly.Trim() != "" && extensionOnly.Trim() != "")
                {
                    strFn = fileNameOnly.Trim().Replace(".", " ");
                    strExt = extensionOnly.Trim();
                    strExt = extensionOnly.Substring(0, 1);
                    if (strExt == ".")
                    {
                        strExt = extensionOnly.Trim();
                    }
                    else
                    {
                        strExt = "." + extensionOnly.Trim();
                    }
                    strFn = strFn.Trim() + strExt.Trim();
                }
                return strFn;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }//eof
        public static System.Data.DataTable dirInfos(string dirPath, out int rows)
        {
            System.Data.DataTable tblDirInfo;
            System.Data.DataColumn colInfo;
            System.Data.DataRow rowInfo;
            System.IO.DirectoryInfo dirInfo = null;
            rows = 0;
            try
            {
                tblDirInfo = new System.Data.DataTable();
                colInfo = new System.Data.DataColumn();
                colInfo.DataType = typeof(string);
                colInfo.ColumnName = "DirectoryName";
                tblDirInfo.Columns.Add(colInfo);
                colInfo = new System.Data.DataColumn();
                colInfo.DataType = typeof(DateTime);
                colInfo.ColumnName = "CreatedOn";
                tblDirInfo.Columns.Add(colInfo);
                colInfo = new System.Data.DataColumn();
                colInfo.DataType = typeof(DateTime);
                colInfo.ColumnName = "LastAccessOn";
                tblDirInfo.Columns.Add(colInfo);
                if (System.IO.Directory.Exists(dirPath))
                {
                    dirInfo = new System.IO.DirectoryInfo(dirPath);
                    foreach (DirectoryInfo dirs in dirInfo.GetDirectories())
                    {
                        if (string.Compare(dirs.Name.ToString().Trim(), "Thumbs.db", true) != 0)
                        {
                            rowInfo = tblDirInfo.NewRow();
                            rowInfo["DirectoryName"] = dirs.Name.ToString().Trim();
                            rowInfo["CreatedOn"] = dirs.CreationTime.Date;
                            rowInfo["LastAccessOn"] = dirs.LastAccessTime.Date;
                            tblDirInfo.Rows.Add(rowInfo);

                            rows++;
                        }
                    }
                }
                else
                {
                    throw new Exception("Directory not found...........");
                }
                return tblDirInfo;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //tblDirInfo = null;
            }
        }//eof
        public enum ALL_OR_TOP
        {
            All = 0,
            Top = 1
        }//eof

        public static string getFilePath(string dirName, string fileName)
        {
            string path = "";
            path = System.Web.HttpContext.Current.Server.MapPath(dirName + "/" + fileName);
            return path;
        }//eof  
        public static string getDirPath(string dirName)
        {
            string path = "";
            path = System.Web.HttpContext.Current.Server.MapPath(dirName);
            return path;
        } //eof
        #endregion

    }


}