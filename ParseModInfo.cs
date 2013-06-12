using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RK_Tool_kit
{
    public static class ParseModInfo
    {
        public static char[] toTrim = { ' ' };
        public static string halfline = String.Empty;

        public static string getName(string path)
        {
            string line;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("name"))
                    {
                        line = line.Replace("\"name\": ", "").Replace("\"name\" : ", "").Replace("\"", "").Replace(",", "").TrimStart(toTrim);

                        if (line == "")
                        {
                            return "N/A";
                        }
                        return line;
                    }
                }
            }

            return "N/A";
        }
        public static string getVersion(string path)
        {
            string line;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("version") && !line.Contains("mcversion") && !line.Contains("modinfoversion"))
                    {
                        line = line.Replace("\"version\": ", "").Replace("\"version\" : ", "").Replace("\"", "").Replace(",", "").TrimStart(toTrim);

                        if (line == "")
                        {
                            return "N/A";
                        }
                        return line;
                    }
                }
            }

            return "N/A";
        }
        public static string getMinecraftVersion(string path)
        {
            string line;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("mcversion"))
                    {
                        line = line.Replace("\"mcversion\": ", "").Replace("\"mcversion\" : ", "").Replace("\"", "").Replace(",", "").TrimStart(toTrim);

                        if (line == "")
                        {
                            return "N/A";
                        }
                        return line;
                    }
                }
            }

            return "N/A";
        }

        public static string getDesc(string path)
        {
            string line;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("description"))
                    {
                        line = line.Replace("\"description\": ", "").Replace("\"description\" : ", "").Replace("\"", "").Replace(",", "").TrimStart(toTrim);

                        if (line == "")
                        {
                            return "N/A";
                        }
                        return line;
                    }
                }
            }

            return "N/A";
        }
        public static string getAuthors(string path)
        {
             string line;

             using (StreamReader sr = new StreamReader(path))
             {
                 while (!sr.EndOfStream)
                 {
                     line = sr.ReadLine();
                     if (line.Contains("author"))
                     {

                         if (line.Contains("[") && line.Contains("]"))
                         {
                             line = line.Replace("\"authors\": ", "").Replace("\"authors\" : ", "").Replace("\"authorList\": ", "").Replace("\"authorList\" : ", "").Replace("[", "").Replace("]", "").Replace("\"", "").TrimStart(toTrim);
                             int Place = line.LastIndexOf(",");
                             string comma = ",";
                             line = line.Remove(Place, comma.Length).Insert(Place, "");

                             if (line == "")
                             {
                                 return "N/A";
                             }
                             return line;
                         }
                         else
                         {
                             line = sr.ReadLine();
                             line = line.Replace("\"", "").TrimStart(toTrim);


                             //Getting VERY weird errors with this. One goto statment throws a NullReference exception.
                             if (line.Contains(","))
                             {
                                 halfline = sr.ReadLine().Replace("\"", "").TrimStart(toTrim);
                                 line = line + halfline;
                             }

                         LookForComma:
                             if (halfline.Contains(","))
                             {
                                 halfline = sr.ReadLine().Replace("\"", "").TrimStart(toTrim);
                                 line = line + halfline;
                                 goto LookForComma;
                             }

                         if (line == "")
                         {
                             return "N/A";
                         }
                             return line;
                         }
                     }
                 }
             }

            return "N/A";
        }
        public static string getDepend(string path)
        {
            string line;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("dependencies"))
                    {

                        if (line.Contains("[") && line.Contains("]"))
                        {
                            line = line.Replace("\"dependencies\": ", "").Replace("\"dependencies\" : ", "").Replace("[", "").Replace("]", "").Replace("\"", "").TrimStart(toTrim);
                            try
                            {
                                int Place = line.LastIndexOf(",");
                                string comma = ",";
                                line = line.Remove(Place, comma.Length).Insert(Place, "");
                            }
                            catch (Exception)
                            {

                            }


                            if (line == "")
                            {
                                return "N/A";
                            }
                            return line;
                        }
                        else
                        {
                            line = sr.ReadLine();
                            line = line.Replace("\"", "").TrimStart(toTrim);


                            //Getting VERY weird errors with this. One goto statment throws a NullReference exception.
                            if (line.Contains(","))
                            {
                                halfline = sr.ReadLine().Replace("\"", "").TrimStart(toTrim);
                                line = line + halfline;
                            }

                        LookForComma:
                            if (halfline.Contains(","))
                            {
                                halfline = sr.ReadLine().Replace("\"", "").TrimStart(toTrim);
                                line = line + halfline;
                                goto LookForComma;
                            }

                            line = line.Replace("useDependencyInformation: true", "");
                            line = line.Replace("useDependencyInformation : true", "");

                            if (line == "" || line == "]")
                            {
                                return "N/A";
                            }
                            return line;
                        }
                    }
                }
            }

            return "N/A";
        }
    }
}
